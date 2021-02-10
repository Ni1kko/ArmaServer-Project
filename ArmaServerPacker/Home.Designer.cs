 namespace ArmaServerFrontend
{
    internal partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BrowseGitDirectory = new System.Windows.Forms.Button();
            this.GitDirectoryPathBox = new System.Windows.Forms.TextBox();
            this.GitDirectoryLabel = new System.Windows.Forms.Label();
            this.FncTagLabel = new System.Windows.Forms.Label();
            this.FncTagBox = new System.Windows.Forms.TextBox();
            this.FncLengthCombo = new System.Windows.Forms.ComboBox();
            this.VarLengthCombo = new System.Windows.Forms.ComboBox();
            this.FncLengthLabel = new System.Windows.Forms.Label();
            this.VarLengthLabel = new System.Windows.Forms.Label();
            this.LaunchButton = new System.Windows.Forms.Button();
            this.PboStartTab = new System.Windows.Forms.TabPage();
            this.PboFileBox = new System.Windows.Forms.TabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.OneLineButton = new System.Windows.Forms.CheckBox();
            this.RenameLocalVarsButton = new System.Windows.Forms.CheckBox();
            this.RenameGlobalVarsButton = new System.Windows.Forms.CheckBox();
            this.RenameFuncsButton = new System.Windows.Forms.CheckBox();
            this.PboServerPathLabel = new System.Windows.Forms.Label();
            this.PboServerPathButton = new System.Windows.Forms.Button();
            this.PboServerPathBox = new System.Windows.Forms.TextBox();
            this.GitUrlLabel = new System.Windows.Forms.Label();
            this.GitUrlBox = new System.Windows.Forms.TextBox();
            this.GitTypeLabel = new System.Windows.Forms.Label();
            this.GitTypeCombo = new System.Windows.Forms.ComboBox();
            this.GitTokenLabel = new System.Windows.Forms.Label();
            this.GitTokenBox = new System.Windows.Forms.TextBox();
            this.GitPathLabel = new System.Windows.Forms.Label();
            this.GitPathBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PboNameBox = new System.Windows.Forms.TextBox();
            this.AddPboTabButton = new System.Windows.Forms.Button();
            this.RemovePboButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LocalRemoveSelectedVariableButton = new System.Windows.Forms.Button();
            this.LocalVariablesListAddButton = new System.Windows.Forms.Button();
            this.LocalVariablesListAddBox = new System.Windows.Forms.TextBox();
            this.LocalVariablesListLabel = new System.Windows.Forms.Label();
            this.LocalVariablesListBox = new System.Windows.Forms.ListBox();
            this.GlobalRemoveSelectedVariableButton = new System.Windows.Forms.Button();
            this.RemoveSelectedFunctionButton = new System.Windows.Forms.Button();
            this.GlobalVariablesListAddButton = new System.Windows.Forms.Button();
            this.GlobalVariablesListAddBox = new System.Windows.Forms.TextBox();
            this.FunctionsListAddButton = new System.Windows.Forms.Button();
            this.FunctionsListAddBox = new System.Windows.Forms.TextBox();
            this.GlobalVariablesListLabel = new System.Windows.Forms.Label();
            this.GlobalVariablesListBox = new System.Windows.Forms.ListBox();
            this.FunctionsListBox = new System.Windows.Forms.ListBox();
            this.FunctionsListLabel = new System.Windows.Forms.Label();
            this.Pages = new System.Windows.Forms.TabControl();
            this.MainTab = new System.Windows.Forms.TabPage();
            this.ServerTab = new System.Windows.Forms.TabPage();
            this.DefaultConsoleLogButton = new System.Windows.Forms.Button();
            this.GenerateCmdPasswordButton = new System.Windows.Forms.Button();
            this.GenerateAdminPasswordButton = new System.Windows.Forms.Button();
            this.GenerateServerPasswordButton = new System.Windows.Forms.Button();
            this.MotdIntervalLabel = new System.Windows.Forms.Label();
            this.MotdIntervalBox = new System.Windows.Forms.TextBox();
            this.MotdRemoveButton = new System.Windows.Forms.Button();
            this.MotdAddButton = new System.Windows.Forms.Button();
            this.MotdAddBox = new System.Windows.Forms.TextBox();
            this.MotdBox = new System.Windows.Forms.ListBox();
            this.MotdLabel = new System.Windows.Forms.Label();
            this.ServerLogLabel = new System.Windows.Forms.Label();
            this.ServerLogBox = new System.Windows.Forms.TextBox();
            this.ServerCMDPasswordLabel = new System.Windows.Forms.Label();
            this.ServerCMDPasswordBox = new System.Windows.Forms.TextBox();
            this.AdminPasswordLLabel = new System.Windows.Forms.Label();
            this.AdminPasswordBox = new System.Windows.Forms.TextBox();
            this.ServerPasswordLabel = new System.Windows.Forms.Label();
            this.ServerPasswordBox = new System.Windows.Forms.TextBox();
            this.ServerDirectoryLabel = new System.Windows.Forms.Label();
            this.ServerDirectoryPathBox = new System.Windows.Forms.TextBox();
            this.BrowseServerDirectory = new System.Windows.Forms.Button();
            this.HostNameLabel = new System.Windows.Forms.Label();
            this.HostNameBox = new System.Windows.Forms.TextBox();
            this.Use64BitArmaButton = new System.Windows.Forms.CheckBox();
            this.PboFileBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Pages.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.ServerTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // BrowseGitDirectory
            // 
            this.BrowseGitDirectory.Location = new System.Drawing.Point(457, 29);
            this.BrowseGitDirectory.Name = "BrowseGitDirectory";
            this.BrowseGitDirectory.Size = new System.Drawing.Size(86, 20);
            this.BrowseGitDirectory.TabIndex = 4;
            this.BrowseGitDirectory.Text = "Browse";
            this.BrowseGitDirectory.UseVisualStyleBackColor = true;
            this.BrowseGitDirectory.Click += new System.EventHandler(this.BrowseGitDirectory_Click);
            // 
            // GitDirectoryPathBox
            // 
            this.GitDirectoryPathBox.Location = new System.Drawing.Point(30, 29);
            this.GitDirectoryPathBox.Name = "GitDirectoryPathBox";
            this.GitDirectoryPathBox.Size = new System.Drawing.Size(421, 20);
            this.GitDirectoryPathBox.TabIndex = 3;
            this.GitDirectoryPathBox.TextChanged += new System.EventHandler(this.GitDirectoryPathBox_TextChanged);
            // 
            // GitDirectoryLabel
            // 
            this.GitDirectoryLabel.AutoSize = true;
            this.GitDirectoryLabel.Location = new System.Drawing.Point(27, 13);
            this.GitDirectoryLabel.Name = "GitDirectoryLabel";
            this.GitDirectoryLabel.Size = new System.Drawing.Size(65, 13);
            this.GitDirectoryLabel.TabIndex = 5;
            this.GitDirectoryLabel.Text = "Git Directory";
            // 
            // FncTagLabel
            // 
            this.FncTagLabel.AutoSize = true;
            this.FncTagLabel.Location = new System.Drawing.Point(67, 54);
            this.FncTagLabel.Name = "FncTagLabel";
            this.FncTagLabel.Size = new System.Drawing.Size(70, 13);
            this.FncTagLabel.TabIndex = 8;
            this.FncTagLabel.Text = "Function Tag";
            // 
            // FncTagBox
            // 
            this.FncTagBox.Location = new System.Drawing.Point(70, 70);
            this.FncTagBox.Name = "FncTagBox";
            this.FncTagBox.Size = new System.Drawing.Size(80, 20);
            this.FncTagBox.TabIndex = 7;
            this.FncTagBox.Text = "Tag";
            this.FncTagBox.TextChanged += new System.EventHandler(this.FncTagBox_TextChanged);
            // 
            // FncLengthCombo
            // 
            this.FncLengthCombo.DisplayMember = "1";
            this.FncLengthCombo.FormattingEnabled = true;
            this.FncLengthCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.FncLengthCombo.Location = new System.Drawing.Point(241, 70);
            this.FncLengthCombo.Name = "FncLengthCombo";
            this.FncLengthCombo.Size = new System.Drawing.Size(86, 21);
            this.FncLengthCombo.TabIndex = 9;
            this.FncLengthCombo.ValueMember = "1";
            this.FncLengthCombo.SelectedValueChanged += new System.EventHandler(this.FncLengthCombo_SelectedValueChanged);
            // 
            // VarLengthCombo
            // 
            this.VarLengthCombo.DisplayMember = "1";
            this.VarLengthCombo.FormattingEnabled = true;
            this.VarLengthCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.VarLengthCombo.Location = new System.Drawing.Point(419, 69);
            this.VarLengthCombo.Name = "VarLengthCombo";
            this.VarLengthCombo.Size = new System.Drawing.Size(83, 21);
            this.VarLengthCombo.TabIndex = 10;
            this.VarLengthCombo.ValueMember = "1";
            this.VarLengthCombo.SelectedValueChanged += new System.EventHandler(this.VarLengthCombo_SelectedValueChanged);
            // 
            // FncLengthLabel
            // 
            this.FncLengthLabel.AutoSize = true;
            this.FncLengthLabel.Location = new System.Drawing.Point(238, 54);
            this.FncLengthLabel.Name = "FncLengthLabel";
            this.FncLengthLabel.Size = new System.Drawing.Size(89, 13);
            this.FncLengthLabel.TabIndex = 11;
            this.FncLengthLabel.Text = "Functions Length";
            // 
            // VarLengthLabel
            // 
            this.VarLengthLabel.AutoSize = true;
            this.VarLengthLabel.Location = new System.Drawing.Point(416, 53);
            this.VarLengthLabel.Name = "VarLengthLabel";
            this.VarLengthLabel.Size = new System.Drawing.Size(86, 13);
            this.VarLengthLabel.TabIndex = 12;
            this.VarLengthLabel.Text = "Variables Length";
            // 
            // LaunchButton
            // 
            this.LaunchButton.Location = new System.Drawing.Point(248, 652);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(75, 23);
            this.LaunchButton.TabIndex = 13;
            this.LaunchButton.Text = "Start";
            this.LaunchButton.UseVisualStyleBackColor = true;
            this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // PboStartTab
            // 
            this.PboStartTab.Location = new System.Drawing.Point(4, 22);
            this.PboStartTab.Name = "PboStartTab";
            this.PboStartTab.Padding = new System.Windows.Forms.Padding(3);
            this.PboStartTab.Size = new System.Drawing.Size(505, 0);
            this.PboStartTab.TabIndex = 0;
            this.PboStartTab.Text = "tabPage1";
            this.PboStartTab.UseVisualStyleBackColor = true;
            // 
            // PboFileBox
            // 
            this.PboFileBox.Controls.Add(this.PboStartTab);
            this.PboFileBox.Location = new System.Drawing.Point(26, 437);
            this.PboFileBox.Name = "PboFileBox";
            this.PboFileBox.SelectedIndex = 0;
            this.PboFileBox.Size = new System.Drawing.Size(513, 20);
            this.PboFileBox.TabIndex = 16;
            this.PboFileBox.Click += new System.EventHandler(this.PboFileBox_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.OneLineButton);
            this.panel1.Controls.Add(this.RenameLocalVarsButton);
            this.panel1.Controls.Add(this.RenameGlobalVarsButton);
            this.panel1.Controls.Add(this.RenameFuncsButton);
            this.panel1.Controls.Add(this.PboServerPathLabel);
            this.panel1.Controls.Add(this.PboServerPathButton);
            this.panel1.Controls.Add(this.PboServerPathBox);
            this.panel1.Controls.Add(this.GitUrlLabel);
            this.panel1.Controls.Add(this.GitUrlBox);
            this.panel1.Controls.Add(this.GitTypeLabel);
            this.panel1.Controls.Add(this.GitTypeCombo);
            this.panel1.Controls.Add(this.GitTokenLabel);
            this.panel1.Controls.Add(this.GitTokenBox);
            this.panel1.Controls.Add(this.GitPathLabel);
            this.panel1.Controls.Add(this.GitPathBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PboNameBox);
            this.panel1.Location = new System.Drawing.Point(26, 465);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 179);
            this.panel1.TabIndex = 17;
            // 
            // OneLineButton
            // 
            this.OneLineButton.AutoSize = true;
            this.OneLineButton.Location = new System.Drawing.Point(402, 147);
            this.OneLineButton.Name = "OneLineButton";
            this.OneLineButton.Size = new System.Drawing.Size(94, 17);
            this.OneLineButton.TabIndex = 23;
            this.OneLineButton.Text = "OneLine Code";
            this.OneLineButton.UseVisualStyleBackColor = true;
            this.OneLineButton.CheckedChanged += new System.EventHandler(this.OneLineButton_CheckedChanged);
            // 
            // RenameLocalVarsButton
            // 
            this.RenameLocalVarsButton.AutoSize = true;
            this.RenameLocalVarsButton.Location = new System.Drawing.Point(280, 147);
            this.RenameLocalVarsButton.Name = "RenameLocalVarsButton";
            this.RenameLocalVarsButton.Size = new System.Drawing.Size(116, 17);
            this.RenameLocalVarsButton.TabIndex = 22;
            this.RenameLocalVarsButton.Text = "Rename LocalVars";
            this.RenameLocalVarsButton.UseVisualStyleBackColor = true;
            this.RenameLocalVarsButton.CheckedChanged += new System.EventHandler(this.RenameLocalVarsButton_CheckedChanged);
            // 
            // RenameGlobalVarsButton
            // 
            this.RenameGlobalVarsButton.AutoSize = true;
            this.RenameGlobalVarsButton.Location = new System.Drawing.Point(154, 147);
            this.RenameGlobalVarsButton.Name = "RenameGlobalVarsButton";
            this.RenameGlobalVarsButton.Size = new System.Drawing.Size(120, 17);
            this.RenameGlobalVarsButton.TabIndex = 21;
            this.RenameGlobalVarsButton.Text = "Rename GlobalVars";
            this.RenameGlobalVarsButton.UseVisualStyleBackColor = true;
            this.RenameGlobalVarsButton.CheckedChanged += new System.EventHandler(this.RenameGlobalVarsButton_CheckedChanged);
            // 
            // RenameFuncsButton
            // 
            this.RenameFuncsButton.AutoSize = true;
            this.RenameFuncsButton.Location = new System.Drawing.Point(28, 147);
            this.RenameFuncsButton.Name = "RenameFuncsButton";
            this.RenameFuncsButton.Size = new System.Drawing.Size(115, 17);
            this.RenameFuncsButton.TabIndex = 20;
            this.RenameFuncsButton.Text = "Rename Functions";
            this.RenameFuncsButton.UseVisualStyleBackColor = true;
            this.RenameFuncsButton.CheckedChanged += new System.EventHandler(this.RenameFuncsButton_CheckedChanged);
            // 
            // PboServerPathLabel
            // 
            this.PboServerPathLabel.AutoSize = true;
            this.PboServerPathLabel.Location = new System.Drawing.Point(25, 92);
            this.PboServerPathLabel.Name = "PboServerPathLabel";
            this.PboServerPathLabel.Size = new System.Drawing.Size(105, 13);
            this.PboServerPathLabel.TabIndex = 19;
            this.PboServerPathLabel.Text = "Pbo Server Directory";
            // 
            // PboServerPathButton
            // 
            this.PboServerPathButton.Location = new System.Drawing.Point(402, 107);
            this.PboServerPathButton.Name = "PboServerPathButton";
            this.PboServerPathButton.Size = new System.Drawing.Size(86, 20);
            this.PboServerPathButton.TabIndex = 18;
            this.PboServerPathButton.Text = "Browse";
            this.PboServerPathButton.UseVisualStyleBackColor = true;
            this.PboServerPathButton.Click += new System.EventHandler(this.PboServerPathButton_Click);
            // 
            // PboServerPathBox
            // 
            this.PboServerPathBox.Location = new System.Drawing.Point(28, 108);
            this.PboServerPathBox.Name = "PboServerPathBox";
            this.PboServerPathBox.Size = new System.Drawing.Size(368, 20);
            this.PboServerPathBox.TabIndex = 17;
            this.PboServerPathBox.TextChanged += new System.EventHandler(this.PboServerPathBox_TextChanged);
            // 
            // GitUrlLabel
            // 
            this.GitUrlLabel.AutoSize = true;
            this.GitUrlLabel.Location = new System.Drawing.Point(25, 49);
            this.GitUrlLabel.Name = "GitUrlLabel";
            this.GitUrlLabel.Size = new System.Drawing.Size(36, 13);
            this.GitUrlLabel.TabIndex = 16;
            this.GitUrlLabel.Text = "Git Url";
            // 
            // GitUrlBox
            // 
            this.GitUrlBox.Location = new System.Drawing.Point(28, 65);
            this.GitUrlBox.Name = "GitUrlBox";
            this.GitUrlBox.Size = new System.Drawing.Size(339, 20);
            this.GitUrlBox.TabIndex = 15;
            this.GitUrlBox.TextChanged += new System.EventHandler(this.GitUrlBox_TextChanged);
            // 
            // GitTypeLabel
            // 
            this.GitTypeLabel.AutoSize = true;
            this.GitTypeLabel.Location = new System.Drawing.Point(427, 10);
            this.GitTypeLabel.Name = "GitTypeLabel";
            this.GitTypeLabel.Size = new System.Drawing.Size(45, 13);
            this.GitTypeLabel.TabIndex = 14;
            this.GitTypeLabel.Text = "Git Host";
            // 
            // GitTypeCombo
            // 
            this.GitTypeCombo.DisplayMember = "1";
            this.GitTypeCombo.FormattingEnabled = true;
            this.GitTypeCombo.Items.AddRange(new object[] {
            "GitHub",
            "GitLab"});
            this.GitTypeCombo.Location = new System.Drawing.Point(427, 26);
            this.GitTypeCombo.Name = "GitTypeCombo";
            this.GitTypeCombo.Size = new System.Drawing.Size(60, 21);
            this.GitTypeCombo.TabIndex = 13;
            this.GitTypeCombo.ValueMember = "1";
            this.GitTypeCombo.SelectedValueChanged += new System.EventHandler(this.GitTypeCombo_SelectedValueChanged);
            // 
            // GitTokenLabel
            // 
            this.GitTokenLabel.AutoSize = true;
            this.GitTokenLabel.Location = new System.Drawing.Point(220, 10);
            this.GitTokenLabel.Name = "GitTokenLabel";
            this.GitTokenLabel.Size = new System.Drawing.Size(54, 13);
            this.GitTokenLabel.TabIndex = 12;
            this.GitTokenLabel.Text = "Git Token";
            // 
            // GitTokenBox
            // 
            this.GitTokenBox.Location = new System.Drawing.Point(222, 25);
            this.GitTokenBox.Name = "GitTokenBox";
            this.GitTokenBox.PasswordChar = '★';
            this.GitTokenBox.Size = new System.Drawing.Size(199, 20);
            this.GitTokenBox.TabIndex = 11;
            this.GitTokenBox.TextChanged += new System.EventHandler(this.GitTokenBox_TextChanged);
            // 
            // GitPathLabel
            // 
            this.GitPathLabel.AutoSize = true;
            this.GitPathLabel.Location = new System.Drawing.Point(370, 49);
            this.GitPathLabel.Name = "GitPathLabel";
            this.GitPathLabel.Size = new System.Drawing.Size(57, 13);
            this.GitPathLabel.TabIndex = 10;
            this.GitPathLabel.Text = "Git Branch";
            // 
            // GitPathBox
            // 
            this.GitPathBox.Location = new System.Drawing.Point(373, 65);
            this.GitPathBox.Name = "GitPathBox";
            this.GitPathBox.Size = new System.Drawing.Size(115, 20);
            this.GitPathBox.TabIndex = 8;
            this.GitPathBox.TextChanged += new System.EventHandler(this.GitPathBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Pbo name";
            // 
            // PboNameBox
            // 
            this.PboNameBox.Location = new System.Drawing.Point(28, 26);
            this.PboNameBox.Name = "PboNameBox";
            this.PboNameBox.Size = new System.Drawing.Size(179, 20);
            this.PboNameBox.TabIndex = 6;
            this.PboNameBox.TextChanged += new System.EventHandler(this.PboNameBox_TextChanged);
            // 
            // AddPboTabButton
            // 
            this.AddPboTabButton.Location = new System.Drawing.Point(26, 408);
            this.AddPboTabButton.Name = "AddPboTabButton";
            this.AddPboTabButton.Size = new System.Drawing.Size(250, 23);
            this.AddPboTabButton.TabIndex = 18;
            this.AddPboTabButton.Text = "Add New PBO";
            this.AddPboTabButton.UseVisualStyleBackColor = true;
            this.AddPboTabButton.Click += new System.EventHandler(this.AddPboTabButton_Click);
            // 
            // RemovePboButton
            // 
            this.RemovePboButton.Location = new System.Drawing.Point(289, 408);
            this.RemovePboButton.Name = "RemovePboButton";
            this.RemovePboButton.Size = new System.Drawing.Size(250, 23);
            this.RemovePboButton.TabIndex = 19;
            this.RemovePboButton.Text = "Remove Selected PBO";
            this.RemovePboButton.UseVisualStyleBackColor = true;
            this.RemovePboButton.Click += new System.EventHandler(this.RemovePboButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LocalRemoveSelectedVariableButton);
            this.panel2.Controls.Add(this.LocalVariablesListAddButton);
            this.panel2.Controls.Add(this.LocalVariablesListAddBox);
            this.panel2.Controls.Add(this.LocalVariablesListLabel);
            this.panel2.Controls.Add(this.LocalVariablesListBox);
            this.panel2.Controls.Add(this.GlobalRemoveSelectedVariableButton);
            this.panel2.Controls.Add(this.RemoveSelectedFunctionButton);
            this.panel2.Controls.Add(this.GlobalVariablesListAddButton);
            this.panel2.Controls.Add(this.GlobalVariablesListAddBox);
            this.panel2.Controls.Add(this.FunctionsListAddButton);
            this.panel2.Controls.Add(this.FunctionsListAddBox);
            this.panel2.Controls.Add(this.GlobalVariablesListLabel);
            this.panel2.Controls.Add(this.GlobalVariablesListBox);
            this.panel2.Controls.Add(this.FunctionsListBox);
            this.panel2.Controls.Add(this.FunctionsListLabel);
            this.panel2.Location = new System.Drawing.Point(30, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 292);
            this.panel2.TabIndex = 20;
            // 
            // LocalRemoveSelectedVariableButton
            // 
            this.LocalRemoveSelectedVariableButton.Location = new System.Drawing.Point(347, 260);
            this.LocalRemoveSelectedVariableButton.Name = "LocalRemoveSelectedVariableButton";
            this.LocalRemoveSelectedVariableButton.Size = new System.Drawing.Size(154, 26);
            this.LocalRemoveSelectedVariableButton.TabIndex = 26;
            this.LocalRemoveSelectedVariableButton.Text = "Remove Selected LocalVar";
            this.LocalRemoveSelectedVariableButton.UseVisualStyleBackColor = true;
            this.LocalRemoveSelectedVariableButton.Click += new System.EventHandler(this.LocalRemoveSelectedVariableButton_Click);
            // 
            // LocalVariablesListAddButton
            // 
            this.LocalVariablesListAddButton.Location = new System.Drawing.Point(347, 234);
            this.LocalVariablesListAddButton.Name = "LocalVariablesListAddButton";
            this.LocalVariablesListAddButton.Size = new System.Drawing.Size(154, 20);
            this.LocalVariablesListAddButton.TabIndex = 25;
            this.LocalVariablesListAddButton.Text = "Add";
            this.LocalVariablesListAddButton.UseVisualStyleBackColor = true;
            this.LocalVariablesListAddButton.Click += new System.EventHandler(this.LocalVariablesListAddButton_Click);
            // 
            // LocalVariablesListAddBox
            // 
            this.LocalVariablesListAddBox.Location = new System.Drawing.Point(342, 203);
            this.LocalVariablesListAddBox.Name = "LocalVariablesListAddBox";
            this.LocalVariablesListAddBox.Size = new System.Drawing.Size(159, 20);
            this.LocalVariablesListAddBox.TabIndex = 24;
            // 
            // LocalVariablesListLabel
            // 
            this.LocalVariablesListLabel.AutoSize = true;
            this.LocalVariablesListLabel.Location = new System.Drawing.Point(342, 1);
            this.LocalVariablesListLabel.Name = "LocalVariablesListLabel";
            this.LocalVariablesListLabel.Size = new System.Drawing.Size(79, 13);
            this.LocalVariablesListLabel.TabIndex = 23;
            this.LocalVariablesListLabel.Text = "Local Variables";
            // 
            // LocalVariablesListBox
            // 
            this.LocalVariablesListBox.FormattingEnabled = true;
            this.LocalVariablesListBox.Location = new System.Drawing.Point(342, 17);
            this.LocalVariablesListBox.Name = "LocalVariablesListBox";
            this.LocalVariablesListBox.Size = new System.Drawing.Size(159, 173);
            this.LocalVariablesListBox.TabIndex = 22;
            // 
            // GlobalRemoveSelectedVariableButton
            // 
            this.GlobalRemoveSelectedVariableButton.Location = new System.Drawing.Point(182, 260);
            this.GlobalRemoveSelectedVariableButton.Name = "GlobalRemoveSelectedVariableButton";
            this.GlobalRemoveSelectedVariableButton.Size = new System.Drawing.Size(154, 26);
            this.GlobalRemoveSelectedVariableButton.TabIndex = 21;
            this.GlobalRemoveSelectedVariableButton.Text = "Remove Selected GloablVar";
            this.GlobalRemoveSelectedVariableButton.UseVisualStyleBackColor = true;
            this.GlobalRemoveSelectedVariableButton.Click += new System.EventHandler(this.GlobalRemoveSelectedVariableButton_Click);
            // 
            // RemoveSelectedFunctionButton
            // 
            this.RemoveSelectedFunctionButton.Location = new System.Drawing.Point(12, 260);
            this.RemoveSelectedFunctionButton.Name = "RemoveSelectedFunctionButton";
            this.RemoveSelectedFunctionButton.Size = new System.Drawing.Size(159, 26);
            this.RemoveSelectedFunctionButton.TabIndex = 20;
            this.RemoveSelectedFunctionButton.Text = "Remove Selected Function";
            this.RemoveSelectedFunctionButton.UseVisualStyleBackColor = true;
            this.RemoveSelectedFunctionButton.Click += new System.EventHandler(this.RemoveSelectedFunctionButton_Click);
            // 
            // GlobalVariablesListAddButton
            // 
            this.GlobalVariablesListAddButton.Location = new System.Drawing.Point(182, 234);
            this.GlobalVariablesListAddButton.Name = "GlobalVariablesListAddButton";
            this.GlobalVariablesListAddButton.Size = new System.Drawing.Size(154, 20);
            this.GlobalVariablesListAddButton.TabIndex = 16;
            this.GlobalVariablesListAddButton.Text = "Add";
            this.GlobalVariablesListAddButton.UseVisualStyleBackColor = true;
            this.GlobalVariablesListAddButton.Click += new System.EventHandler(this.GlobalVariablesListAddButton_Click);
            // 
            // GlobalVariablesListAddBox
            // 
            this.GlobalVariablesListAddBox.Location = new System.Drawing.Point(177, 203);
            this.GlobalVariablesListAddBox.Name = "GlobalVariablesListAddBox";
            this.GlobalVariablesListAddBox.Size = new System.Drawing.Size(159, 20);
            this.GlobalVariablesListAddBox.TabIndex = 15;
            // 
            // FunctionsListAddButton
            // 
            this.FunctionsListAddButton.Location = new System.Drawing.Point(14, 234);
            this.FunctionsListAddButton.Name = "FunctionsListAddButton";
            this.FunctionsListAddButton.Size = new System.Drawing.Size(157, 20);
            this.FunctionsListAddButton.TabIndex = 14;
            this.FunctionsListAddButton.Text = "Add";
            this.FunctionsListAddButton.UseVisualStyleBackColor = true;
            this.FunctionsListAddButton.Click += new System.EventHandler(this.FunctionsListAddButton_Click);
            // 
            // FunctionsListAddBox
            // 
            this.FunctionsListAddBox.Location = new System.Drawing.Point(12, 203);
            this.FunctionsListAddBox.Name = "FunctionsListAddBox";
            this.FunctionsListAddBox.Size = new System.Drawing.Size(159, 20);
            this.FunctionsListAddBox.TabIndex = 13;
            // 
            // GlobalVariablesListLabel
            // 
            this.GlobalVariablesListLabel.AutoSize = true;
            this.GlobalVariablesListLabel.Location = new System.Drawing.Point(177, 1);
            this.GlobalVariablesListLabel.Name = "GlobalVariablesListLabel";
            this.GlobalVariablesListLabel.Size = new System.Drawing.Size(83, 13);
            this.GlobalVariablesListLabel.TabIndex = 12;
            this.GlobalVariablesListLabel.Text = "Global Variables";
            // 
            // GlobalVariablesListBox
            // 
            this.GlobalVariablesListBox.FormattingEnabled = true;
            this.GlobalVariablesListBox.Location = new System.Drawing.Point(177, 17);
            this.GlobalVariablesListBox.Name = "GlobalVariablesListBox";
            this.GlobalVariablesListBox.Size = new System.Drawing.Size(159, 173);
            this.GlobalVariablesListBox.TabIndex = 11;
            // 
            // FunctionsListBox
            // 
            this.FunctionsListBox.FormattingEnabled = true;
            this.FunctionsListBox.Location = new System.Drawing.Point(12, 17);
            this.FunctionsListBox.Name = "FunctionsListBox";
            this.FunctionsListBox.Size = new System.Drawing.Size(159, 173);
            this.FunctionsListBox.TabIndex = 10;
            // 
            // FunctionsListLabel
            // 
            this.FunctionsListLabel.AutoSize = true;
            this.FunctionsListLabel.Location = new System.Drawing.Point(9, 1);
            this.FunctionsListLabel.Name = "FunctionsListLabel";
            this.FunctionsListLabel.Size = new System.Drawing.Size(53, 13);
            this.FunctionsListLabel.TabIndex = 9;
            this.FunctionsListLabel.Text = "Functions";
            // 
            // Pages
            // 
            this.Pages.Controls.Add(this.MainTab);
            this.Pages.Controls.Add(this.ServerTab);
            this.Pages.Location = new System.Drawing.Point(0, 0);
            this.Pages.Name = "Pages";
            this.Pages.SelectedIndex = 0;
            this.Pages.Size = new System.Drawing.Size(577, 712);
            this.Pages.TabIndex = 21;
            this.Pages.Tag = "";
            // 
            // MainTab
            // 
            this.MainTab.Controls.Add(this.panel2);
            this.MainTab.Controls.Add(this.RemovePboButton);
            this.MainTab.Controls.Add(this.AddPboTabButton);
            this.MainTab.Controls.Add(this.GitDirectoryPathBox);
            this.MainTab.Controls.Add(this.panel1);
            this.MainTab.Controls.Add(this.BrowseGitDirectory);
            this.MainTab.Controls.Add(this.PboFileBox);
            this.MainTab.Controls.Add(this.GitDirectoryLabel);
            this.MainTab.Controls.Add(this.FncTagBox);
            this.MainTab.Controls.Add(this.LaunchButton);
            this.MainTab.Controls.Add(this.FncTagLabel);
            this.MainTab.Controls.Add(this.VarLengthLabel);
            this.MainTab.Controls.Add(this.FncLengthCombo);
            this.MainTab.Controls.Add(this.FncLengthLabel);
            this.MainTab.Controls.Add(this.VarLengthCombo);
            this.MainTab.Location = new System.Drawing.Point(4, 22);
            this.MainTab.Name = "MainTab";
            this.MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainTab.Size = new System.Drawing.Size(569, 686);
            this.MainTab.TabIndex = 0;
            this.MainTab.Text = "Main Settings";
            this.MainTab.UseVisualStyleBackColor = true;
            // 
            // ServerTab
            // 
            this.ServerTab.Controls.Add(this.DefaultConsoleLogButton);
            this.ServerTab.Controls.Add(this.GenerateCmdPasswordButton);
            this.ServerTab.Controls.Add(this.GenerateAdminPasswordButton);
            this.ServerTab.Controls.Add(this.GenerateServerPasswordButton);
            this.ServerTab.Controls.Add(this.MotdIntervalLabel);
            this.ServerTab.Controls.Add(this.MotdIntervalBox);
            this.ServerTab.Controls.Add(this.MotdRemoveButton);
            this.ServerTab.Controls.Add(this.MotdAddButton);
            this.ServerTab.Controls.Add(this.MotdAddBox);
            this.ServerTab.Controls.Add(this.MotdBox);
            this.ServerTab.Controls.Add(this.MotdLabel);
            this.ServerTab.Controls.Add(this.ServerLogLabel);
            this.ServerTab.Controls.Add(this.ServerLogBox);
            this.ServerTab.Controls.Add(this.ServerCMDPasswordLabel);
            this.ServerTab.Controls.Add(this.ServerCMDPasswordBox);
            this.ServerTab.Controls.Add(this.AdminPasswordLLabel);
            this.ServerTab.Controls.Add(this.AdminPasswordBox);
            this.ServerTab.Controls.Add(this.ServerPasswordLabel);
            this.ServerTab.Controls.Add(this.ServerPasswordBox);
            this.ServerTab.Controls.Add(this.ServerDirectoryLabel);
            this.ServerTab.Controls.Add(this.ServerDirectoryPathBox);
            this.ServerTab.Controls.Add(this.BrowseServerDirectory);
            this.ServerTab.Controls.Add(this.HostNameLabel);
            this.ServerTab.Controls.Add(this.HostNameBox);
            this.ServerTab.Controls.Add(this.Use64BitArmaButton);
            this.ServerTab.Location = new System.Drawing.Point(4, 22);
            this.ServerTab.Name = "ServerTab";
            this.ServerTab.Padding = new System.Windows.Forms.Padding(3);
            this.ServerTab.Size = new System.Drawing.Size(569, 686);
            this.ServerTab.TabIndex = 1;
            this.ServerTab.Text = "Server Settings";
            this.ServerTab.UseVisualStyleBackColor = true;
            // 
            // DefaultConsoleLogButton
            // 
            this.DefaultConsoleLogButton.Location = new System.Drawing.Point(171, 228);
            this.DefaultConsoleLogButton.Name = "DefaultConsoleLogButton";
            this.DefaultConsoleLogButton.Size = new System.Drawing.Size(68, 20);
            this.DefaultConsoleLogButton.TabIndex = 39;
            this.DefaultConsoleLogButton.Text = "Default";
            this.DefaultConsoleLogButton.UseVisualStyleBackColor = true;
            this.DefaultConsoleLogButton.Click += new System.EventHandler(this.DefaultConsoleLogButton_Click);
            // 
            // GenerateCmdPasswordButton
            // 
            this.GenerateCmdPasswordButton.Location = new System.Drawing.Point(171, 193);
            this.GenerateCmdPasswordButton.Name = "GenerateCmdPasswordButton";
            this.GenerateCmdPasswordButton.Size = new System.Drawing.Size(68, 20);
            this.GenerateCmdPasswordButton.TabIndex = 38;
            this.GenerateCmdPasswordButton.Text = "Generate";
            this.GenerateCmdPasswordButton.UseVisualStyleBackColor = true;
            this.GenerateCmdPasswordButton.Click += new System.EventHandler(this.GenerateCmdPasswordButton_Click);
            // 
            // GenerateAdminPasswordButton
            // 
            this.GenerateAdminPasswordButton.Location = new System.Drawing.Point(171, 155);
            this.GenerateAdminPasswordButton.Name = "GenerateAdminPasswordButton";
            this.GenerateAdminPasswordButton.Size = new System.Drawing.Size(68, 20);
            this.GenerateAdminPasswordButton.TabIndex = 37;
            this.GenerateAdminPasswordButton.Text = "Generate";
            this.GenerateAdminPasswordButton.UseVisualStyleBackColor = true;
            this.GenerateAdminPasswordButton.Click += new System.EventHandler(this.GenerateAdminPasswordButton_Click);
            // 
            // GenerateServerPasswordButton
            // 
            this.GenerateServerPasswordButton.Location = new System.Drawing.Point(171, 116);
            this.GenerateServerPasswordButton.Name = "GenerateServerPasswordButton";
            this.GenerateServerPasswordButton.Size = new System.Drawing.Size(68, 20);
            this.GenerateServerPasswordButton.TabIndex = 36;
            this.GenerateServerPasswordButton.Text = "Generate";
            this.GenerateServerPasswordButton.UseVisualStyleBackColor = true;
            this.GenerateServerPasswordButton.Click += new System.EventHandler(this.GenerateServerPasswordButton_Click);
            // 
            // MotdIntervalLabel
            // 
            this.MotdIntervalLabel.AutoSize = true;
            this.MotdIntervalLabel.Location = new System.Drawing.Point(506, 177);
            this.MotdIntervalLabel.Name = "MotdIntervalLabel";
            this.MotdIntervalLabel.Size = new System.Drawing.Size(45, 13);
            this.MotdIntervalLabel.TabIndex = 35;
            this.MotdIntervalLabel.Text = " Interval";
            // 
            // MotdIntervalBox
            // 
            this.MotdIntervalBox.Location = new System.Drawing.Point(509, 196);
            this.MotdIntervalBox.Name = "MotdIntervalBox";
            this.MotdIntervalBox.Size = new System.Drawing.Size(48, 20);
            this.MotdIntervalBox.TabIndex = 34;
            this.MotdIntervalBox.TextChanged += new System.EventHandler(this.MotdIntervalBox_TextChanged);
            // 
            // MotdRemoveButton
            // 
            this.MotdRemoveButton.Location = new System.Drawing.Point(254, 222);
            this.MotdRemoveButton.Name = "MotdRemoveButton";
            this.MotdRemoveButton.Size = new System.Drawing.Size(303, 20);
            this.MotdRemoveButton.TabIndex = 33;
            this.MotdRemoveButton.Text = "Remove Selected Message";
            this.MotdRemoveButton.UseVisualStyleBackColor = true;
            this.MotdRemoveButton.Click += new System.EventHandler(this.MotdRemoveButton_Click);
            // 
            // MotdAddButton
            // 
            this.MotdAddButton.Location = new System.Drawing.Point(254, 196);
            this.MotdAddButton.Name = "MotdAddButton";
            this.MotdAddButton.Size = new System.Drawing.Size(246, 20);
            this.MotdAddButton.TabIndex = 32;
            this.MotdAddButton.Text = "Add";
            this.MotdAddButton.UseVisualStyleBackColor = true;
            this.MotdAddButton.Click += new System.EventHandler(this.MotdAddButton_Click);
            // 
            // MotdAddBox
            // 
            this.MotdAddBox.Location = new System.Drawing.Point(254, 170);
            this.MotdAddBox.Name = "MotdAddBox";
            this.MotdAddBox.Size = new System.Drawing.Size(246, 20);
            this.MotdAddBox.TabIndex = 31;
            // 
            // MotdBox
            // 
            this.MotdBox.FormattingEnabled = true;
            this.MotdBox.Location = new System.Drawing.Point(254, 69);
            this.MotdBox.Name = "MotdBox";
            this.MotdBox.Size = new System.Drawing.Size(303, 95);
            this.MotdBox.TabIndex = 30;
            // 
            // MotdLabel
            // 
            this.MotdLabel.AutoSize = true;
            this.MotdLabel.Location = new System.Drawing.Point(251, 53);
            this.MotdLabel.Name = "MotdLabel";
            this.MotdLabel.Size = new System.Drawing.Size(100, 13);
            this.MotdLabel.TabIndex = 29;
            this.MotdLabel.Text = "Message of the day";
            // 
            // ServerLogLabel
            // 
            this.ServerLogLabel.AutoSize = true;
            this.ServerLogLabel.Location = new System.Drawing.Point(5, 213);
            this.ServerLogLabel.Name = "ServerLogLabel";
            this.ServerLogLabel.Size = new System.Drawing.Size(100, 13);
            this.ServerLogLabel.TabIndex = 28;
            this.ServerLogLabel.Text = "Server Console Log";
            // 
            // ServerLogBox
            // 
            this.ServerLogBox.Location = new System.Drawing.Point(8, 229);
            this.ServerLogBox.Name = "ServerLogBox";
            this.ServerLogBox.Size = new System.Drawing.Size(157, 20);
            this.ServerLogBox.TabIndex = 27;
            this.ServerLogBox.TextChanged += new System.EventHandler(this.ServerLogBox_TextChanged);
            // 
            // ServerCMDPasswordLabel
            // 
            this.ServerCMDPasswordLabel.AutoSize = true;
            this.ServerCMDPasswordLabel.Location = new System.Drawing.Point(5, 177);
            this.ServerCMDPasswordLabel.Name = "ServerCMDPasswordLabel";
            this.ServerCMDPasswordLabel.Size = new System.Drawing.Size(103, 13);
            this.ServerCMDPasswordLabel.TabIndex = 26;
            this.ServerCMDPasswordLabel.Text = "Command Password";
            // 
            // ServerCMDPasswordBox
            // 
            this.ServerCMDPasswordBox.Location = new System.Drawing.Point(8, 193);
            this.ServerCMDPasswordBox.Name = "ServerCMDPasswordBox";
            this.ServerCMDPasswordBox.Size = new System.Drawing.Size(157, 20);
            this.ServerCMDPasswordBox.TabIndex = 25;
            this.ServerCMDPasswordBox.TextChanged += new System.EventHandler(this.ServerCMDPasswordBox_TextChanged);
            // 
            // AdminPasswordLLabel
            // 
            this.AdminPasswordLLabel.AutoSize = true;
            this.AdminPasswordLLabel.Location = new System.Drawing.Point(5, 139);
            this.AdminPasswordLLabel.Name = "AdminPasswordLLabel";
            this.AdminPasswordLLabel.Size = new System.Drawing.Size(85, 13);
            this.AdminPasswordLLabel.TabIndex = 24;
            this.AdminPasswordLLabel.Text = "Admin Password";
            // 
            // AdminPasswordBox
            // 
            this.AdminPasswordBox.Location = new System.Drawing.Point(8, 155);
            this.AdminPasswordBox.Name = "AdminPasswordBox";
            this.AdminPasswordBox.Size = new System.Drawing.Size(157, 20);
            this.AdminPasswordBox.TabIndex = 23;
            this.AdminPasswordBox.TextChanged += new System.EventHandler(this.AdminPasswordBox_TextChanged);
            // 
            // ServerPasswordLabel
            // 
            this.ServerPasswordLabel.AutoSize = true;
            this.ServerPasswordLabel.Location = new System.Drawing.Point(5, 100);
            this.ServerPasswordLabel.Name = "ServerPasswordLabel";
            this.ServerPasswordLabel.Size = new System.Drawing.Size(87, 13);
            this.ServerPasswordLabel.TabIndex = 22;
            this.ServerPasswordLabel.Text = "Server Password";
            // 
            // ServerPasswordBox
            // 
            this.ServerPasswordBox.Location = new System.Drawing.Point(8, 116);
            this.ServerPasswordBox.Name = "ServerPasswordBox";
            this.ServerPasswordBox.Size = new System.Drawing.Size(157, 20);
            this.ServerPasswordBox.TabIndex = 21;
            this.ServerPasswordBox.TextChanged += new System.EventHandler(this.ServerPasswordBox_TextChanged);
            // 
            // ServerDirectoryLabel
            // 
            this.ServerDirectoryLabel.AutoSize = true;
            this.ServerDirectoryLabel.Location = new System.Drawing.Point(5, 13);
            this.ServerDirectoryLabel.Name = "ServerDirectoryLabel";
            this.ServerDirectoryLabel.Size = new System.Drawing.Size(83, 13);
            this.ServerDirectoryLabel.TabIndex = 20;
            this.ServerDirectoryLabel.Text = "Server Directory";
            // 
            // ServerDirectoryPathBox
            // 
            this.ServerDirectoryPathBox.Location = new System.Drawing.Point(8, 29);
            this.ServerDirectoryPathBox.Name = "ServerDirectoryPathBox";
            this.ServerDirectoryPathBox.Size = new System.Drawing.Size(370, 20);
            this.ServerDirectoryPathBox.TabIndex = 18;
            this.ServerDirectoryPathBox.TextChanged += new System.EventHandler(this.ServerDirectoryPathBox_TextChanged);
            // 
            // BrowseServerDirectory
            // 
            this.BrowseServerDirectory.Location = new System.Drawing.Point(384, 29);
            this.BrowseServerDirectory.Name = "BrowseServerDirectory";
            this.BrowseServerDirectory.Size = new System.Drawing.Size(86, 20);
            this.BrowseServerDirectory.TabIndex = 19;
            this.BrowseServerDirectory.Text = "Browse";
            this.BrowseServerDirectory.UseVisualStyleBackColor = true;
            this.BrowseServerDirectory.Click += new System.EventHandler(this.BrowseServerDirectory_Click);
            // 
            // HostNameLabel
            // 
            this.HostNameLabel.AutoSize = true;
            this.HostNameLabel.Location = new System.Drawing.Point(5, 61);
            this.HostNameLabel.Name = "HostNameLabel";
            this.HostNameLabel.Size = new System.Drawing.Size(60, 13);
            this.HostNameLabel.TabIndex = 17;
            this.HostNameLabel.Text = "Host Name";
            // 
            // HostNameBox
            // 
            this.HostNameBox.Location = new System.Drawing.Point(8, 77);
            this.HostNameBox.Name = "HostNameBox";
            this.HostNameBox.Size = new System.Drawing.Size(157, 20);
            this.HostNameBox.TabIndex = 16;
            this.HostNameBox.TextChanged += new System.EventHandler(this.HostNameBox_TextChanged);
            // 
            // Use64BitArmaButton
            // 
            this.Use64BitArmaButton.AutoSize = true;
            this.Use64BitArmaButton.Location = new System.Drawing.Point(480, 32);
            this.Use64BitArmaButton.Name = "Use64BitArmaButton";
            this.Use64BitArmaButton.Size = new System.Drawing.Size(74, 17);
            this.Use64BitArmaButton.TabIndex = 15;
            this.Use64BitArmaButton.Text = "64Bit EXE";
            this.Use64BitArmaButton.UseVisualStyleBackColor = true;
            this.Use64BitArmaButton.CheckedChanged += new System.EventHandler(this.Use64BitArmaButton_CheckedChanged);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(575, 711);
            this.Controls.Add(this.Pages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(555, 728);
            this.Name = "Home";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ArmaServerCQC";
            this.PboFileBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.Pages.ResumeLayout(false);
            this.MainTab.ResumeLayout(false);
            this.MainTab.PerformLayout();
            this.ServerTab.ResumeLayout(false);
            this.ServerTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BrowseGitDirectory;
        private System.Windows.Forms.TextBox GitDirectoryPathBox;
        private System.Windows.Forms.Label GitDirectoryLabel;
        private System.Windows.Forms.Label FncTagLabel;
        private System.Windows.Forms.TextBox FncTagBox;
        private System.Windows.Forms.ComboBox FncLengthCombo;
        private System.Windows.Forms.ComboBox VarLengthCombo;
        private System.Windows.Forms.Label FncLengthLabel;
        private System.Windows.Forms.Label VarLengthLabel;
        private System.Windows.Forms.Button LaunchButton;
        private System.Windows.Forms.TabPage PboStartTab;
        private System.Windows.Forms.TabControl PboFileBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PboNameBox;
        private System.Windows.Forms.Label GitPathLabel;
        private System.Windows.Forms.TextBox GitPathBox;
        private System.Windows.Forms.Label GitTokenLabel;
        private System.Windows.Forms.TextBox GitTokenBox;
        private System.Windows.Forms.Label GitTypeLabel;
        private System.Windows.Forms.ComboBox GitTypeCombo;
        private System.Windows.Forms.Label GitUrlLabel;
        private System.Windows.Forms.TextBox GitUrlBox;
        private System.Windows.Forms.Label PboServerPathLabel;
        private System.Windows.Forms.Button PboServerPathButton;
        private System.Windows.Forms.TextBox PboServerPathBox;
        private System.Windows.Forms.CheckBox RenameLocalVarsButton;
        private System.Windows.Forms.CheckBox RenameGlobalVarsButton;
        private System.Windows.Forms.CheckBox RenameFuncsButton;
        private System.Windows.Forms.CheckBox OneLineButton;
        private System.Windows.Forms.Button AddPboTabButton;
        private System.Windows.Forms.Button RemovePboButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button GlobalVariablesListAddButton;
        private System.Windows.Forms.TextBox GlobalVariablesListAddBox;
        private System.Windows.Forms.Button FunctionsListAddButton;
        private System.Windows.Forms.TextBox FunctionsListAddBox;
        private System.Windows.Forms.Label GlobalVariablesListLabel;
        private System.Windows.Forms.ListBox GlobalVariablesListBox;
        private System.Windows.Forms.ListBox FunctionsListBox;
        private System.Windows.Forms.Label FunctionsListLabel;
        private System.Windows.Forms.Button GlobalRemoveSelectedVariableButton;
        private System.Windows.Forms.Button RemoveSelectedFunctionButton;
        private System.Windows.Forms.Button LocalRemoveSelectedVariableButton;
        private System.Windows.Forms.Button LocalVariablesListAddButton;
        private System.Windows.Forms.TextBox LocalVariablesListAddBox;
        private System.Windows.Forms.Label LocalVariablesListLabel;
        private System.Windows.Forms.ListBox LocalVariablesListBox;
        private System.Windows.Forms.TabControl Pages;
        private System.Windows.Forms.TabPage MainTab;
        private System.Windows.Forms.TabPage ServerTab;
        private System.Windows.Forms.CheckBox Use64BitArmaButton;
        private System.Windows.Forms.Label HostNameLabel;
        private System.Windows.Forms.TextBox HostNameBox;
        private System.Windows.Forms.Label ServerDirectoryLabel;
        private System.Windows.Forms.TextBox ServerDirectoryPathBox;
        private System.Windows.Forms.Button BrowseServerDirectory;
        private System.Windows.Forms.Label ServerPasswordLabel;
        private System.Windows.Forms.TextBox ServerPasswordBox;
        private System.Windows.Forms.Label ServerCMDPasswordLabel;
        private System.Windows.Forms.TextBox ServerCMDPasswordBox;
        private System.Windows.Forms.Label AdminPasswordLLabel;
        private System.Windows.Forms.TextBox AdminPasswordBox;
        private System.Windows.Forms.Label ServerLogLabel;
        private System.Windows.Forms.TextBox ServerLogBox;
        private System.Windows.Forms.Button MotdRemoveButton;
        private System.Windows.Forms.Button MotdAddButton;
        private System.Windows.Forms.TextBox MotdAddBox;
        private System.Windows.Forms.ListBox MotdBox;
        private System.Windows.Forms.Label MotdLabel;
        private System.Windows.Forms.Label MotdIntervalLabel;
        private System.Windows.Forms.TextBox MotdIntervalBox;
        private System.Windows.Forms.Button DefaultConsoleLogButton;
        private System.Windows.Forms.Button GenerateCmdPasswordButton;
        private System.Windows.Forms.Button GenerateAdminPasswordButton;
        private System.Windows.Forms.Button GenerateServerPasswordButton;
    }
}

