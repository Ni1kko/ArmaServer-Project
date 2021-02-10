using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ArmaServerBackend;

namespace ArmaServerFrontend
{
    internal partial class Home : Form
    {
        private static Form thisForm;
        private bool preloaded = false;
        private bool isOnline = false;
        private int lastSelectedPbo = 0;
        private Dictionary<int, PboFiles> pboList = new Dictionary<int, PboFiles>();

        public Home() {
            InitializeComponent();
            PreLoadHomeTabWindow();
            PreLoadServerTabWindow();
            PreLoadServerTab2Window();
            thisForm = this;
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

            PullOnStartButton.Checked = DLL.ConfigValues.serverSettings.PullOnStart;
        }

        /// GitDirectory
        private void BrowseGitDirectory_Click(object sender, EventArgs e) => GitDirectoryPathBox.Text = DLL.HelperFunctions.GetFolderPathDialog();
        private void GitDirectoryPathBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.GitDirectory = GitDirectoryPathBox.Text;
            DLL.ConfigFunctions.Save(); 
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
            if (!preloaded) return; 
            DLL.ConfigValues.RandomFunctionsLength = (FncLengthCombo.SelectedIndex + 1);
            DLL.ConfigFunctions.Save();
        
        }
        private void VarLengthCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.RandomVariablesLength = (VarLengthCombo.SelectedIndex + 1);
            DLL.ConfigFunctions.Save(); 
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

