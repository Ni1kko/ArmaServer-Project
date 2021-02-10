using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArmaServerBackend;

namespace ArmaServerFrontend
{
    internal partial class Home : Form
    {
        private bool preloaded = false;

        public Home() {
            InitializeComponent();
            PreLoadHomeTabWindow();
            PreLoadServerTabWindow();
            preloaded = true;
        }
         
        ///////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Home TAB
        /// </summary> 
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void PreLoadHomeTabWindow()
        { 
            GitDirectoryPathBox.Text = DLL.ConfigValues.GitDirectory;
            
            //Randomize
            FncTagBox.Text = DLL.ConfigValues.FunctionsTag;
            FncLengthCombo.SelectedIndex = (DLL.ConfigValues.RandomFunctionsLength - 1);
            VarLengthCombo.SelectedIndex = (DLL.ConfigValues.RandomVariablesLength - 1);
            DLL.HelperFunctions.AddListBoxValue(FunctionsListBox, DLL.ConfigValues.Functions);
            DLL.HelperFunctions.AddListBoxValue(GlobalVariablesListBox, DLL.ConfigValues.GlobalVariables);
            DLL.HelperFunctions.AddListBoxValue(LocalVariablesListBox, DLL.ConfigValues.LocalVaribales);

            //Pbos
            int pboListCount = 0;
            foreach (PboFiles pbo in DLL.ConfigValues.Pbos)
            {
                NewPboTab(pbo);
                if (pboListCount == 0) LoadPboContols(pbo);
                pboListCount += 1;
            }
            PboFileBox.TabPages.Remove(PboStartTab);
        }

