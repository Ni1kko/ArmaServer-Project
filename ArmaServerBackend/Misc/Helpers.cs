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

        public bool isValidSteamID(string steamID)
        {
            if (steamID.Length != 17) return false;

            //TODO: Add steamkit dll and verify

            return true;
        }
         
        /// <summary>
        /// Opens Folder Path Dialog and returns selected folder path
        /// </summary>
        /// <returns></returns>
        public string GetFolderPathDialog()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            { 
                return (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath)) ? folderBrowserDialog.SelectedPath : "";
            }
        }

        
        /// <summary>
        /// Add new string to list box
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="newVar"></param>
        /// <returns></returns>
        public List<string> AddListBoxValue(ListBox listBox, string newVar)
        {
            List<string> varsInBox = new List<string>(); 
            if (newVar == "") return varsInBox;
            listBox.Items.Add(newVar);
            foreach (string item in listBox.Items) varsInBox.Add(item);
            return varsInBox;
        }

        /// <summary>
        /// Add new string to listbox from a textbox and returns new List Array
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="textBox"></param>
        /// <returns>List<string></returns>
        public List<string> AddListBoxValue(ListBox listBox, TextBox textBox)
        {
            string text = textBox.Text;
            if (text == "") return new List<string>();
            textBox.Text = "";
            return AddListBoxValue(listBox, text);
        }

        /// <summary>
        /// Add new list array of strings to list box
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="strings"></param>
        public void AddListBoxValue(ListBox listBox, List<string> strings)
        {
            foreach (string str in strings) AddListBoxValue(listBox, str);
        }

        /// <summary>
        /// Removes an item from given index if none given it will remove the user selected one and returns new List Array
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="SelectedItem"></param>
        /// <returns>List<string></returns>
        public List<string> RemoveListBoxValue(ListBox listBox, int SelectedItem = -1)
        {
            if (SelectedItem < 0) SelectedItem = listBox.SelectedIndex;
            listBox.Items.RemoveAt(SelectedItem);
            List<string> InBox = new List<string>();
            foreach (string item in listBox.Items) InBox.Add(item);
            return InBox;
        }

        /// <summary>
        /// Checks to see if string contains a charater
        /// </summary>
        /// <param name="text"></param>
        /// <returns>bool</returns>
        public bool StringContainsChar(string text)
        {
            List<string> letters = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            foreach (string _char in letters) if (text.ToLower().Contains(_char)) return true;
            return false;
        }

        /// <summary>
        /// Checks to see if string contains a number
        /// </summary>
        /// <param name="text"></param>
        /// <returns>bool</returns>
        public bool StringContainsNumber(string text)
        {
            List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            foreach (string _char in numbers) if (text.Contains(_char)) return true;
            return false;
        }

        /// <summary>
        /// Checks too see if string is already listed
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="stringToCheck"></param>
        /// <returns>bool</returns>
        public bool AlreadyInBox(ListBox listBox, string stringToCheck)
        {
            foreach (string item in listBox.Items) if (item == stringToCheck) return true;
            return false;
        }

        /// <summary>
        /// Checks too see if textBox is already listed
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public bool AlreadyInBox(ListBox listBox, TextBox textBox)
        {
            if(textBox.Text == "") return false;
            return AlreadyInBox(listBox, textBox.Text);
        }
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
        internal static bool GitDownload(PboFiles pbo)
        {  
            Console.WriteLine("Pulling from git...");

            try
            {
                string token = pbo.GitToken.Replace("token ", "");
                string gitPath = Path.Combine(DLL.ConfigValues.GitDirectory + "/git.zip");

                if (!Directory.Exists(DLL.ConfigValues.GitDirectory)) Directory.CreateDirectory(DLL.ConfigValues.GitDirectory);

                using (var webClient = new WebClient())
                {
                    if (pbo.GitServer == GitServer.GitHub)
                    {
                        webClient.Headers.Add("Authorization", "token " + token);
                        Console.WriteLine($"{pbo.Name} using GitHub");
                    }
                    else if (pbo.GitServer == GitServer.GitLab)
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
                if (pbo.GitServer == GitServer.GitLab)
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
            if (pbo.ModType != PboModType.Mission) serverPath = Path.Combine(pbo.ServerPath, "addons", pbo.Name) + ".pbo";

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
  
        internal static string NewLine(int numOfLines = 1)
        {
            var newLines = "";
            for (var i = 0; i < numOfLines; i++)
            {
                newLines += Environment.NewLine;
            }
            return newLines;
        }
        internal static string NewTab(int numOfTabs = 1)
        {
            var newLines = "";
            for (var i = 0; i < numOfTabs; i++)
            {
                newLines += "\t";
            }
            return newLines;
        }

        private static string RemoveComments(string source)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            string noComments = Regex.Replace(source, blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings, line => {
                if (line.Value.StartsWith("/*") || line.Value.StartsWith("//")) return line.Value.StartsWith("//") ? NewLine() : "";
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

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstvwxyz";
        private static string CreateRandomString(string character, int length) => new string(Enumerable.Repeat(character, length).Select(s => s[random.Next(s.Length)]).ToArray());

        public string RandomString(int length) => CreateRandomString(chars, length);
         
        public string RandomVariable(int length)
        {
            string variable = RandomString(1);
            variable += CreateRandomString(string.Format("{0}_0123456789", chars), length);
            if (variable.EndsWith("_")) variable = variable.TrimEnd('_');
            return variable;
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

                if (pbo.RandomizeGlobalVariables) contents = RenameVars(contents, DLL._globalVars);
                if (pbo.RandomizeLocalVariables) contents = RenameVars(contents, DLL._localVars);
                if (pbo.RandomizeFunctions) 
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

                if (pbo.SingleLineFunctions && file.EndsWith(".sqf")) contents = OneLine(contents);

                File.WriteAllText(file, contents);
                if (file != outName) File.Move(file, outName);
            }

            Console.WriteLine($"{pbo.Name} obfuscated.");
        }
    }
}
