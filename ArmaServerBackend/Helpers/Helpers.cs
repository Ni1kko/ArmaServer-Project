using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ArmaServerBackend
{
    public class Helpers
    {
        public static List<Process> ObfuProcs = new List<Process>();

        public static string RemoveComments(string source)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";

            string noComments = Regex.Replace(source,
            blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
            me => {
                if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                    return me.Value.StartsWith("//") ? Environment.NewLine : "";
                // Keep the literal strings
                return me.Value;
            },
            RegexOptions.Singleline);

            return noComments;
        }

        public static string OneLine(string contents)
        {
            string newshit = RemoveComments(contents);
            newshit = newshit.Replace("\n", " ");
            newshit = newshit.Replace("\r", "");

            return newshit;
        }

        public static bool Move(PboFiles sm)
        {
            string folderPath = Path.Combine(DLL.ConfigValues.GitDirectory, sm.GitPath);
            string modPath = Path.Combine(DLL.ConfigValues.GitDirectory, sm.Name) + ".pbo";

            try
            {
                File.Copy(modPath, sm.ServerPath, true);

                Console.WriteLine($"Moved ({sm.Name}): {modPath} => {sm.ServerPath}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error moving files");
                Utilities.Assert(ex);
                return false;
            }
        }

        public static void Pack(PboFiles sm)
        {

            string folderPath = Path.Combine(DLL.ConfigValues.GitDirectory, sm.GitPath);
            string modPath = Path.Combine(DLL.ConfigValues.GitDirectory, sm.Name);

             
            Console.WriteLine($"Packing {sm.Name}\n");

            PboFile pbo = new PboFile();

            foreach (string s in Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories))
            {
                string path = s.Replace(folderPath, "");

                if (path.StartsWith(@"\"))
                    path = path.Substring(1);

                string file = File.ReadAllText(s);

                pbo.AddEntry(path, Encoding.UTF8.GetBytes(file));
            }

            pbo.Save($"{modPath}.pbo");
        }
        
        public string GetFolderPathDialog()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    return fbd.SelectedPath;
                }
                else
                {
                    return "";
                }
            }
        }

        public static void EndTask(string taskname)
        {
            string processName = taskname;
            string fixstring = taskname.Replace(".exe", "");

            if (taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(fixstring))
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

        public static void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        public static void RandomizeEverything(PboFiles sm)
        {
            string folderPath = Path.Combine(DLL.ConfigValues.GitDirectory, sm.GitPath);


            Console.WriteLine("Begining obfuscation");


            string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

            foreach (string file in files)
            {

                if (file.EndsWith(".ogg") || file.EndsWith(".paa") || file.EndsWith(".jpg") || file.EndsWith(".png"))
                    continue;

                string outName = file;

                string contents = File.ReadAllText(file);

                if (sm.RenameGlobalVars)
                {
                    contents = Utilities.RenameVars(contents, DLL._globalVars);
                }

                if (sm.RenameLocalVars)
                {
                    contents = Utilities.RenameVars(contents, DLL._localVars);
                }

                if (sm.RenameFuncs)
                {
                    foreach (KeyValuePair<string, string> kv in DLL._scriptFuncs)
                    {
                        contents = contents.Replace(kv.Key, kv.Value);

                        if (file.Contains(kv.Key))
                        {
                            outName = file.Replace(kv.Key, kv.Value);
                        }
                    }
                }

                if (sm.OneLine)
                {
                    if (file.EndsWith(".sqf"))
                    {
                        contents = Helpers.OneLine(contents);
                    }
                }

                File.WriteAllText(file, contents);
                if (file != outName)
                {
                    Console.WriteLine($"Filename: {file} => {outName}");
                    File.Move(file, outName);
                }
            }

            Console.WriteLine($"{sm.Name} obfuscated.");
        }
    }
}
