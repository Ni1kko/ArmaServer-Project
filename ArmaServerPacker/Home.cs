using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArmaServerBackend;

namespace ArmaServerFrontend
{
    public partial class Home : Form
    {
        bool preloaded = false;

        public Home() => PreLoadWindow();

        private void PreLoadWindow()
        {
            InitializeComponent();
            ServerDirectoryPathBox.Text = DLL.ConfigValues.ServerDirectory;
            GitDirectoryPathBox.Text = DLL.ConfigValues.GitDirectory;
            FncTagBox.Text = DLL.ConfigValues.FunctionsTag;
            KillArmaCheckBox.Checked = DLL.ConfigValues.KillArmaServer;
            Use64BitArmaButton.Checked = DLL.ConfigValues.Use64BitServer;

            //Randomize
            FncLengthCombo.SelectedIndex = (DLL.ConfigValues.RandomFuncsLength - 1);
            VarLengthCombo.SelectedIndex = (DLL.ConfigValues.RandomVarsLength - 1);
            LoadRandomListBoxes(0, DLL.ConfigValues.ObfFunctions);
            LoadRandomListBoxes(1, DLL.ConfigValues.ObfGlobalVars);
            LoadRandomListBoxes(2, DLL.ConfigValues.ObfLocalVars);

            //Pbos
            int pboListCount = 0;
            foreach (PboFiles pbo in DLL.ConfigValues.Pbos)
            {
                NewPboTab(pbo);
                if (pboListCount == 0) LoadPboContols(pbo);
                pboListCount += 1;
            }
            PboFileBox.TabPages.Remove(PboStartTab);

            preloaded = true;
        }

        /// <summary>
        /// ServerDirectory
        /// </summary>
        private void BrowseServerDirectory_Click(object sender, EventArgs e) => ServerDirectoryPathBox.Text = DLL.HelperFunctions.GetFolderPathDialog();
        private void ServerDirectoryPathBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return; 
            DLL.ConfigValues.ServerDirectory = ServerDirectoryPathBox.Text;
            DLL.ConfigFunctions.Save();
        }

