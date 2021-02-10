using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO.Compression; 
using System.Net;

namespace ArmaServerBackend
{
    public class Helpers
    {
        private static Random random = new Random();
        private static List<string> pulledGits = new List<string>();

        public string GetFolderPathDialog()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            { 
                return (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath)) ? folderBrowserDialog.SelectedPath : "";
            }
        }

        internal static bool GitDownload(PboFiles pbo)
        {
            if (pulledGits.Contains(pbo.GitUrl.ToLower())) return true;

            pulledGits.Add(pbo.GitUrl.ToLower());

            Console.WriteLine("Pulling from git...");

            try
            {
                string token = pbo.GitToken.Replace("token ", "");
                string gitPath = Path.Combine(DLL.ConfigValues.GitDirectory + "/git.zip");

                if (!Directory.Exists(DLL.ConfigValues.GitDirectory)) Directory.CreateDirectory(DLL.ConfigValues.GitDirectory);

                using (var webClient = new WebClient())
                {
                    if (pbo.GitType == 1)
                    {
                        webClient.Headers.Add("Authorization", "token " + token);
                        Console.WriteLine($"{pbo.Name} using GitHub");
                    }
                    else if (pbo.GitType == 2)
                    {
                        webClient.Headers.Add("Private-Token", token);
                        Console.WriteLine($"{pbo.Name} using GitLab");
                    }
                    else
                    {
                        throw new Exception("Unknown git type! 1 = GitHub, 2 = GitLab");
                    }
                    webClient.DownloadFile(pbo.GitUrl, gitPath);
                }

                if (Directory.Exists(Path.Combine(DLL.ConfigValues.GitDirectory, pbo.GitBranch))) Directory.Delete(DLL.ConfigValues.GitDirectory, true);
                Directory.CreateDirectory(DLL.ConfigValues.GitDirectory);

                // very dirty hack for gitlab stupid file bullshit
                if (pbo.GitType == 2)
                {

                    using (ZipArchive archive = ZipFile.OpenRead(gitPath))
                    {
                        string folderName = archive.Entries[0].FullName;

                        foreach (ZipArchiveEntry entry in archive.Entries.Skip(1))
                        {
                            string name = DLL.ConfigValues.GitDirectory + "/" + entry.FullName.Replace(folderName, "");
                            if (entry.FullName.EndsWith("/"))
                            {
                                Directory.CreateDirectory(name);
                            }
                            else
                            {
                                entry.ExtractToFile(name);
                            }
                        }
                    }
                }
                else
                {
                    ZipFile.ExtractToDirectory(gitPath, DLL.ConfigValues.GitDirectory); // github :)
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in download: " + ex);
                return false;
            }
        }

        internal static bool Move(PboFiles pbo)
        { 
            string modPath = Path.Combine(DLL.ConfigValues.GitDirectory, pbo.Name) + ".pbo";
            string serverPath = Path.Combine(pbo.ServerPath, pbo.Name) + ".pbo";

            try
            {
                File.Copy(modPath, serverPath, true);
                Console.WriteLine($"Moved ({pbo.Name}): {modPath} => {serverPath}");
                return File.Exists(serverPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error moving files");
                Utilities.Assert(ex);
                return false;
            }
        }

        internal static void Pack(PboFiles pbo)
        {

            string folderPath = Path.Combine(DLL.ConfigValues.GitDirectory, pbo.GitBranch);
            string modPath = Path.Combine(DLL.ConfigValues.GitDirectory, pbo.Name); 

            Console.WriteLine($"Packing {pbo.Name}\n");

            PboFile pboFile = new PboFile(); 
            foreach (string files in Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories))
            {
                string path = files.Replace(folderPath, "");
                if (path.StartsWith(@"\")) path = path.Substring(1);
                string file = File.ReadAllText(files);
                pboFile.AddEntry(path, Encoding.UTF8.GetBytes(file));
            }
            pboFile.Save($"{modPath}.pbo");
        }
                
        internal static void EndTask(string taskname)
        {
            string processName = taskname;

            if (taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(taskname.Replace(".exe", "")))
                {
                    process.Kill();
                }
            }
            else if (!taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(processName))
                {
                    process.Kill();
                }
            }
        }

        private static string RemoveComments(string source)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            string noComments = Regex.Replace(source, blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings, line => {
                if (line.Value.StartsWith("/*") || line.Value.StartsWith("//")) return line.Value.StartsWith("//") ? Environment.NewLine : "";
                return line.Value;// Keep the literal strings
            }, RegexOptions.Singleline);

            return noComments;
        }
        
        private static string OneLine(string file) => RemoveComments(file).Replace("\n", " ").Replace("\r", "");
          
        private static string RenameVars(string contents, Dictionary<string, string> vars)
        { 
            foreach (KeyValuePair<string, string> var in vars)
            {
                string pattern = string.Format(@"\b{0}\b", Regex.Escape(var.Key));
                contents = Regex.Replace(contents, pattern, var.Value, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            }
            return contents;
        }
        
        internal static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal static void RandomizeEverything(PboFiles pbo)
        {
            string folderPath = Path.Combine(DLL.ConfigValues.GitDirectory, pbo.GitBranch);

            Console.WriteLine($"Begining {pbo.Name} obfuscation");

            foreach (string file in (Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)))
            {
                string outName = file;

                if (outName.EndsWith(".ogg") || outName.EndsWith(".paa") || outName.EndsWith(".jpg") || outName.EndsWith(".png")) continue;

                string contents = File.ReadAllText(outName);

                if (pbo.RenameGlobalVars) contents = RenameVars(contents, DLL._globalVars);
                if (pbo.RenameLocalVars) contents = RenameVars(contents, DLL._localVars);
                if (pbo.RenameFuncs) 
                {
                    foreach (KeyValuePair<string, string> var in DLL._scriptFuncs)
                    {
                        contents = contents.Replace(var.Key, var.Value);
                        if (file.Contains(var.Key))
                        {
                            outName = file.Replace(var.Key, var.Value);
                            Console.WriteLine($"Filename: {file} => {outName}");
                        }
                    }
                }

                if (pbo.OneLine && file.EndsWith(".sqf")) contents = OneLine(contents);

                File.WriteAllText(file, contents);
                if (file != outName) File.Move(file, outName);
            }

            Console.WriteLine($"{pbo.Name} obfuscated.");
        }
    }
}