        /// GitDirectory
        private void BrowseGitDirectory_Click(object sender, EventArgs e) => GitDirectoryPathBox.Text = DLL.HelperFunctions.GetFolderPathDialog();
        private void GitDirectoryPathBox_TextChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.GitDirectory = GitDirectoryPathBox.Text;
                DLL.ConfigFunctions.Save();
            }
        }
         
        /// Randomize
        private void FncTagBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.FunctionsTag = FncTagBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void FncLengthCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.RandomFunctionsLength = (FncLengthCombo.SelectedIndex + 1);
                DLL.ConfigFunctions.Save();
            }
        }
        private void VarLengthCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.RandomVariablesLength = (VarLengthCombo.SelectedIndex + 1);
                DLL.ConfigFunctions.Save();
            }
        }
        private void FunctionsListAddButton_Click(object sender, EventArgs e)
        { 
            if (FunctionsListAddBox.Text == "") return; 
            DLL.ConfigValues.Functions = DLL.HelperFunctions.AddListBoxValue(FunctionsListBox, FunctionsListAddBox);
            DLL.ConfigFunctions.Save();
        }
        private void RemoveSelectedFunctionButton_Click(object sender, EventArgs e)
        {
            DLL.ConfigValues.Functions = DLL.HelperFunctions.RemoveListBoxValue(FunctionsListBox);
            DLL.ConfigFunctions.Save();
        }
        private void GlobalVariablesListAddButton_Click(object sender, EventArgs e)
        { 
            if (GlobalVariablesListAddBox.Text == "") return; 
            DLL.ConfigValues.GlobalVariables = DLL.HelperFunctions.AddListBoxValue(GlobalVariablesListBox, GlobalVariablesListAddBox);
            DLL.ConfigFunctions.Save();
        }
        private void GlobalRemoveSelectedVariableButton_Click(object sender, EventArgs e)
        { 
            DLL.ConfigValues.GlobalVariables = DLL.HelperFunctions.RemoveListBoxValue(GlobalVariablesListBox);
            DLL.ConfigFunctions.Save();
        }
        private void LocalVariablesListAddButton_Click(object sender, EventArgs e)
        { 
            if (LocalVariablesListAddBox.Text == "") return;
            DLL.ConfigValues.LocalVaribales = DLL.HelperFunctions.AddListBoxValue(LocalVariablesListBox, LocalVariablesListAddBox);
            DLL.ConfigFunctions.Save();
        }
        private void LocalRemoveSelectedVariableButton_Click(object sender, EventArgs e)
        { 
            DLL.ConfigValues.LocalVaribales = DLL.HelperFunctions.RemoveListBoxValue(LocalVariablesListBox);
            DLL.ConfigFunctions.Save();
        }

        /// PBO setup
        private int lastSelectedPbo = 0;
        private Dictionary<int, PboFiles> pboList = new Dictionary<int, PboFiles>();
        private void NewPboTab(PboFiles newPboValues)
        {
            List<PboFiles> PboFilesUpdated = new List<PboFiles>();
            int pboListCount = 0;

            if (PboFileBox.TabCount > 4)
            {
                MessageBox.Show("5 PBO's Max");
                return;
            }

            foreach (KeyValuePair<int, PboFiles> pbo in pboList.ToList())
            { 
                pboListCount += 1;
                PboFilesUpdated.Add(pbo.Value);
            }

            TabPage NewPboTab = new TabPage(newPboValues.Name);//add tab
            PboFileBox.TabPages.Add(NewPboTab);//Add Tab Name 
            PboFilesUpdated.Add(newPboValues);//add new list
            pboList.Add(pboListCount, newPboValues);//add to dictonary
            
            DLL.ConfigValues.Pbos = PboFilesUpdated;
            DLL.ConfigFunctions.Save();
        }
        private void LoadPboContols(PboFiles pboValues)
        {
            if (!preloaded) GitTypeCombo.SelectedIndex += 1;
            PboNameBox.Text = pboValues.Name;
            GitPathBox.Text = pboValues.GitBranch;
            GitUrlBox.Text = pboValues.GitUrl;
            GitTokenBox.Text = pboValues.GitToken;
            GitTypeCombo.SelectedIndex = (int)Enum.ToObject(typeof(GitServer), GitTypeCombo.SelectedIndex);
            PboServerPathBox.Text = pboValues.ServerPath;
            OneLineButton.Checked = pboValues.SingleLineFunctions;
            RenameFuncsButton.Checked = pboValues.RandomizeFunctions;
            RenameGlobalVarsButton.Checked = pboValues.RandomizeGlobalVariables;
            RenameLocalVarsButton.Checked = pboValues.RandomizeLocalVariables;
        }
        private void UpdatePbos()
        {
            if (!preloaded) return;
            List<PboFiles> PboFilesUpdated = new List<PboFiles>();

            foreach (KeyValuePair<int, PboFiles> pbo in pboList.ToList())
            {
                if (PboFileBox.SelectedIndex == pbo.Key)
                {
                    PboFileBox.TabPages[pbo.Key].Text = PboNameBox.Text;

                    PboFiles PboFileUpdated = new PboFiles()
                    {
                        Name = PboNameBox.Text,
                        GitBranch = GitPathBox.Text,
                        GitUrl = GitUrlBox.Text,
                        GitToken = GitTokenBox.Text,
                        GitServer = (GitServer)Enum.ToObject(typeof(GitServer), (GitTypeCombo.SelectedIndex + 1)),
                        ServerPath = PboServerPathBox.Text,
                        SingleLineFunctions = OneLineButton.Checked,
                        RandomizeFunctions = RenameFuncsButton.Checked,
                        RandomizeGlobalVariables = RenameGlobalVarsButton.Checked,
                        RandomizeLocalVariables = RenameLocalVarsButton.Checked
                    };

                    pboList[pbo.Key] = PboFileUpdated;
                }
                PboFilesUpdated.Add(pboList[pbo.Key]);
            }

            DLL.ConfigValues.Pbos = PboFilesUpdated;
            DLL.ConfigFunctions.Save();

        }
        private void AddPboTabButton_Click(object sender, EventArgs e) => NewPboTab(new PboFilesDefault().Values("Your_PBO_Name_Here", "C:\\Arma3\\@SomeAddon\\addons\\Your_PBO_Name_Here.pbo"));
        private void RemovePboButton_Click(object sender, EventArgs e)
        {
            List<PboFiles> PboFilesUpdated = new List<PboFiles>();

            foreach (KeyValuePair<int, PboFiles> pbo in pboList.ToList())
            {
                if (PboFileBox.SelectedIndex != pbo.Key)
                { 
                    PboFilesUpdated.Add(pboList[pbo.Key]);
                }
            }

            pboList.Remove(PboFileBox.SelectedIndex);
            PboFileBox.TabPages.Remove(PboFileBox.TabPages[PboFileBox.SelectedIndex]);
            //PboFileBox.SelectedIndex = 0;
            //LoadPboContols(pboList[PboFileBox.SelectedIndex]);

            DLL.ConfigValues.Pbos = PboFilesUpdated;
            DLL.ConfigFunctions.Save();
        }
        private void PboFileBox_Click(object sender, EventArgs e)
        {
            if (!preloaded) return;
            if (lastSelectedPbo == PboFileBox.SelectedIndex) return;

            lastSelectedPbo = PboFileBox.SelectedIndex;
            foreach (KeyValuePair<int, PboFiles> pbo in pboList.ToList())
            {
                if (PboFileBox.SelectedIndex == pbo.Key)
                {
                    LoadPboContols(pbo.Value);
                }
            }

        }
        private void PboNameBox_TextChanged(object sender, EventArgs e) => UpdatePbos();
        private void GitPathBox_TextChanged(object sender, EventArgs e) => UpdatePbos();
        private void PboServerPathButton_Click(object sender, EventArgs e) => PboServerPathBox.Text = DLL.HelperFunctions.GetFolderPathDialog();
        private void PboServerPathBox_TextChanged(object sender, EventArgs e) => UpdatePbos();
        private void GitUrlBox_TextChanged(object sender, EventArgs e) => UpdatePbos();
        private void GitTokenBox_TextChanged(object sender, EventArgs e) => UpdatePbos();
        private void GitTypeCombo_SelectedValueChanged(object sender, EventArgs e) => UpdatePbos();
        private void OneLineButton_CheckedChanged(object sender, EventArgs e) => UpdatePbos();
        private void RenameFuncsButton_CheckedChanged(object sender, EventArgs e) => UpdatePbos();
        private void RenameGlobalVarsButton_CheckedChanged(object sender, EventArgs e) => UpdatePbos();
        private void RenameLocalVarsButton_CheckedChanged(object sender, EventArgs e) => UpdatePbos();

        /// Launch
        private void LaunchButton_Click(object sender, EventArgs e) => Program.BackendDLL.LaunchServer();

        ///////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Server TAB
        /// </summary> 
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void PreLoadServerTabWindow()
        {
            ServerDirectoryPathBox.Text = DLL.ConfigValues.serverSettings.ServerDirectory;
            Use64BitArmaButton.Checked = DLL.ConfigValues.serverSettings.X64Architecture;
            HostNameBox.Text = DLL.ConfigValues.serverSettings.hostName;
            ServerPasswordBox.Text = DLL.ConfigValues.serverSettings.password;
            AdminPasswordBox.Text = DLL.ConfigValues.serverSettings.passwordAdmin;
            ServerCMDPasswordBox.Text = DLL.ConfigValues.serverSettings.serverCommandPassword;
            ServerLogBox.Text = DLL.ConfigValues.serverSettings.logFile;
            DLL.HelperFunctions.AddListBoxValue(MotdBox, DLL.ConfigValues.serverSettings.motd);
            MotdIntervalBox.Text = DLL.ConfigValues.serverSettings.motdInterval.ToString();
        }

        // ServerDirectory
        private void BrowseServerDirectory_Click(object sender, EventArgs e) => ServerDirectoryPathBox.Text = DLL.HelperFunctions.GetFolderPathDialog();
        private void ServerDirectoryPathBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.ServerDirectory = ServerDirectoryPathBox.Text;
            DLL.ConfigFunctions.Save();
        }

        //64Bit arma process
        private void Use64BitArmaButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.X64Architecture = Use64BitArmaButton.Checked;
            DLL.ConfigFunctions.Save();
        }

        //HostName
        private void HostNameBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.hostName = HostNameBox.Text;
            DLL.ConfigFunctions.Save();
        }
        
        //ServerPassword 
        private void ServerPasswordBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.password = ServerPasswordBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void GenerateServerPasswordButton_Click(object sender, EventArgs e) => ServerPasswordBox.Text = DLL.HelperFunctions.RandomString(5);

        //AdminPassword
        private void AdminPasswordBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.passwordAdmin = AdminPasswordBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void GenerateAdminPasswordButton_Click(object sender, EventArgs e) => AdminPasswordBox.Text = DLL.HelperFunctions.RandomString(7);

        //Command password
        private void ServerCMDPasswordBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.serverCommandPassword = ServerCMDPasswordBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void GenerateCmdPasswordButton_Click(object sender, EventArgs e) => ServerCMDPasswordBox.Text = DLL.HelperFunctions.RandomVariable(8);
    
        //console log
        private void ServerLogBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.logFile = ServerLogBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void DefaultConsoleLogButton_Click(object sender, EventArgs e) => ServerLogBox.Text = ServerSettingsDefault.consoleLogFile;
    
        //Message of the day
        private void MotdAddButton_Click(object sender, EventArgs e)
        { 
            if (MotdAddBox.Text == "") return;
            DLL.ConfigValues.serverSettings.motd = DLL.HelperFunctions.AddListBoxValue(MotdBox, MotdAddBox);
            DLL.ConfigFunctions.Save();
        } 
        private void MotdRemoveButton_Click(object sender, EventArgs e)
        { 
            DLL.ConfigValues.serverSettings.motd = DLL.HelperFunctions.RemoveListBoxValue(MotdBox);
            DLL.ConfigFunctions.Save();
        }
        private void MotdIntervalBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;

            //empty string, reset and update
            if (MotdIntervalBox.Text == "")
            {
                MotdIntervalBox.Text = "0"; 
                DLL.ConfigValues.serverSettings.motdInterval = 0;
                DLL.ConfigFunctions.Save();
                return;
            }
            
            string oldValue = DLL.ConfigValues.serverSettings.motdInterval.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(MotdIntervalBox.Text))
            {
                MotdIntervalBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            int motdInterval = int.Parse(MotdIntervalBox.Text);//convert to int
            int maxInterval = 999999;

            //Nope not that large
            if (motdInterval > maxInterval)
            {
                MotdIntervalBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {maxInterval}!");
                return;
            } 

            //update
            DLL.ConfigValues.serverSettings.motdInterval = motdInterval;
            DLL.ConfigFunctions.Save();
        }

        



        //

    }
}