        /// <summary>
        /// GitDirectory
        /// </summary>
        private void BrowseGitDirectory_Click(object sender, EventArgs e) => GitDirectoryPathBox.Text = DLL.HelperFunctions.GetFolderPathDialog();
        private void GitDirectoryPathBox_TextChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.GitDirectory = GitDirectoryPathBox.Text;
                DLL.ConfigFunctions.Save();
            }
        }

        /// <summary>
        /// Stop arma process
        /// </summary>
        private void KillArmaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.KillArmaServer = KillArmaCheckBox.Checked;
                DLL.ConfigFunctions.Save();
            }
        }

        /// <summary>
        /// 64/32 Bit arma process
        /// </summary>
        private void Use64BitArmaButton_CheckedChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.Use64BitServer = Use64BitArmaButton.Checked;
                DLL.ConfigFunctions.Save();
            }
        }

        /// <summary>
        /// Randomize
        /// </summary>
        private void LoadRandomListBoxes(int box, List<string> vars)
        {
            foreach (string var in vars)
            {
                switch (box)
                {
                    case 0: FunctionsListBox.Items.Add(var); break;
                    case 1: GlobalVariablesListBox.Items.Add(var); break;
                    case 2: LocalVariablesListBox.Items.Add(var); break;
                    default: break;
                }
            }
        } 
        private void FncTagBox_TextChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.FunctionsTag = FncTagBox.Text;
                DLL.ConfigFunctions.Save();
            }
        }
        private void FncLengthCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.RandomFuncsLength = (FncLengthCombo.SelectedIndex + 1);
                DLL.ConfigFunctions.Save();
            }
        }
        private void VarLengthCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.RandomVarsLength = (VarLengthCombo.SelectedIndex + 1);
                DLL.ConfigFunctions.Save();
            }
        }
        private void FunctionsListAddButton_Click(object sender, EventArgs e)
        {
            string newVar = FunctionsListAddBox.Text;
            if (newVar == "") return;
            FunctionsListBox.Items.Add(newVar);
            FunctionsListAddBox.Text = "";
            List<string> varsInBox = new List<string>();
            foreach (string item in FunctionsListBox.Items) varsInBox.Add(item);
            DLL.ConfigValues.ObfFunctions = varsInBox;
            DLL.ConfigFunctions.Save();
        }
        private void RemoveSelectedFunctionButton_Click(object sender, EventArgs e)
        {
            FunctionsListBox.Items.Remove(FunctionsListBox.SelectedItem);
            List<string> varsInBox = new List<string>();
            foreach (string item in FunctionsListBox.Items) varsInBox.Add(item);
            DLL.ConfigValues.ObfFunctions = varsInBox;
            DLL.ConfigFunctions.Save();
        }
        private void GlobalVariablesListAddButton_Click(object sender, EventArgs e)
        {
            string newVar = GlobalVariablesListAddBox.Text;
            if (newVar == "") return;
            GlobalVariablesListBox.Items.Add(newVar);
            GlobalVariablesListAddBox.Text = "";
            List<string> varsInBox = new List<string>();
            foreach (string item in GlobalVariablesListBox.Items) varsInBox.Add(item);
            DLL.ConfigValues.ObfGlobalVars = varsInBox;
            DLL.ConfigFunctions.Save();
        }
        private void GlobalRemoveSelectedVariableButton_Click(object sender, EventArgs e)
        {
            GlobalVariablesListBox.Items.Remove(GlobalVariablesListBox.SelectedItem);
            List<string> varsInBox = new List<string>();
            foreach (string item in GlobalVariablesListBox.Items) varsInBox.Add(item);
            DLL.ConfigValues.ObfGlobalVars = varsInBox;
            DLL.ConfigFunctions.Save();
        }
        private void LocalVariablesListAddButton_Click(object sender, EventArgs e)
        {
            string newVar = LocalVariablesListAddBox.Text;
            if (newVar == "") return;
            LocalVariablesListBox.Items.Add(newVar);
            LocalVariablesListAddBox.Text = "";
            List<string> varsInBox = new List<string>();
            foreach (string item in LocalVariablesListBox.Items) varsInBox.Add(item);
            DLL.ConfigValues.ObfLocalVars = varsInBox;
            DLL.ConfigFunctions.Save();
        }
        private void LocalRemoveSelectedVariableButton_Click(object sender, EventArgs e)
        {
            LocalVariablesListBox.Items.Remove(LocalVariablesListBox.SelectedItem);
            List<string> varsInBox = new List<string>();
            foreach (string item in LocalVariablesListBox.Items) varsInBox.Add(item);
            DLL.ConfigValues.ObfLocalVars = varsInBox;
            DLL.ConfigFunctions.Save();
        }

        /// <summary>
        /// PBO setup
        /// </summary>
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
            PboNameBox.Text = pboValues.Name;
            GitPathBox.Text = pboValues.GitPath;
            GitUrlBox.Text = pboValues.GitUrl;
            GitTokenBox.Text = pboValues.GitToken;
            GitTypeCombo.SelectedIndex = (pboValues.GitType - 1);
            PboServerPathBox.Text = pboValues.ServerPath;
            OneLineButton.Checked = pboValues.OneLine;
            RenameFuncsButton.Checked = pboValues.RenameFuncs;
            RenameGlobalVarsButton.Checked = pboValues.RenameGlobalVars;
            RenameLocalVarsButton.Checked = pboValues.RenameLocalVars;
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
                        GitPath = GitPathBox.Text,
                        GitUrl = GitUrlBox.Text,
                        GitToken = GitTokenBox.Text,
                        GitType = (int)(GitType)Enum.ToObject(typeof(GitType), (GitTypeCombo.SelectedIndex + 1)),
                        ServerPath = PboServerPathBox.Text,
                        OneLine = OneLineButton.Checked,
                        RenameFuncs = RenameFuncsButton.Checked,
                        RenameGlobalVars = RenameGlobalVarsButton.Checked,
                        RenameLocalVars = RenameLocalVarsButton.Checked
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
            LoadPboContols(pboList[PboFileBox.SelectedIndex]);

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



        /// <summary>
        /// Launch
        /// </summary>
        private void LaunchButton_Click(object sender, EventArgs e) => Program.BackendDLL.PackServer();
      
    }   
}
