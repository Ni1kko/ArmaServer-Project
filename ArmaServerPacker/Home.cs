using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ArmaServerBackend;

namespace ArmaServerFrontend
{
    public partial class Home : Form
    {
        public Home() => PreLoadWindow();

        Dictionary<int, PboFiles> pboList = new Dictionary<int, PboFiles>();
        bool preloaded = false;
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

        private void PreLoadWindow()
        {
            InitializeComponent();
            ServerDirectoryPathBox.Text = DLL.ConfigValues.ServerDirectory;
            GitDirectoryPathBox.Text = DLL.ConfigValues.GitDirectory;
            FncTagBox.Text = DLL.ConfigValues.FunctionsTag;
            KillArmaCheckBox.Checked = DLL.ConfigValues.KillArmaServer;
            Use64BitArmaButton.Checked = DLL.ConfigValues.Use64BitServer;
            FncLengthCombo.SelectedIndex = (DLL.ConfigValues.RandomFuncsLength - 1);
            VarLengthCombo.SelectedIndex = (DLL.ConfigValues.RandomVarsLength - 1);

            // as will your original TabPage

            int pboListCount = 0;
            foreach (PboFiles pbo in DLL.ConfigValues.Pbos)
            {
                TabPage NewPboTab = new TabPage(pbo.Name);
                PboFileBox.TabPages.Add(pbo.Name);//Add Tab Name 
                pboList.Add(pboListCount, pbo);
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
        /// FunctionTag
        /// </summary>
        private void FncTagBox_TextChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.FunctionsTag = FncTagBox.Text;
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
        /// Random Functions Length
        /// </summary>
        private void FncLengthCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.RandomFuncsLength = (FncLengthCombo.SelectedIndex + 1);
                DLL.ConfigFunctions.Save();
            }
        }

        /// <summary>
        /// Random Varibales Length
        /// </summary>
        private void VarLengthCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (preloaded)
            {
                DLL.ConfigValues.RandomVarsLength = (VarLengthCombo.SelectedIndex + 1);
                DLL.ConfigFunctions.Save();
            }
        }

        /// <summary>
        /// PBO setup
        /// </summary> 
        private int lastSelectedPbo = 0;
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
        private void UpdatePbos(int _index, PboFiles newPboValues)
        {
            if (!preloaded) return;
            List<PboFiles> PboFilesUpdated = new List<PboFiles>();

            foreach (KeyValuePair<int, PboFiles> pbo in pboList.ToList())
            {
                if (_index == pbo.Key)
                {
                    PboFileBox.TabPages[pbo.Key].Text = newPboValues.Name;

                    PboFiles PboFileUpdated = new PboFiles
                    {
                        Name = newPboValues.Name,
                        GitPath = newPboValues.GitPath,
                        GitUrl = newPboValues.GitUrl,
                        GitToken = newPboValues.GitToken,
                        GitType = newPboValues.GitType,
                        ServerPath = newPboValues.ServerPath,
                        OneLine = newPboValues.OneLine,
                        RenameFuncs = newPboValues.RenameFuncs,
                        RenameGlobalVars = newPboValues.RenameGlobalVars,
                        RenameLocalVars = newPboValues.RenameLocalVars
                    };

                    pboList[pbo.Key] = PboFileUpdated;
                }
                PboFilesUpdated.Add(pboList[pbo.Key]);
            }

            DLL.ConfigValues.Pbos = PboFilesUpdated;
            DLL.ConfigFunctions.Save();

        }
        private void PboNameBox_TextChanged(object sender, EventArgs e)
        {
            //name
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }

        private void GitPathBrowse_Click(object sender, EventArgs e) => GitPathBox.Text = DLL.HelperFunctions.GetFolderPathDialog();
        private void GitPathBox_TextChanged(object sender, EventArgs e)
        {
            //Url
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }
        private void PboServerPathButton_Click(object sender, EventArgs e) => PboServerPathBox.Text = DLL.HelperFunctions.GetFolderPathDialog();
        private void PboServerPathBox_TextChanged(object sender, EventArgs e)
        {
            //output path
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }

       
        
        private void GitUrlBox_TextChanged(object sender, EventArgs e)
        {
            //Url
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }

        private void GitTokenBox_TextChanged(object sender, EventArgs e)
        {
            //Token
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }

        private void GitTypeCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            //GitTypeCombo 
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }
        private void OneLineButton_CheckedChanged(object sender, EventArgs e)
        {
            //OneLine
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }
        private void RenameFuncsButton_CheckedChanged(object sender, EventArgs e)
        {
            //RenameFuncs
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }

        private void RenameGlobalVarsButton_CheckedChanged(object sender, EventArgs e)
        {
            //RenameGlobalVars
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }

        private void RenameLocalVarsButton_CheckedChanged(object sender, EventArgs e)
        {
            //RenameLocalVar
            UpdatePbos(PboFileBox.SelectedIndex, new PboFiles()
            {
                Name = PboNameBox.Text,
                GitPath = GitPathBox.Text,
                GitUrl = GitUrlBox.Text,
                GitToken = GitTokenBox.Text,
                GitType = (GitTypeCombo.SelectedIndex + 1),
                ServerPath = PboServerPathBox.Text,
                OneLine = OneLineButton.Checked,
                RenameFuncs = RenameFuncsButton.Checked,
                RenameGlobalVars = RenameGlobalVarsButton.Checked,
                RenameLocalVars = RenameLocalVarsButton.Checked
            });
        }
        

        /// <summary>
        /// Launch
        /// </summary>
        private void LaunchButton_Click(object sender, EventArgs e) => Program.BackendDLL.PackServer();

        
    }   
}
