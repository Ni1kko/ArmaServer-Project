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
    /// <summary>
    /// Helper methods
    /// </summary>
    public class Helpers
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstvwxyz";
        private static readonly Random random = new Random();

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
        ///  Check if IPAdresses
        /// </summary>
        /// <param name="IPIn"></param>
        /// <param name="IPOut"></param>
        /// <param name="defaultOnBadParse"></param>
        /// <returns></returns>
        public bool IsValidIP(string IPIn, out string IPOut, bool defaultOnBadParse = false)
        {
            IPAddress IPParse;
            if (IPAddress.TryParse(IPIn, out IPParse))
            {
                IPOut = IPParse.ToString();
                return true;
            }

            IPOut = defaultOnBadParse ? "192.168.0.1" : "";
            MessageBox.Show("Invalid IP Address");
            return false;
        }

        /// <summary>
        /// Checks if given processID is running
        /// </summary>
        /// <param name="procID"></param>
        /// <returns></returns>
        public bool IsProcessRunning(int procID)
        {
            try { Process.GetProcessById(procID); }
            catch (InvalidOperationException) { return false; }
            catch (ArgumentException) { return false; }
            return true;
        }

        /// <summary>
        /// Get current HardwareID
        /// </summary>
        /// <returns></returns>
        public static string GetMachineGuid(out Exception exception)
        {
            try
            {
                exception = null;
                using RegistryKey localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                using RegistryKey rk = localMachineX64View.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography");
                if (rk == null) throw new KeyNotFoundException(string.Format("Key Not Found: {0}", @"SOFTWARE\Microsoft\Cryptography"));
                object machineGuid = rk.GetValue("MachineGuid"); if (machineGuid == null) throw new IndexOutOfRangeException(string.Format("Index Not Found: {0}", "MachineGuid"));
                return machineGuid.ToString();
            } 
            catch (KeyNotFoundException ex)
            {
                exception = ex;
            }
            catch (IndexOutOfRangeException ex)
            {
                exception = ex;
            }
            catch (Exception ex)
            {
                exception = ex; 
            }
            return "";
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
            };
        }

        /// <summary>
        /// Opens Folder File Dialog and returns selected P3D
        /// </summary>
        /// <returns></returns>
        public string GetP3DPathDialog()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "[binarized P3D's] | *.p3d";
                if (openFileDialog.ShowDialog() != DialogResult.OK) return ""; 
                return openFileDialog.FileName;
            }; 
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
        public List<string> RemoveListBoxValue(ListBox listBox, int SelectedItem = -2)
        { 
            if (SelectedItem == -2) SelectedItem = listBox.SelectedIndex;
            if (SelectedItem == -1) return new List<string>();
            listBox.Items.RemoveAt(SelectedItem);
            List<string> InBox = new List<string>();
            foreach (string item in listBox.Items) InBox.Add(item);
            return InBox;
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

        public static string NewEscape(EscapeType escape) => escape switch
        {
            EscapeType.Nothing => "",
            EscapeType.DoubleQuotationMark => "\"",
            EscapeType.SingleQuotationMark => "\'",
            EscapeType.Newline => "\n",
            EscapeType.CarriageReturn => "\r",
            EscapeType.HorizontalTab => "\t",
            EscapeType.VerticalTab => "\v",
            EscapeType.Backslash => "\\",
            _ => NewEscape(EscapeType.DoubleQuotationMark),
        };

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
                newLines += NewEscape(EscapeType.Newline);
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
                newLines += NewEscape(EscapeType.HorizontalTab);
            }
            return newLines;
        }

        /// <summary>
        /// Create new space(s)
        /// </summary>
        /// <param name="numOfSpaces"></param>
        /// <returns>string of new tab(s)</returns>
        internal static string NewSpace(int numOfSpaces = 1) => new string(' ', numOfSpaces);

        /// <summary>
        /// Add new option
        /// </summary>
        /// <param name="option"></param>
        /// <param name="value"></param>
        /// <param name="escape"></param>
        /// <param name="numofescape"></param>
        /// <returns></returns>
        internal string NewOption(string option, string value, EscapeType escape = EscapeType.Nothing, int numofescape = 1)
        {
            if (escape != EscapeType.Nothing)
            {
                var escapeString = "";
                for (var i = 0; i < numofescape; i++) escapeString += NewEscape(escape);
                value = escapeString + value + escapeString;
            };
            option += " = " + value + ";" + NewEscape(EscapeType.Newline);
            return option;
        }

        /// <summary> 
        /// Initializes a line break 
        /// </summary> 
        /// <param name="text">Text to initiate the line break</param> 
        /// <param name="pos">Position to break the line at</param> 
        /// <param name="max">Maximum word to break line at</param> 
        /// <returns>Int32</returns> 
        private static int BreakLine(string text, int pos, int max)
        {
            int i = max;
            while (i >= 0 && !char.IsWhiteSpace(text[pos + i])) i--;
            if (i < 0) return max;
            while (i >= 0 && char.IsWhiteSpace(text[pos + i])) i--;
            return i + 1;
        } 

        /// <summary> 
        /// Normalizes text 
        /// </summary> 
        /// <param name="text">Text to normalize</param> 
        /// <returns>System.String</returns> 
        public static string NormalizeText(string text)
        {
            var wrappedText = WrapText(text);
            string[] lines = wrappedText.Split(new string[] { "\n" }, StringSplitOptions.None);
            var maxlen = lines[0].Length;

            for (int i = 0; i < lines.Length; i++)
            {
                var count = lines[i].Split(new string[] { "" }, StringSplitOptions.None).Length;
                var spaceCount = maxlen - count;
                var append = string.Empty;
                for (var j = 0; j < spaceCount; j++) append += " ";
                lines[i] = lines[i] + append;
            }

            var sb = new StringBuilder();
            foreach (var line in lines) sb.Append(line + '\n');
            return sb.ToString();
        }

        /// <summary> 
        /// Performs a word wrap for text 
        /// </summary> 
        /// <param name="text">The text to perform the wrap on</param> 
        /// <param name="wrapSize">Word count to start wrapping at</param> 
        /// <returns>string</returns> 
        public static string WrapText(string text, int wrapSize = 40)
        {
            int pos, next = 0;
            var sb = new StringBuilder();
            if (wrapSize < 1) return text;

            for (pos = 0; pos < text.Length; pos = next)
            {
                int eol = text.IndexOf(NewLine(), pos);
                next = (eol == -1) ? eol = text.Length : eol + NewLine().Length;

                if (eol > pos) do
                {
                    int len = eol - pos;
                    if (len > wrapSize) len = BreakLine(text, pos, wrapSize);

                    sb.Append(text, pos, len);
                    sb.Append(NewLine());
                    pos += len;

                    while (pos < eol && char.IsWhiteSpace(text[pos])) pos++;
                } while (eol > pos);
                else sb.Append(NewLine());
            };
            return sb.ToString();
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
        internal static string OneLine(string file) => RemoveComments(file).Replace(NewEscape(EscapeType.Newline), NewSpace()).Replace(NewEscape(EscapeType.CarriageReturn), "");

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
        /// Downloads PBO from a gitserver
        /// </summary>
        /// <param name="pbo"></param>
        /// <returns>bool</returns>
        internal static bool DownloadPBO(PBOFile pbo, out Exception exception)
        { 
            try
            {
                exception = null;
                string token = pbo.GitToken.Replace("token ", "");
                string gitPath = Path.Combine(DLL.ConfigValues.GitDirectory + "/git.zip");

                if (!Directory.Exists(DLL.ConfigValues.GitDirectory)) Directory.CreateDirectory(DLL.ConfigValues.GitDirectory);

                using (var webClient = new WebClient())
                {
                    if (pbo.GitServer == GitServer.GitHub) webClient.Headers.Add("Authorization", "token " + token);
                    else if (pbo.GitServer == GitServer.GitLab) webClient.Headers.Add("Private-Token", token); 
                    else throw new Exception("Unknown GitServer");
                    webClient.DownloadFile(pbo.GitUrl, gitPath);
                };

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
                exception = ex;
                return false;
            }
        }
        internal static bool DownloadPBO(PBOFile pbo) => DownloadPBO(pbo, out _);

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
        /// Randomizes PBO variables and functions
        /// </summary>
        /// <param name="pbo"></param>
        /// <returns>bool</returns>
        internal static bool RandomizePBO(PBOFile pbo, out Exception exception)
        {
            exception = null;
            List<string> badExtensions = new List<string>()
            {
                "ogg","paa","jpg","png","p3d","wav","tga","dds","rvmat","ods","fxy","lip","csv","kb","bik","bikb","html","htm","biedi"
            };

            try
            {
                string folderPath = Path.Combine(DLL.ConfigValues.GitDirectory, pbo.GitBranch);
                
                Console.WriteLine($"Begining {pbo.Name} obfuscation");

                foreach (string file in (Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories)))
                {
                    if (IsExtension(file, badExtensions)) continue;

                    string outName = file;
                    string contents = File.ReadAllText(outName);

                    if (pbo.RandomizeGlobalVariables) contents = RenameVariables(contents, DLL._globalVars);
                    if (pbo.RandomizeLocalVariables) contents = RenameVariables(contents, DLL._localVars);
                    if (pbo.RandomizeFunctions) contents = RenameVariables(file, out outName, contents, DLL._scriptFuncs);
                    if (pbo.SingleLineFunctions && IsExtension(file, "sqf")) contents = OneLine(contents);

                    File.WriteAllText(file, contents);
                    if (file == outName) continue;//Move to next file

                    File.Move(file, outName); //Change file name
                }

                Console.WriteLine($"{pbo.Name} obfuscated.");
                return true;
            }
            catch (IOException ex)
            {
                exception = ex;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return false;
        }
        internal static bool RandomizePBO(PBOFile pbo) => RandomizePBO(pbo, out _);
        
        /// <summary>
        /// Pack a folder into .pbo format
        /// </summary>
        /// <param name="pbo"></param>
        /// <returns>bool</returns>
        internal static bool PackPBO(PBOFile pbo, out Exception exception)
        { 
            try
            {
                exception = null;
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

                string modDirectory = modPath + ".pbo";

                pboFile.Save(modDirectory);
                return File.Exists(modDirectory);
            }
            catch (IOException ex)
            {
                exception = ex;
                return false;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }
        internal static bool PackPBO(PBOFile pbo) => PackPBO(pbo, out _);
        
        /// <summary>
        /// Moves a PBO to new location
        /// </summary>
        /// <param name="pbo"></param>
        /// <returns></returns>
        internal static bool MovePBO(PBOFile pbo, out Exception exception)
        {
            string modPath = Path.Combine(DLL.ConfigValues.GitDirectory, pbo.Name) + ".pbo";
            string serverPath = Path.Combine(pbo.ServerPath, pbo.Name) + ".pbo";
            if (pbo.ModType != PboModType.Mission) serverPath = Path.Combine(pbo.ServerPath, "addons", pbo.Name) + ".pbo";

            try
            {
                exception = null;
                File.Copy(modPath, serverPath, true);
                Console.WriteLine($"Moved ({pbo.Name}): {modPath} => {serverPath}");
                return File.Exists(serverPath);
            }
            catch (IOException ex)
            {
                exception = ex;
                return false;
            }
        }
        internal static bool MovePBO(PBOFile pbo) => MovePBO(pbo, out _);

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
        /// Coppy array of file to new location
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="fileArray"></param>
        private bool CopyFiles(string targetPath, string[] fileArray, out Exception exception)
        {
            exception = null;
            bool didCopy = true;
            try
            {
                foreach (var file in fileArray)
                {
                    if (!File.Exists(file))
                    {
                        didCopy = false;
                        continue;
                    }
                    var target = Path.Combine(targetPath, file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1));
                    File.Copy(file, target, true);
                    didCopy = File.Exists(target);
                    if (!didCopy) return false;
                }
            }
            catch (IOException ex)
            {
                exception = ex;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return (exception == null) ? didCopy : false;
        }

        /// <summary>
        /// Write Configs
        /// </summary>
        /// <param name="file"></param> 
        internal virtual void WriteFile(string file, string conetnts) => File.WriteAllText(file, conetnts, Encoding.Unicode);
        
        /// <summary>
        /// Gets a list of all RPT files
        /// </summary>
        /// <returns>List of RPT's</returns>
        private List<string> GetAllRPTFiles()
        {
            var path = Path.Combine(DLL.ConfigValues.serverSettings.ServerDirectory, "A3Config");
            List<string> RPTFiles = new DirectoryInfo(path).GetFiles("*.rpt").Select(
                info => Path.Combine(path, info.Name) 
            ).ToList();
            if (RPTFiles.Count < 1) return new List<string>();
            RPTFiles.Sort();
            return RPTFiles;
        } 

        /// <summary>
        /// Returns latest RPT file
        /// </summary>
        /// <returns>filePath</returns>
        private string GetLatestRPT()
        {
            List<string> RPTFiles = GetAllRPTFiles();
            if (RPTFiles.Count < 1) return "";
            string file = RPTFiles[RPTFiles.Count - 1];
            if (string.IsNullOrEmpty(file) || !File.Exists(file)) return "";
            return file;
        }

        /// <summary>
        /// Deletes latest RPT file
        /// </summary>
        /// <returns>true/false</returns>
        public bool DeleteLatestRPT()
        {
            string file = GetLatestRPT();
            if (string.IsNullOrEmpty(file)) return false;
            File.Delete(file);
            return File.Exists(file) ? false : true;
        }

        /// <summary>
        /// Deletes older RPT files
        /// </summary>
        /// <returns>true/false</returns>
        public bool DeleteOlderRPTFiles()
        {
            List<string> RPTFiles = GetAllRPTFiles();
            if (RPTFiles.Count < 1) return false;
            foreach (string filePath in RPTFiles) if (filePath != RPTFiles[RPTFiles.Count - 1]) File.Delete(filePath);
            return true;
        }

        /// <summary>
        /// Deletes All RPT files
        /// </summary>
        /// <returns>true/false</returns>
        public bool DeleteAllRPTFiles()
        {
            List<string> RPTFiles = GetAllRPTFiles();
            if (RPTFiles.Count < 1) return false;
            foreach (string filePath in RPTFiles) File.Delete(filePath);
            return true;
        }

        /// <summary>
        /// Opens RPT file in registered code editor
        /// </summary>
        /// <returns></returns>
        public bool OpenLatestRPT()
        {
            string file = GetLatestRPT();
            if (string.IsNullOrEmpty(file)) return false;
            Process.Start(file);
            return true;
        }
    }
}
