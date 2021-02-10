using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO.Compression; 
using System.Net;
using Microsoft.Win32;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;

namespace ArmaServerBackend
{
    public class Helpers
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstvwxyz";
        private static Random random = new Random();

        /// <summary>
        /// Get the assembly version number
        /// </summary>
        /// <returns></returns>
        public static string GetAppVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }

        /// <summary>
        /// Gets Local steamIDs from steams config.vdf
        /// </summary>
        /// <returns>List of SteamIDs</returns>
        public List<string> GetLocalSteamIDS()
        {
            string path;
            List<string> LocalSteamIDS = new List<string>(); 
            if (FindSteamConfig(out path)) foreach (KeyValuePair<string, string> steamConfig in ParseSteamConfig(path)) LocalSteamIDS.Add(steamConfig.Key);
            return LocalSteamIDS;
        }

        /// <summary>
        /// Gets Local Accounts from steams config.vdf
        /// </summary>
        /// <returns>List of SteamIDs</returns>
        public List<string> GetLocalSteamAccount()
        {
            string path;
            List<string> LocalSteamIDS = new List<string>();
            if (FindSteamConfig(out path)) foreach (KeyValuePair<string, string> steamConfig in ParseSteamConfig(path)) LocalSteamIDS.Add(steamConfig.Value);
            return LocalSteamIDS;
        }

        /// <summary>
        /// Parses steams config.vdf
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns>Dictionary id, name</returns>
        private Dictionary<string, string> ParseSteamConfig(string configPath)
        {
            Dictionary<string, string> user = new Dictionary<string, string>();
            StreamReader file = new StreamReader(configPath);
            String text = null;
            String line;

            int i = 0;
            int k = 0;

            while ((line = file.ReadLine().Trim()) != null)
            {
                if (line.Contains("Accounts"))
                {
                    text = line + "\n";
                }
                else if (text != null)
                {
                    text += line + "\n";
                    if (line.Contains("{")) i++;
                    if (line.Contains("}")) k++;
                    if (i == k)
                    {
                        break;
                    }
                }
            }

            file.Close();

            var regex = new Regex(@"[^\S\r\n]+|.*Accounts.*|.*}.*|.*{.*", RegexOptions.Multiline);
            text = regex.Replace(text, "");
            text = text.Replace("\"SteamID\"", "");
            text = text.Replace("\"", "");


            string[] userIDArray = text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (userIDArray.Length < 2 || userIDArray.Length % 2 != 0)
                return user;

            for (i = 1; i < userIDArray.Length; i = i + 2)
            {
                string name = userIDArray[i - 1];
                string id = userIDArray[i];
                user.Add(id, name);
            }

            return user;
        }

        /// <summary>
        /// Finds steams config.vdf
        /// </summary>
        /// <param name="path"></param>
        /// <returns>bool</returns>
        private bool FindSteamConfig(out string path)
        {
            string configPath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", "InstallPath", string.Empty);
            configPath += @"\config\config.vdf";
            if (File.Exists(configPath))
            {
                path = configPath;
                return true;
            }
            path = "";
            return false;
        }

        /// <summary>
        /// Check to see if given string steamID is valid
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns></returns>
        public bool IsValidSteamID(string SteamIDIn)
        {
            long steamID;
            bool status = long.TryParse(SteamIDIn, out steamID);
            if (status && IsValidSteamID(steamID)) return true;
            return false;
        }

        /// <summary>
        /// Check to see if given steamID is valid
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns></returns>
        public bool IsValidSteamID(long steamID)
        {
            //TODO: Add steamkit dll and verify
            if (steamID.ToString().Length != 17) return false;

            return true;
            
        }

        /// <summary>
        /// Checks if given string steamID is banned
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns>bool</returns>
        public bool IsSteamIDBattlEyeBanned(string steamID) => (BEGuid.CheckSteamID(BEPort.Arma3, steamID) != "Clean");

        /// <summary>
        /// Checks if given steamID is banned
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns>bool</returns>
        public bool IsSteamIDBattlEyeBanned(long steamID) => IsSteamIDBattlEyeBanned(steamID.ToString());
 
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
        /// Capitalize first letter of string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("ARGH!");
            }
            return input.First().ToString().ToUpper(new CultureInfo("en-UK", false)) + string.Join("", input.Skip(1));
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
        public bool StringContainsChar(string text) => text.Any(x => char.IsLetter(x));
       
        /// <summary>
        /// Checks to see if string contains a number
        /// </summary>
        /// <param name="text"></param>
        /// <returns>bool</returns>
        public bool StringContainsNumber(string text) => new Regex("^[0-9]*$").IsMatch(text);
        
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

        /// <summary>
        /// Deletes all file inside directory then attempt to delete directory
        /// </summary>
        /// <param name="target_dir"></param>
        internal static void DeleteDirectory(string target_dir)
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
         
        /// <summary>
        /// Create new line(s)
        /// </summary>
        /// <param name="numOfLines"></param>
        /// <returns>string of new line(s)</returns>
        internal static string NewLine(int numOfLines = 1)
        {
            var newLines = "";
            for (var i = 0; i < numOfLines; i++)
            {
                newLines += Environment.NewLine;
            }
            return newLines;
        }
        
        /// <summary>
        /// Create new tab(s)
        /// </summary>
        /// <param name="numOfTabs"></param>
        /// <returns>string of new tab(s)</returns>
        internal static string NewTab(int numOfTabs = 1)
        {
            var newLines = "";
            for (var i = 0; i < numOfTabs; i++)
            {
                newLines += "\t";
            }
            return newLines;
        }

        /// <summary>
        /// Creates Random string of given characters & length
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static string CreateRandomString(string characters, int length) => new string(Enumerable.Repeat(characters, length).Select(s => s[random.Next(s.Length)]).ToArray());

        /// <summary>
        /// Creates Random string of given length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string CreateRandomString(int length) => CreateRandomString(chars, length);

        /// <summary>
        /// Creates Random Variable of given length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string CreateRandomVariable(int length)
        {
            string variable = CreateRandomString(1);
            variable += CreateRandomString(string.Format("{0}_0123456789", chars), length);
            if (variable.EndsWith("_")) variable = variable.TrimEnd('_');
            return variable;
        }

        /// <summary>
        /// Remove Comments form given file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        internal static string RemoveComments(string file)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            return Regex.Replace(file, blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings, line => {
                if (line.Value.StartsWith("/*") || line.Value.StartsWith("//")) return line.Value.StartsWith("//") ? NewLine() : "";
                return line.Value;// Keep the literal strings
            }, RegexOptions.Singleline);
        }

        /// <summary>
        /// Convert given file to one line
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        internal static string OneLine(string file) => RemoveComments(file).Replace("\n", " ").Replace("\r", "");

        /// <summary>
        /// Downloads PBO from a gitserver
        /// </summary>
        /// <param name="pbo"></param>
        /// <returns>bool</returns>
        internal static bool DownloadPBO(PBOFile pbo)
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
                        throw new Exception("Unknown git type! 0 = GitHub, 1 = GitLab");
                    }
                    webClient.DownloadFile(pbo.GitUrl, gitPath);
                }

                if (Directory.Exists(Path.Combine(DLL.ConfigValues.GitDirectory, pbo.GitBranch))) Directory.Delete(DLL.ConfigValues.GitDirectory, true);
                Directory.CreateDirectory(DLL.ConfigValues.GitDirectory);

                if (pbo.GitServer == GitServer.GitLab)
                {
                    // very dirty hack for gitlab stupid file bullshit
                    using (ZipArchive archive = ZipFile.OpenRead(gitPath))
                    {
                        string folderName = archive.Entries[0].FullName;
                        foreach (ZipArchiveEntry entry in archive.Entries.Skip(1))
                        {
                            string name = DLL.ConfigValues.GitDirectory + "/" + entry.FullName.Replace(folderName, "");
                            if (entry.FullName.EndsWith("/")) Directory.CreateDirectory(name); else entry.ExtractToFile(name);
                        }
                    }
                    return true;
                }

                ZipFile.ExtractToDirectory(gitPath, DLL.ConfigValues.GitDirectory);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in download: " + ex);
                return false;
            }
        }

        /// <summary>
        /// Changes Variable in given string from a dictionary
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="vars"></param>
        /// <returns>new contents</returns>
        private static string RenameVariables(string contents, Dictionary<string, string> vars)
        {
            foreach (KeyValuePair<string, string> var in vars)  contents = Regex.Replace(contents, string.Format(@"\b{0}\b", Regex.Escape(var.Key)), var.Value, RegexOptions.Multiline | RegexOptions.IgnoreCase);//Relpace var in file with random var
            return contents;
        }

        /// <summary>
        /// Changes Function name in given string from a dictionary
        /// </summary>
        /// <param name="file"></param>
        /// <param name="outName"></param>
        /// <param name="contents"></param>
        /// <param name="vars"></param>
        /// <returns>new Dictionary [outName], [contents]</returns>
        private static string RenameVariables(string file, out string outName, string contents, Dictionary<string, string> vars)
        {
            outName = file;

            foreach (KeyValuePair<string, string> var in vars)
            { 
                contents = contents.Replace(var.Key, var.Value);//Relpace function name in file with random var
                if (file.Contains(var.Key)) outName = file.Replace(var.Key, var.Value);//Change file name of function to random var 
            };

            return contents;
        }

        /// <summary>
        /// Check to see if file is a specifc extension
        /// </summary>
        /// <param name="file"></param>
        /// <param name="extension"></param>
        /// <returns>bool</returns>
        internal static bool IsExtension(string file, string extension) => (file.EndsWith($".{extension.Replace(".", "")}"));

        /// <summary>
        /// Check to see if file is in a list of extensions
        /// </summary>
        /// <param name="file"></param>
        /// <param name="extensions"></param>
        /// <returns>bool</returns>
        internal static bool IsExtension(string file, List<string> extensions)
        {
            foreach (string extension in extensions) if (IsExtension(file, extension)) return true;
            return false;
        }

        /// <summary>
        /// Randomizes PBO variables and functions
        /// </summary>
        /// <param name="pbo"></param>
        internal static void RandomizePBO(PBOFile pbo)
        {
            string folderPath = Path.Combine(DLL.ConfigValues.GitDirectory, pbo.GitBranch);
            List<string> badExtensions = new List<string>() {
                "ogg","paa","jpg","png","p3d"
            };

            Console.WriteLine($"Begining {pbo.Name} obfuscation");

            foreach (string file in (Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)))
            { 
                if (IsExtension(file, badExtensions)) continue;
                
                string outName = file;
                string contents = File.ReadAllText(outName);

                if (pbo.RandomizeGlobalVariables) contents = RenameVariables(contents, DLL._globalVars);
                if (pbo.RandomizeLocalVariables) contents = RenameVariables(contents, DLL._localVars);
                if (pbo.RandomizeFunctions) contents = RenameVariables(file, out outName, contents, DLL._scriptFuncs);
                if (pbo.SingleLineFunctions && file.EndsWith(".sqf")) contents = OneLine(contents);

                File.WriteAllText(file, contents);
                if (file != outName)
                {
                    Console.WriteLine($"Filename Updated: {file} => {outName}");
                    File.Move(file, outName);
                }
            }

            Console.WriteLine($"{pbo.Name} obfuscated.");
        }
         
        /// <summary>
        /// Pack a folder into .bpo format
        /// </summary>
        /// <param name="pbo"></param>
        internal static void PackPBO(PBOFile pbo)
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

        /// <summary>
        /// Moves a PBO to new location
        /// </summary>
        /// <param name="pbo"></param>
        /// <returns></returns>
        internal static bool MovePBO(PBOFile pbo)
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

    }
}