        // PBO setup 
        private void NewPboTab(PboFiles newPboValues)
        {
            List<PboFiles> PboFilesUpdated = new List<PboFiles>();
            int pboListCount = 0;

            /*if (PboFileBox.TabCount > 4)
            {
                MessageBox.Show("5 PBO's Max");
                return;
            }*/

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
            ModTypeCombo.SelectedIndex = (int)Enum.ToObject(typeof(PboModType), pboValues.ModType);
            PboEnabledCheckBox.Checked = pboValues.IsEnabled;
            PboNameBox.Text = pboValues.Name;
            GitPathBox.Text = pboValues.GitBranch;
            GitUrlBox.Text = pboValues.GitUrl;
            GitTokenBox.Text = pboValues.GitToken;
            GitTypeCombo.SelectedIndex = (int)Enum.ToObject(typeof(GitServer), pboValues.GitServer);
            PboServerPathBox.Text = pboValues.ServerPath;
            MissionDifficultyComboBox.SelectedIndex = (int)Enum.ToObject(typeof(MissionDifficulty), pboValues.MissionDifficulty); 
            OneLineButton.Checked = pboValues.SingleLineFunctions;
            RenameFuncsButton.Checked = pboValues.RandomizeFunctions;
            RenameGlobalVarsButton.Checked = pboValues.RandomizeGlobalVariables;
            RenameLocalVarsButton.Checked = pboValues.RandomizeLocalVariables;

            //Hide-Show controls
            MissionDifficultyLabel.Visible = (pboValues.ModType == PboModType.Mission);
            MissionDifficultyComboBox.Visible = (pboValues.ModType == PboModType.Mission);
            PboServerPathLabel.Visible = (pboValues.ModType != PboModType.Mission);
            PboServerPathBox.Visible = (pboValues.ModType != PboModType.Mission);
            PboServerPathButton.Visible = (pboValues.ModType != PboModType.Mission);
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
                    PboModType pboModType = (PboModType)Enum.ToObject(typeof(PboModType), ModTypeCombo.SelectedIndex);

                    PboFiles PboFileUpdated = new PboFiles()
                    {
                        ModType = pboModType,
                        IsEnabled = PboEnabledCheckBox.Checked,
                        Name = PboNameBox.Text,
                        GitBranch = GitPathBox.Text,
                        GitUrl = GitUrlBox.Text,
                        GitToken = GitTokenBox.Text,
                        GitServer = (GitServer)Enum.ToObject(typeof(GitServer), GitTypeCombo.SelectedIndex),
                        ServerPath = pboModType == PboModType.Mission  ? Path.Combine(DLL.ConfigValues.serverSettings.ServerDirectory, "mpmissions") : PboServerPathBox.Text,
                        MissionDifficulty = (MissionDifficulty)Enum.ToObject(typeof(MissionDifficulty), MissionDifficultyComboBox.SelectedIndex),
                        SingleLineFunctions = OneLineButton.Checked,
                        RandomizeFunctions = RenameFuncsButton.Checked,
                        RandomizeGlobalVariables = RenameGlobalVarsButton.Checked,
                        RandomizeLocalVariables = RenameLocalVarsButton.Checked
                    };

                    //Hide-Show controls
                    MissionDifficultyLabel.Visible = (pboModType == PboModType.Mission);
                    MissionDifficultyComboBox.Visible = (pboModType == PboModType.Mission);
                    PboServerPathLabel.Visible = (pboModType != PboModType.Mission);
                    PboServerPathBox.Visible = (pboModType != PboModType.Mission);
                    PboServerPathButton.Visible = (pboModType != PboModType.Mission);

                    pboList[pbo.Key] = PboFileUpdated;
                }
                PboFilesUpdated.Add(pboList[pbo.Key]);
            }
           
            DLL.ConfigValues.Pbos = PboFilesUpdated;
            DLL.ConfigFunctions.Save();

        }
        private void AddPboTabButton_Click(object sender, EventArgs e) => NewPboTab(new PboFilesDefault().Values(_IsEnabled:false));
        private void RemovePboButton_Click(object sender, EventArgs e)
        {
            List<PboFiles> PboFilesUpdated = new List<PboFiles>();

            foreach (KeyValuePair<int, PboFiles> pbo in pboList.ToList()) if (PboFileBox.SelectedIndex != pbo.Key) PboFilesUpdated.Add(pboList[pbo.Key]);
     
            pboList.Remove(PboFileBox.SelectedIndex);
            PboFileBox.TabPages.Remove(PboFileBox.TabPages[PboFileBox.SelectedIndex]);

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
                if (lastSelectedPbo == pbo.Key)
                {
                    LoadPboContols(pbo.Value);
                    return;
                }
            }
        }
        private void PboEnabledCheckBox_CheckedChanged(object sender, EventArgs e) => UpdatePbos();
        private void ModTypeCombo_SelectedValueChanged(object sender, EventArgs e) => UpdatePbos(); 
        private void MissionDifficultyComboBox_SelectedValueChanged(object sender, EventArgs e) => UpdatePbos();
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

        // Launch
        private void PullOnStartButton_CheckedChanged(object sender, EventArgs e)
        {
            DLL.ConfigValues.serverSettings.PullOnStart = PullOnStartButton.Checked;
            DLL.ConfigFunctions.Save();
        } 
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            isOnline = isOnline ? false : true;
            LaunchButton.Text = isOnline ? "Stop" : "Start";
            if (isOnline) LaunchButton.Enabled = false;
            ArmaProcessIDBox.Text = DLL.UtilityFunctions.SwitchOnlineState(
                ArmaProcessIDBox,
                isOnline,
                false,//runOnce
                thisForm,
                StartupProgressBar,
                PullOnStartButton.Checked,
                LaunchButton
            );
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Server TAB
        /// </summary> 
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void PreLoadServerTabWindow()
        {
            bool updateConfig = false;
            ServerDirectoryPathBox.Text = DLL.ConfigValues.serverSettings.ServerDirectory;
            ServerArchitectureCombo.SelectedIndex = (DLL.ConfigValues.serverSettings.X64Architecture) ? 1 : 0;
            HostNameBox.Text = DLL.ConfigValues.serverSettings.hostName;
            ServerPasswordBox.Text = DLL.ConfigValues.serverSettings.password;
            AdminPasswordBox.Text = DLL.ConfigValues.serverSettings.passwordAdmin;
            ServerCMDPasswordBox.Text = DLL.ConfigValues.serverSettings.serverCommandPassword;
            ServerLogBox.Text = DLL.ConfigValues.serverSettings.logFile;
            DLL.HelperFunctions.AddListBoxValue(MotdBox, DLL.ConfigValues.serverSettings.motd);
            MotdIntervalBox.Text = DLL.ConfigValues.serverSettings.motdInterval.ToString();
            MaxPlayersBox.Text = DLL.ConfigValues.serverSettings.maxPlayers.ToString();
            kickDuplicatesCheckBox.Checked = (DLL.ConfigValues.serverSettings.kickDuplicate == 1);
            VerifySignaturesCheckBox.Checked = (DLL.ConfigValues.serverSettings.verifySignatures == 1);
            DrawingInMapCheckBox.Checked = (DLL.ConfigValues.serverSettings.drawingInMap == 1);
            EnableVotingCheckbox.Checked = DLL.ConfigValues.serverSettings.VotingEnabled;
            VoteThresholdBox.Text = DLL.ConfigValues.serverSettings.voteThreshold.ToString();
            VoteMissionPlayersBox.Text = DLL.ConfigValues.serverSettings.voteMissionPlayers.ToString();
            DisableVoNBox.Checked = (DLL.ConfigValues.serverSettings.disableVoN == 1);
            if (DLL.ConfigValues.serverSettings.vonCodecQuality < 1 || DLL.ConfigValues.serverSettings.vonCodecQuality > 30)
            {
                DLL.ConfigValues.serverSettings.vonCodecQuality = 1;
                updateConfig = true;
            } 
            VonCodecQualityCombo.SelectedIndex = DLL.ConfigValues.serverSettings.vonCodecQuality - 1;
            if (DLL.ConfigValues.serverSettings.vonCodec < 0 || DLL.ConfigValues.serverSettings.vonCodec > 1)
            {
                DLL.ConfigValues.serverSettings.vonCodec = 0;
                updateConfig = true;
            }
            VoNCodecComboBox.SelectedIndex = DLL.ConfigValues.serverSettings.vonCodec;
            switch (DLL.ConfigValues.serverSettings.timeStampFormat)
            {
                case "none":
                    LogTimeStampFormatComboBox.SelectedIndex = 0;
                    break;
                case "short":
                    LogTimeStampFormatComboBox.SelectedIndex = 1;
                    break;
                case "full":
                    LogTimeStampFormatComboBox.SelectedIndex = 2;
                    break;
                default:
                    LogTimeStampFormatComboBox.SelectedIndex = 0;
                    break;
            }
            PersistentCheckBox.Checked = (DLL.ConfigValues.serverSettings.persistent == 1);
            BattlEyeCheckBox.Checked = (DLL.ConfigValues.serverSettings.BattlEye == 1);
            kickClientsOnSlowNetworkCheckBox.Checked = (DLL.ConfigValues.serverSettings.kickClientsOnSlowNetwork == 1);
            EnableMaxPingCheckBox.Checked = DLL.ConfigValues.serverSettings.maxPingEnabled;
            MaxPingBox.Text = DLL.ConfigValues.serverSettings.maxPing.ToString();
            EnableMaxPacketLossCheckBox.Checked = DLL.ConfigValues.serverSettings.maxPacketLossEnabled;
            MaxPacketLossBox.Text = DLL.ConfigValues.serverSettings.maxPacketLoss.ToString();
            MaxDesyncCheckbox.Checked = DLL.ConfigValues.serverSettings.maxDesyncEnabled;
            MaxDesyncBox.Text = DLL.ConfigValues.serverSettings.maxDesync.ToString(); 
            DisconnectTimeoutCheckBox.Checked = DLL.ConfigValues.serverSettings.disconnectTimeoutEnabled;
            DisconnectTimeoutBox.Text = DLL.ConfigValues.serverSettings.disconnectTimeout.ToString();
            LobbyTimeoutCheckBox.Checked = DLL.ConfigValues.serverSettings.lobbyIdleTimeoutEnabled;
            LobbyTimeoutBox.Text = DLL.ConfigValues.serverSettings.lobbyIdleTimeout.ToString();
            OnUserConnectedBox.Text = DLL.ConfigValues.serverSettings.onUserConnected;
            OnUserDisconnectedBox.Text = DLL.ConfigValues.serverSettings.onUserDisconnected;
            OnUnsignedDataBox.Text = DLL.ConfigValues.serverSettings.onUnsignedData;
            OnHackedDataBox.Text = DLL.ConfigValues.serverSettings.onHackedData;
            OnDifferentDataBox.Text = DLL.ConfigValues.serverSettings.onDifferentData;
            DoubleIdDetectedBox.Text = DLL.ConfigValues.serverSettings.doubleIdDetected;
            RegularCheckBox.Text = DLL.ConfigValues.serverSettings.regularCheck; 
            DLL.HelperFunctions.AddListBoxValue(AdminsBox, DLL.ConfigValues.serverSettings.admins);
            UpnpCheckbox.Checked = DLL.ConfigValues.serverSettings.upnp;
            EnableLoopbackCheckBox.Checked = DLL.ConfigValues.serverSettings.loopback;
            EnableNetlogCheckBox.Checked = (DLL.ConfigValues.serverSettings.enablePlayerDiag == 1);
            ExtReportLimitBox.Text = DLL.ConfigValues.serverSettings.callExtReportLimit.ToString();

            //Update if needed
            if (updateConfig) DLL.ConfigFunctions.Save();
        }

        // ServerDirectory
        private void BrowseServerDirectory_Click(object sender, EventArgs e) => ServerDirectoryPathBox.Text = DLL.HelperFunctions.GetFolderPathDialog();
        private void ServerDirectoryPathBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.ServerDirectory = ServerDirectoryPathBox.Text;
            DLL.ConfigFunctions.Save();
        }

        //Server architecture
        private void ServerArchitectureCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.X64Architecture = (ServerArchitectureCombo.SelectedIndex == 1);
            DLL.ConfigFunctions.Save();
        } 

        //HostName
        private void HostNameBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.hostName = HostNameBox.Text;
            DLL.ConfigFunctions.Save();
        }

        //Maxplayers
        private void MaxPlayersBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return; 
            //empty string, reset and update
            if (MaxPlayersBox.Text == "")
            {
                MaxPlayersBox.Text = "0";
                DLL.ConfigValues.serverSettings.maxPlayers = 0;
                DLL.ConfigFunctions.Save();
                return;
            }

            string oldValue = DLL.ConfigValues.serverSettings.maxPlayers.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(MaxPlayersBox.Text))
            {
                MaxPlayersBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            int maxPlayers = int.Parse(MaxPlayersBox.Text);//convert to int
            int maxPLayersLimit = 999;

            //Nope not that large
            if (maxPlayers > maxPLayersLimit)
            {
                MaxPlayersBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {maxPLayersLimit}!");
                return;
            }

            //update 
            DLL.ConfigValues.serverSettings.maxPlayers = maxPlayers;
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
        private void LogTimeStampFormatComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.timeStampFormat = (string)LogTimeStampFormatComboBox.Items[LogTimeStampFormatComboBox.SelectedIndex];
            DLL.ConfigFunctions.Save();
        }
        private void DefaultConsoleLogButton_Click(object sender, EventArgs e)
        {
            ServerLogBox.Text = ServerSettingsDefault.consoleLogFile;
            LogTimeStampFormatComboBox.SelectedIndex = 1;
            DLL.ConfigFunctions.Save();
        }
        
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

        //kick duplicates
        private void kickDuplicatesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return; 
            DLL.ConfigValues.serverSettings.kickDuplicate = (kickDuplicatesCheckBox.Checked ? 1 : 0);
            DLL.ConfigFunctions.Save();
        }

        //verify signatures
        private void VerifySignaturesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.verifySignatures = (VerifySignaturesCheckBox.Checked ? 1 : 0);
            DLL.ConfigFunctions.Save();
        }

        //Drawing In Map
        private void DrawingInMapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.drawingInMap = (DrawingInMapCheckBox.Checked ? 1 : 0);
            DLL.ConfigFunctions.Save();
        }

        //Voting
        private void EnableVotingCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.VotingEnabled = EnableVotingCheckbox.Checked;
            DLL.ConfigFunctions.Save();
        } 
        private void VoteThresholdBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            //empty string, reset and update
            if (VoteThresholdBox.Text == "")
            {
                VoteThresholdBox.Text = "0.00";
                DLL.ConfigValues.serverSettings.voteThreshold = 0.00;
                DLL.ConfigFunctions.Save();
                return;
            }

            string oldValue = DLL.ConfigValues.serverSettings.voteThreshold.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(VoteThresholdBox.Text))
            {
                VoteThresholdBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            double voteThreshold = double.Parse(VoteThresholdBox.Text);//convert to double
            double maxVoteThreshold = 999.9;

            //Nope not that large
            if (voteThreshold > maxVoteThreshold)
            {
                VoteThresholdBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {maxVoteThreshold}!");
                return;
            }

            //update 
            DLL.ConfigValues.serverSettings.voteThreshold = voteThreshold;
            DLL.ConfigFunctions.Save();
        }

        private void VoteMissionPlayersBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            //empty string, reset and update
            if (VoteMissionPlayersBox.Text == "")
            {
                VoteMissionPlayersBox.Text = "0";
                DLL.ConfigValues.serverSettings.voteMissionPlayers = 0;
                DLL.ConfigFunctions.Save();
                return;
            }

            string oldValue = DLL.ConfigValues.serverSettings.voteMissionPlayers.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(VoteMissionPlayersBox.Text))
            {
                VoteMissionPlayersBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            int maxPlayers = int.Parse(VoteMissionPlayersBox.Text);//convert to int
            int maxPLayersLimit = 999;

            //Nope not that large
            if (maxPlayers > maxPLayersLimit)
            {
                VoteMissionPlayersBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {maxPLayersLimit}!");
                return;
            }

            //update 
            DLL.ConfigValues.serverSettings.voteMissionPlayers = maxPlayers;
            DLL.ConfigFunctions.Save();
        }

        //VoN
        private void DisableVoNBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.disableVoN = (DisableVoNBox.Checked ? 1 : 0);
            DLL.ConfigFunctions.Save();
        } 
        private void VonCodecQualityCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.vonCodecQuality = VonCodecQualityCombo.SelectedIndex + 1;
            DLL.ConfigFunctions.Save();
        }
        private void VoNCodecComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.vonCodec = VoNCodecComboBox.SelectedIndex;
            DLL.ConfigFunctions.Save();
        }

        //Persistent
        private void PersistentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.persistent = (PersistentCheckBox.Checked ? 1 : 0);
            DLL.ConfigFunctions.Save();
        }

        //BattlEye
        private void BattlEyeCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.BattlEye = (BattlEyeCheckBox.Checked ? 1 : 0);
            DLL.ConfigFunctions.Save();
        }

        // kick On SlowNetwork
        private void kickClientsOnSlowNetworkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.kickClientsOnSlowNetwork = (kickClientsOnSlowNetworkCheckBox.Checked ? 1 : 0);
            DLL.ConfigFunctions.Save();
        }

        //Max ping
        private void EnableMaxPingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.maxPingEnabled = EnableMaxPingCheckBox.Checked;
            DLL.ConfigFunctions.Save();
        } 
        private void MaxPingBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            //empty string, reset and update
            if (MaxPingBox.Text == "")
            {
                MaxPingBox.Text = "0";
                DLL.ConfigValues.serverSettings.maxPing = 0;
                DLL.ConfigFunctions.Save();
                return;
            }

            string oldValue = DLL.ConfigValues.serverSettings.maxPing.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(MaxPingBox.Text))
            {
                MaxPingBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            int maxPing = int.Parse(MaxPingBox.Text);//convert to int
            int maxPingLimit = 9999;

            //Nope not that large
            if (maxPing > maxPingLimit)
            {
                MaxPingBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {maxPingLimit}!");
                return;
            }

            //update 
            DLL.ConfigValues.serverSettings.maxPing = maxPing;
            DLL.ConfigFunctions.Save();
        }

        //MaxPacketLoss
        private void EnableMaxPacketLossCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.maxPacketLossEnabled = EnableMaxPacketLossCheckBox.Checked;
            DLL.ConfigFunctions.Save();
        } 
        private void MaxPacketLossBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            //empty string, reset and update
            if (MaxPacketLossBox.Text == "")
            {
                MaxPacketLossBox.Text = "0";
                DLL.ConfigValues.serverSettings.maxPacketLoss = 0;
                DLL.ConfigFunctions.Save();
                return;
            }

            string oldValue = DLL.ConfigValues.serverSettings.maxPacketLoss.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(MaxPacketLossBox.Text))
            {
                MaxPacketLossBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            int maxPacketLoss = int.Parse(MaxPacketLossBox.Text);//convert to int
            int maxPacketLossLimit = 9999;

            //Nope not that large
            if (maxPacketLoss > maxPacketLossLimit)
            {
                MaxPacketLossBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {maxPacketLossLimit}!");
                return;
            }

            //update 
            DLL.ConfigValues.serverSettings.maxPacketLoss = maxPacketLoss;
            DLL.ConfigFunctions.Save();
        }

        //MaxDesync
        private void MaxDesyncCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.maxDesyncEnabled = MaxDesyncCheckbox.Checked;
            DLL.ConfigFunctions.Save();
        }
        private void MaxDesyncBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            //empty string, reset and update
            if (MaxDesyncBox.Text == "")
            {
                MaxDesyncBox.Text = "0";
                DLL.ConfigValues.serverSettings.maxDesync = 0;
                DLL.ConfigFunctions.Save();
                return;
            }

            string oldValue = DLL.ConfigValues.serverSettings.maxDesync.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(MaxDesyncBox.Text))
            {
                MaxDesyncBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            int maxDesync = int.Parse(MaxDesyncBox.Text);//convert to int
            int maxDesyncLimit = 9999;

            //Nope not that large
            if (maxDesync > maxDesyncLimit)
            {
                MaxDesyncBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {maxDesyncLimit}!");
                return;
            }

            //update 
            DLL.ConfigValues.serverSettings.maxDesync = maxDesync;
            DLL.ConfigFunctions.Save();
        }

        //Disconnected timeout
        private void DisconnectTimeoutCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.disconnectTimeoutEnabled = DisconnectTimeoutCheckBox.Checked;
            DLL.ConfigFunctions.Save();
        }
        private void DisconnectTimeoutBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            //empty string, reset and update
            if (DisconnectTimeoutBox.Text == "")
            {
                DisconnectTimeoutBox.Text = "5";
                DLL.ConfigValues.serverSettings.disconnectTimeout = 5;
                DLL.ConfigFunctions.Save();
                return;
            }

            string oldValue = DLL.ConfigValues.serverSettings.disconnectTimeout.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(DisconnectTimeoutBox.Text))
            {
                DisconnectTimeoutBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            int DisconnectTimeout = int.Parse(DisconnectTimeoutBox.Text);//convert to int
            int DisconnectTimeoutMin = 5;
            int DisconnectTimeouMax = 90;

            //Nope not that small
            if (DisconnectTimeout < DisconnectTimeoutMin)
            {
                DisconnectTimeoutBox.Text = oldValue;
                MessageBox.Show($"Enter a value above {DisconnectTimeoutMin}!");
                return;
            }
             
            //Nope not that large
            if (DisconnectTimeout > DisconnectTimeouMax)
            {
                DisconnectTimeoutBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {DisconnectTimeouMax}!");
                return;
            }

            //update 
            DLL.ConfigValues.serverSettings.disconnectTimeout = DisconnectTimeout;
            DLL.ConfigFunctions.Save();
        }

        //LobbyTimeout
        private void LobbyTimeoutCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.lobbyIdleTimeoutEnabled = LobbyTimeoutCheckBox.Checked;
            DLL.ConfigFunctions.Save();
        }
        private void LobbyTimeoutBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            //empty string, reset and update
            if (LobbyTimeoutBox.Text == "")
            {
                LobbyTimeoutBox.Text = "0";
                DLL.ConfigValues.serverSettings.lobbyIdleTimeout = 0;
                DLL.ConfigFunctions.Save();
                return;
            }

            string oldValue = DLL.ConfigValues.serverSettings.lobbyIdleTimeout.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(LobbyTimeoutBox.Text))
            {
                LobbyTimeoutBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            int LobbyTimeout = int.Parse(LobbyTimeoutBox.Text);//convert to int
            int LobbyTimeoutMin = 1;
            int LobbyTimeoutMax = 9999;
            
            //Nope not that small
            if (LobbyTimeout < LobbyTimeoutMin)
            {
                LobbyTimeoutBox.Text = oldValue;
                MessageBox.Show($"Enter a value above {LobbyTimeoutMin}!");
                return;
            }

            //Nope not that large
            if (LobbyTimeout > LobbyTimeoutMax)
            {
                LobbyTimeoutBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {LobbyTimeoutMax}!");
                return;
            }

            //update 
            DLL.ConfigValues.serverSettings.lobbyIdleTimeout = LobbyTimeout;
            DLL.ConfigFunctions.Save();
        }

        //scripts
        private void OnUserConnectedBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.onUserConnected = OnUserConnectedBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void OnUserDisconnectedBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.onUserDisconnected = OnUserDisconnectedBox.Text;
            DLL.ConfigFunctions.Save();
        } 
        private void OnUnsignedDataBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.onUnsignedData = OnUnsignedDataBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void OnHackedDataBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.onHackedData = OnHackedDataBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void OnDifferentDataBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.onDifferentData = OnDifferentDataBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void DoubleIdDetectedBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.doubleIdDetected = DoubleIdDetectedBox.Text;
            DLL.ConfigFunctions.Save();
        }
        private void RegularCheckBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.regularCheck = RegularCheckBox.Text;
            DLL.ConfigFunctions.Save();
        }
         
        //Admins
        private void AdminsAddButton_Click(object sender, EventArgs e)
        {
            if (AdminsAddBox.Text == "") return;

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(AdminsAddBox.Text))
            {
                AdminsAddBox.Text = "";
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            //Bad steamID
            if (!DLL.HelperFunctions.isValidSteamID(AdminsAddBox.Text))
            {
                AdminsAddBox.Text = "";
                MessageBox.Show("Enter a valid steamID!");
                return;
            }

            //Duplicate entry
            if (DLL.HelperFunctions.AlreadyInBox(AdminsBox, AdminsAddBox))
            {
                AdminsAddBox.Text = "";
                MessageBox.Show("steamID Already Listed!");
                return;
            }

            //update config
            DLL.ConfigValues.serverSettings.admins = DLL.HelperFunctions.AddListBoxValue(AdminsBox, AdminsAddBox);
            DLL.ConfigFunctions.Save();
        }
        private void AdminsRemoveButton_Click(object sender, EventArgs e)
        {
            DLL.ConfigValues.serverSettings.admins = DLL.HelperFunctions.RemoveListBoxValue(AdminsBox);
            DLL.ConfigFunctions.Save();
        }

        //Upnp
        private void UpnpCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.upnp = UpnpCheckbox.Checked;
            DLL.ConfigFunctions.Save();
        }

        //EnableLoopback
        private void EnableLoopbackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.loopback = EnableLoopbackCheckBox.Checked;
            DLL.ConfigFunctions.Save();
        }

        //EnableNetlog
        private void EnableNetlogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.enablePlayerDiag = (EnableNetlogCheckBox.Checked ? 1 : 0);
            DLL.ConfigFunctions.Save();
        }

        //callExtReportLimit
        private void ExtReportLimitBox_TextChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            //empty string, reset and update
            if (ExtReportLimitBox.Text == "")
            {
                ExtReportLimitBox.Text = "1000.0";
                DLL.ConfigValues.serverSettings.callExtReportLimit = 5;
                DLL.ConfigFunctions.Save();
                return;
            }
            
            string oldValue = DLL.ConfigValues.serverSettings.callExtReportLimit.ToString();//old value converted to a string

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(ExtReportLimitBox.Text))
            {
                ExtReportLimitBox.Text = oldValue;
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            double ExtReportLimit = double.Parse(ExtReportLimitBox.Text);//convert to int
            double ExtReportLimitMin = 1;
            double ExtReportLimitMax = 9999.9;

            //Nope not that small
            if (ExtReportLimit < ExtReportLimitMin)
            {
                ExtReportLimitBox.Text = oldValue;
                MessageBox.Show($"Enter a value above {ExtReportLimitMin}!");
                return;
            }

            //Nope not that large
            if (ExtReportLimit > ExtReportLimitMax)
            {
                ExtReportLimitBox.Text = oldValue;
                MessageBox.Show($"Enter a value below {ExtReportLimitMax}!");
                return;
            }

            //update 
            DLL.ConfigValues.serverSettings.callExtReportLimit = ExtReportLimit;
            DLL.ConfigFunctions.Save();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Server TAB
        /// </summary> 
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void PreLoadServerTab2Window()
        {
            bool updateConfig = false; 
            AllowFilePatchingCheckBox.Checked = (DLL.ConfigValues.serverSettings.allowedFilePatching == 1);
            DLL.HelperFunctions.AddListBoxValue(FilePatchingExceptionsBox, DLL.ConfigValues.serverSettings.filePatchingExceptions);
        

            //Update if needed
            if (updateConfig) DLL.ConfigFunctions.Save();
        }


        //FilePatching
        private void AllowFilePatchingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!preloaded) return;
            DLL.ConfigValues.serverSettings.allowedFilePatching = (AllowFilePatchingCheckBox.Checked ? 1 : 0);
            DLL.ConfigFunctions.Save();
        }
        private void FilePatchingExceptionsAddButton_Click(object sender, EventArgs e)
        {
            if (FilePatchingExceptionsAddBox.Text == "") return;

            //Has char in string
            if (DLL.HelperFunctions.StringContainsChar(FilePatchingExceptionsAddBox.Text))
            {
                FilePatchingExceptionsAddBox.Text = "";
                MessageBox.Show("Enter a numeric value only!");
                return;
            }

            //Bad steamID
            if (!DLL.HelperFunctions.isValidSteamID(FilePatchingExceptionsAddBox.Text))
            {
                FilePatchingExceptionsAddBox.Text = "";
                MessageBox.Show("Enter a valid steamID!");
                return;
            }

            //Duplicate entry
            if (DLL.HelperFunctions.AlreadyInBox(FilePatchingExceptionsBox, FilePatchingExceptionsAddBox))
            {
                FilePatchingExceptionsAddBox.Text = "";
                MessageBox.Show("steamID Already Listed!");
                return;
            }

            //update config
            DLL.ConfigValues.serverSettings.filePatchingExceptions = DLL.HelperFunctions.AddListBoxValue(FilePatchingExceptionsBox, FilePatchingExceptionsAddBox);
            DLL.ConfigFunctions.Save();
        }
        private void FilePatchingExceptionsRemoveButton_Click(object sender, EventArgs e)
        {
            DLL.ConfigValues.serverSettings.filePatchingExceptions = DLL.HelperFunctions.RemoveListBoxValue(FilePatchingExceptionsBox);
            DLL.ConfigFunctions.Save();
        }
 




























        //

    }
}
