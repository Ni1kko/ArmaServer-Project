namespace ArmaServerFrontend
{
    partial class Home
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
            this.KillArmaCheckBox = new System.Windows.Forms.CheckBox();
            this.ServerDirectoryPathBox = new System.Windows.Forms.TextBox();
            this.BrowseServerDirectory = new System.Windows.Forms.Button();
            this.BrowseGitDirectory = new System.Windows.Forms.Button();
            this.GitDirectoryPathBox = new System.Windows.Forms.TextBox();
            this.GitDirectoryLabel = new System.Windows.Forms.Label();
            this.ServerDirectoryLabel = new System.Windows.Forms.Label();
            this.FncTagLabel = new System.Windows.Forms.Label();
            this.FncTagBox = new System.Windows.Forms.TextBox();
            this.FncLengthCombo = new System.Windows.Forms.ComboBox();
            this.VarLengthCombo = new System.Windows.Forms.ComboBox();
            this.FncLengthLabel = new System.Windows.Forms.Label();
            this.VarLengthLabel = new System.Windows.Forms.Label();
            this.LaunchButton = new System.Windows.Forms.Button();
            this.Use64BitArmaButton = new System.Windows.Forms.CheckBox();
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
            this.GitPathBrowse = new System.Windows.Forms.Button();
            this.GitPathBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PboNameBox = new System.Windows.Forms.TextBox();
            this.PboFileBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // KillArmaCheckBox
            // 
            this.KillArmaCheckBox.AutoSize = true;
            this.KillArmaCheckBox.Location = new System.Drawing.Point(16, 116);
            this.KillArmaCheckBox.Name = "KillArmaCheckBox";
            this.KillArmaCheckBox.Size = new System.Drawing.Size(81, 17);
            this.KillArmaCheckBox.TabIndex = 0;
            this.KillArmaCheckBox.Text = "Stop Arma3";
            this.KillArmaCheckBox.UseVisualStyleBackColor = true;
            this.KillArmaCheckBox.CheckedChanged += new System.EventHandler(this.KillArmaCheckBox_CheckedChanged);
            // 
            // ServerDirectoryPathBox
            // 
            this.ServerDirectoryPathBox.Location = new System.Drawing.Point(15, 25);
            this.ServerDirectoryPathBox.Name = "ServerDirectoryPathBox";
            this.ServerDirectoryPathBox.Size = new System.Drawing.Size(379, 20);
            this.ServerDirectoryPathBox.TabIndex = 1;
            this.ServerDirectoryPathBox.TextChanged += new System.EventHandler(this.ServerDirectoryPathBox_TextChanged);
            // 
            // BrowseServerDirectory
            // 
            this.BrowseServerDirectory.Location = new System.Drawing.Point(400, 25);
            this.BrowseServerDirectory.Name = "BrowseServerDirectory";
            this.BrowseServerDirectory.Size = new System.Drawing.Size(86, 20);
            this.BrowseServerDirectory.TabIndex = 2;
            this.BrowseServerDirectory.Text = "Browse";
            this.BrowseServerDirectory.UseVisualStyleBackColor = true;
            this.BrowseServerDirectory.Click += new System.EventHandler(this.BrowseServerDirectory_Click);
            // 
            // BrowseGitDirectory
            // 
            this.BrowseGitDirectory.Location = new System.Drawing.Point(400, 74);
            this.BrowseGitDirectory.Name = "BrowseGitDirectory";
            this.BrowseGitDirectory.Size = new System.Drawing.Size(86, 20);
            this.BrowseGitDirectory.TabIndex = 4;
            this.BrowseGitDirectory.Text = "Browse";
            this.BrowseGitDirectory.UseVisualStyleBackColor = true;
            this.BrowseGitDirectory.Click += new System.EventHandler(this.BrowseGitDirectory_Click);
            // 
            // GitDirectoryPathBox
            // 
            this.GitDirectoryPathBox.Location = new System.Drawing.Point(15, 74);
            this.GitDirectoryPathBox.Name = "GitDirectoryPathBox";
            this.GitDirectoryPathBox.Size = new System.Drawing.Size(379, 20);
            this.GitDirectoryPathBox.TabIndex = 3;
            this.GitDirectoryPathBox.TextChanged += new System.EventHandler(this.GitDirectoryPathBox_TextChanged);
            // 
            // GitDirectoryLabel
            // 
            this.GitDirectoryLabel.AutoSize = true;
            this.GitDirectoryLabel.Location = new System.Drawing.Point(12, 58);
            this.GitDirectoryLabel.Name = "GitDirectoryLabel";
            this.GitDirectoryLabel.Size = new System.Drawing.Size(62, 13);
            this.GitDirectoryLabel.TabIndex = 5;
            this.GitDirectoryLabel.Text = "GitDirectory";
            // 
            // ServerDirectoryLabel
            // 
            this.ServerDirectoryLabel.AutoSize = true;
            this.ServerDirectoryLabel.Location = new System.Drawing.Point(12, 9);
            this.ServerDirectoryLabel.Name = "ServerDirectoryLabel";
            this.ServerDirectoryLabel.Size = new System.Drawing.Size(80, 13);
            this.ServerDirectoryLabel.TabIndex = 6;
            this.ServerDirectoryLabel.Text = "ServerDirectory";
            // 
            // FncTagLabel
            // 
            this.FncTagLabel.AutoSize = true;
            this.FncTagLabel.Location = new System.Drawing.Point(180, 129);
            this.FncTagLabel.Name = "FncTagLabel";
            this.FncTagLabel.Size = new System.Drawing.Size(70, 13);
            this.FncTagLabel.TabIndex = 8;
            this.FncTagLabel.Text = "Function Tag";
            // 
            // FncTagBox
            // 
            this.FncTagBox.Location = new System.Drawing.Point(183, 145);
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
            this.FncLengthCombo.Location = new System.Drawing.Point(287, 144);
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
            this.VarLengthCombo.Location = new System.Drawing.Point(400, 145);
            this.VarLengthCombo.Name = "VarLengthCombo";
            this.VarLengthCombo.Size = new System.Drawing.Size(83, 21);
            this.VarLengthCombo.TabIndex = 10;
            this.VarLengthCombo.ValueMember = "1";
            this.VarLengthCombo.SelectedValueChanged += new System.EventHandler(this.VarLengthCombo_SelectedValueChanged);
            // 
            // FncLengthLabel
            // 
            this.FncLengthLabel.AutoSize = true;
            this.FncLengthLabel.Location = new System.Drawing.Point(284, 128);
            this.FncLengthLabel.Name = "FncLengthLabel";
            this.FncLengthLabel.Size = new System.Drawing.Size(89, 13);
            this.FncLengthLabel.TabIndex = 11;
            this.FncLengthLabel.Text = "Functions Length";
            // 
            // VarLengthLabel
            // 
            this.VarLengthLabel.AutoSize = true;
            this.VarLengthLabel.Location = new System.Drawing.Point(397, 129);
            this.VarLengthLabel.Name = "VarLengthLabel";
            this.VarLengthLabel.Size = new System.Drawing.Size(86, 13);
            this.VarLengthLabel.TabIndex = 12;
            this.VarLengthLabel.Text = "Variables Length";
            // 
            // LaunchButton
            // 
            this.LaunchButton.Location = new System.Drawing.Point(223, 432);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(75, 23);
            this.LaunchButton.TabIndex = 13;
            this.LaunchButton.Text = "Start";
            this.LaunchButton.UseVisualStyleBackColor = true;
            this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // Use64BitArmaButton
            // 
            this.Use64BitArmaButton.AutoSize = true;
            this.Use64BitArmaButton.Location = new System.Drawing.Point(16, 149);
            this.Use64BitArmaButton.Name = "Use64BitArmaButton";
            this.Use64BitArmaButton.Size = new System.Drawing.Size(83, 17);
            this.Use64BitArmaButton.TabIndex = 14;
            this.Use64BitArmaButton.Text = "Arma3 64Bit";
            this.Use64BitArmaButton.UseVisualStyleBackColor = true;
            this.Use64BitArmaButton.CheckedChanged += new System.EventHandler(this.Use64BitArmaButton_CheckedChanged);
            // 
            // PboStartTab
            // 
            this.PboStartTab.Location = new System.Drawing.Point(4, 22);
            this.PboStartTab.Name = "PboStartTab";
            this.PboStartTab.Padding = new System.Windows.Forms.Padding(3);
            this.PboStartTab.Size = new System.Drawing.Size(806, 0);
            this.PboStartTab.TabIndex = 0;
            this.PboStartTab.Text = "tabPage1";
            this.PboStartTab.UseVisualStyleBackColor = true;
            // 
            // PboFileBox
            // 
            this.PboFileBox.Controls.Add(this.PboStartTab);
            this.PboFileBox.Location = new System.Drawing.Point(15, 182);
            this.PboFileBox.Name = "PboFileBox";
            this.PboFileBox.SelectedIndex = 0;
            this.PboFileBox.Size = new System.Drawing.Size(814, 20);
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
            this.panel1.Controls.Add(this.GitPathBrowse);
            this.panel1.Controls.Add(this.GitPathBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PboNameBox);
            this.panel1.Location = new System.Drawing.Point(15, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 216);
            this.panel1.TabIndex = 17;
            // 
            // OneLineButton
            // 
            this.OneLineButton.AutoSize = true;
            this.OneLineButton.Location = new System.Drawing.Point(388, 180);
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
            this.RenameLocalVarsButton.Location = new System.Drawing.Point(266, 180);
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
            this.RenameGlobalVarsButton.Location = new System.Drawing.Point(140, 180);
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
            this.RenameFuncsButton.Location = new System.Drawing.Point(14, 180);
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
            this.PboServerPathLabel.Location = new System.Drawing.Point(11, 125);
            this.PboServerPathLabel.Name = "PboServerPathLabel";
            this.PboServerPathLabel.Size = new System.Drawing.Size(105, 13);
            this.PboServerPathLabel.TabIndex = 19;
            this.PboServerPathLabel.Text = "Pbo Server Directory";
            // 
            // PboServerPathButton
            // 
            this.PboServerPathButton.Location = new System.Drawing.Point(388, 140);
            this.PboServerPathButton.Name = "PboServerPathButton";
            this.PboServerPathButton.Size = new System.Drawing.Size(86, 20);
            this.PboServerPathButton.TabIndex = 18;
            this.PboServerPathButton.Text = "Browse";
            this.PboServerPathButton.UseVisualStyleBackColor = true;
            this.PboServerPathButton.Click += new System.EventHandler(this.PboServerPathButton_Click);
            // 
            // PboServerPathBox
            // 
            this.PboServerPathBox.Location = new System.Drawing.Point(14, 141);
            this.PboServerPathBox.Name = "PboServerPathBox";
            this.PboServerPathBox.Size = new System.Drawing.Size(368, 20);
            this.PboServerPathBox.TabIndex = 17;
            this.PboServerPathBox.TextChanged += new System.EventHandler(this.PboServerPathBox_TextChanged);
            // 
            // GitUrlLabel
            // 
            this.GitUrlLabel.AutoSize = true;
            this.GitUrlLabel.Location = new System.Drawing.Point(11, 45);
            this.GitUrlLabel.Name = "GitUrlLabel";
            this.GitUrlLabel.Size = new System.Drawing.Size(33, 13);
            this.GitUrlLabel.TabIndex = 16;
            this.GitUrlLabel.Text = "GitUrl";
            // 
            // GitUrlBox
            // 
            this.GitUrlBox.Location = new System.Drawing.Point(14, 61);
            this.GitUrlBox.Name = "GitUrlBox";
            this.GitUrlBox.Size = new System.Drawing.Size(460, 20);
            this.GitUrlBox.TabIndex = 15;
            this.GitUrlBox.TextChanged += new System.EventHandler(this.GitUrlBox_TextChanged);
            // 
            // GitTypeLabel
            // 
            this.GitTypeLabel.AutoSize = true;
            this.GitTypeLabel.Location = new System.Drawing.Point(430, 4);
            this.GitTypeLabel.Name = "GitTypeLabel";
            this.GitTypeLabel.Size = new System.Drawing.Size(44, 13);
            this.GitTypeLabel.TabIndex = 14;
            this.GitTypeLabel.Text = "GitType";
            // 
            // GitTypeCombo
            // 
            this.GitTypeCombo.DisplayMember = "1";
            this.GitTypeCombo.FormattingEnabled = true;
            this.GitTypeCombo.Items.AddRange(new object[] {
            "1",
            "2"});
            this.GitTypeCombo.Location = new System.Drawing.Point(433, 20);
            this.GitTypeCombo.Name = "GitTypeCombo";
            this.GitTypeCombo.Size = new System.Drawing.Size(41, 21);
            this.GitTypeCombo.TabIndex = 13;
            this.GitTypeCombo.ValueMember = "1";
            this.GitTypeCombo.SelectedValueChanged += new System.EventHandler(this.GitTypeCombo_SelectedValueChanged);
            // 
            // GitTokenLabel
            // 
            this.GitTokenLabel.AutoSize = true;
            this.GitTokenLabel.Location = new System.Drawing.Point(206, 6);
            this.GitTokenLabel.Name = "GitTokenLabel";
            this.GitTokenLabel.Size = new System.Drawing.Size(51, 13);
            this.GitTokenLabel.TabIndex = 12;
            this.GitTokenLabel.Text = "GitToken";
            // 
            // GitTokenBox
            // 
            this.GitTokenBox.Location = new System.Drawing.Point(208, 21);
            this.GitTokenBox.Name = "GitTokenBox";
            this.GitTokenBox.Size = new System.Drawing.Size(216, 20);
            this.GitTokenBox.TabIndex = 11;
            this.GitTokenBox.TextChanged += new System.EventHandler(this.GitTokenBox_TextChanged);
            // 
            // GitPathLabel
            // 
            this.GitPathLabel.AutoSize = true;
            this.GitPathLabel.Location = new System.Drawing.Point(11, 84);
            this.GitPathLabel.Name = "GitPathLabel";
            this.GitPathLabel.Size = new System.Drawing.Size(42, 13);
            this.GitPathLabel.TabIndex = 10;
            this.GitPathLabel.Text = "GitPath";
            // 
            // GitPathBrowse
            // 
            this.GitPathBrowse.Location = new System.Drawing.Point(388, 99);
            this.GitPathBrowse.Name = "GitPathBrowse";
            this.GitPathBrowse.Size = new System.Drawing.Size(86, 20);
            this.GitPathBrowse.TabIndex = 9;
            this.GitPathBrowse.Text = "Browse";
            this.GitPathBrowse.UseVisualStyleBackColor = true;
            this.GitPathBrowse.Click += new System.EventHandler(this.GitPathBrowse_Click);
            // 
            // GitPathBox
            // 
            this.GitPathBox.Location = new System.Drawing.Point(14, 100);
            this.GitPathBox.Name = "GitPathBox";
            this.GitPathBox.Size = new System.Drawing.Size(368, 20);
            this.GitPathBox.TabIndex = 8;
            this.GitPathBox.TextChanged += new System.EventHandler(this.GitPathBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Pbo name";
            // 
            // PboNameBox
            // 
            this.PboNameBox.Location = new System.Drawing.Point(14, 22);
            this.PboNameBox.Name = "PboNameBox";
            this.PboNameBox.Size = new System.Drawing.Size(179, 20);
            this.PboNameBox.TabIndex = 6;
            this.PboNameBox.TextChanged += new System.EventHandler(this.PboNameBox_TextChanged);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 463);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PboFileBox);
            this.Controls.Add(this.Use64BitArmaButton);
            this.Controls.Add(this.LaunchButton);
            this.Controls.Add(this.VarLengthLabel);
            this.Controls.Add(this.FncLengthLabel);
            this.Controls.Add(this.VarLengthCombo);
            this.Controls.Add(this.FncLengthCombo);
            this.Controls.Add(this.FncTagLabel);
            this.Controls.Add(this.FncTagBox);
            this.Controls.Add(this.ServerDirectoryLabel);
            this.Controls.Add(this.GitDirectoryLabel);
            this.Controls.Add(this.BrowseGitDirectory);
            this.Controls.Add(this.GitDirectoryPathBox);
            this.Controls.Add(this.BrowseServerDirectory);
            this.Controls.Add(this.ServerDirectoryPathBox);
            this.Controls.Add(this.KillArmaCheckBox);
            this.Name = "Home";
            this.Text = "ArmaServerCQC";
            this.PboFileBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox KillArmaCheckBox;
        private System.Windows.Forms.TextBox ServerDirectoryPathBox;
        private System.Windows.Forms.Button BrowseServerDirectory;
        private System.Windows.Forms.Button BrowseGitDirectory;
        private System.Windows.Forms.TextBox GitDirectoryPathBox;
        private System.Windows.Forms.Label GitDirectoryLabel;
        private System.Windows.Forms.Label ServerDirectoryLabel;
        private System.Windows.Forms.Label FncTagLabel;
        private System.Windows.Forms.TextBox FncTagBox;
        private System.Windows.Forms.ComboBox FncLengthCombo;
        private System.Windows.Forms.ComboBox VarLengthCombo;
        private System.Windows.Forms.Label FncLengthLabel;
        private System.Windows.Forms.Label VarLengthLabel;
        private System.Windows.Forms.Button LaunchButton;
        private System.Windows.Forms.CheckBox Use64BitArmaButton;
        private System.Windows.Forms.TabPage PboStartTab;
        private System.Windows.Forms.TabControl PboFileBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PboNameBox;
        private System.Windows.Forms.Label GitPathLabel;
        private System.Windows.Forms.Button GitPathBrowse;
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
    }
}

