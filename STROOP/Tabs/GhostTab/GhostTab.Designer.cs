namespace STROOP.Tabs.GhostTab
{
    partial class GhostTab
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBoxGhosts = new System.Windows.Forms.GroupBox();
            this.listBoxGhosts = new System.Windows.Forms.ListBox();
            this.buttonMarioColor = new System.Windows.Forms.Button();
            this.buttonWatchGhostFile = new System.Windows.Forms.Button();
            this.buttonLoadGhost = new System.Windows.Forms.Button();
            this.groupBoxGhostInfo = new System.Windows.Forms.GroupBox();
            this.checkTransparentGhosts = new System.Windows.Forms.CheckBox();
            this.buttonGhostColor = new System.Windows.Forms.Button();
            this.textBoxGhostName = new System.Windows.Forms.TextBox();
            this.labelNumFrames = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelGhostFile = new System.Windows.Forms.Label();
            this.labelGhostPlaybackStart = new System.Windows.Forms.Label();
            this.labelBaseGlobalTimer = new System.Windows.Forms.Label();
            this.numericUpDownStartOfPlayback = new System.Windows.Forms.NumericUpDown();
            this.buttonSaveGhost = new System.Windows.Forms.Button();
            this.buttonTutorialRecord = new System.Windows.Forms.Button();
            this.groupGhostHack = new System.Windows.Forms.GroupBox();
            this.labelHackActiveState = new System.Windows.Forms.Label();
            this.buttonDisableGhostHack = new System.Windows.Forms.Button();
            this.buttonEnableGhostHack = new System.Windows.Forms.Button();
            this.watchVariablePanelGhost = new STROOP.Controls.WatchVariableFlowLayoutPanel();
            this.groupBoxVariables = new System.Windows.Forms.GroupBox();
            this.groupBoxHelp = new System.Windows.Forms.GroupBox();
            this.buttonTutorialFileWatch = new System.Windows.Forms.Button();
            this.buttonTutorialPlayback = new System.Windows.Forms.Button();
            this.buttonTutorialNotes = new System.Windows.Forms.Button();
            this.groupBoxGhosts.SuspendLayout();
            this.groupBoxGhostInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartOfPlayback)).BeginInit();
            this.groupGhostHack.SuspendLayout();
            this.groupBoxVariables.SuspendLayout();
            this.groupBoxHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxGhosts
            // 
            this.groupBoxGhosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGhosts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxGhosts.Controls.Add(this.listBoxGhosts);
            this.groupBoxGhosts.Controls.Add(this.buttonMarioColor);
            this.groupBoxGhosts.Controls.Add(this.buttonWatchGhostFile);
            this.groupBoxGhosts.Controls.Add(this.buttonLoadGhost);
            this.groupBoxGhosts.Controls.Add(this.groupBoxGhostInfo);
            this.groupBoxGhosts.Controls.Add(this.buttonSaveGhost);
            this.groupBoxGhosts.Location = new System.Drawing.Point(170, 3);
            this.groupBoxGhosts.Name = "groupBoxGhosts";
            this.groupBoxGhosts.Size = new System.Drawing.Size(515, 457);
            this.groupBoxGhosts.TabIndex = 2;
            this.groupBoxGhosts.TabStop = false;
            this.groupBoxGhosts.Text = "Ghosts";
            // 
            // listBoxGhosts
            // 
            this.listBoxGhosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxGhosts.FormattingEnabled = true;
            this.listBoxGhosts.Location = new System.Drawing.Point(9, 12);
            this.listBoxGhosts.Name = "listBoxGhosts";
            this.listBoxGhosts.Size = new System.Drawing.Size(350, 316);
            this.listBoxGhosts.TabIndex = 5;
            this.listBoxGhosts.SelectedIndexChanged += new System.EventHandler(this.listBoxGhosts_SelectedIndexChanged);
            // 
            // buttonMarioColor
            // 
            this.buttonMarioColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMarioColor.BackColor = System.Drawing.Color.Red;
            this.buttonMarioColor.Location = new System.Drawing.Point(365, 118);
            this.buttonMarioColor.Name = "buttonMarioColor";
            this.buttonMarioColor.Size = new System.Drawing.Size(115, 23);
            this.buttonMarioColor.TabIndex = 5;
            this.buttonMarioColor.Text = "Main Mario Color";
            this.buttonMarioColor.UseVisualStyleBackColor = false;
            this.buttonMarioColor.Click += new System.EventHandler(this.buttonMarioColor_Click);
            // 
            // buttonWatchGhostFile
            // 
            this.buttonWatchGhostFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWatchGhostFile.Location = new System.Drawing.Point(365, 12);
            this.buttonWatchGhostFile.Name = "buttonWatchGhostFile";
            this.buttonWatchGhostFile.Size = new System.Drawing.Size(143, 23);
            this.buttonWatchGhostFile.TabIndex = 0;
            this.buttonWatchGhostFile.Text = "Edit File Watch List";
            this.buttonWatchGhostFile.UseVisualStyleBackColor = true;
            this.buttonWatchGhostFile.Click += new System.EventHandler(this.buttonWatchGhostFile_Click);
            // 
            // buttonLoadGhost
            // 
            this.buttonLoadGhost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadGhost.Location = new System.Drawing.Point(365, 60);
            this.buttonLoadGhost.Name = "buttonLoadGhost";
            this.buttonLoadGhost.Size = new System.Drawing.Size(143, 23);
            this.buttonLoadGhost.TabIndex = 0;
            this.buttonLoadGhost.Text = "Load Ghost";
            this.buttonLoadGhost.UseVisualStyleBackColor = true;
            this.buttonLoadGhost.Click += new System.EventHandler(this.buttonLoadGhost_Click);
            // 
            // groupBoxGhostInfo
            // 
            this.groupBoxGhostInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGhostInfo.Controls.Add(this.checkTransparentGhosts);
            this.groupBoxGhostInfo.Controls.Add(this.buttonGhostColor);
            this.groupBoxGhostInfo.Controls.Add(this.textBoxGhostName);
            this.groupBoxGhostInfo.Controls.Add(this.labelNumFrames);
            this.groupBoxGhostInfo.Controls.Add(this.labelName);
            this.groupBoxGhostInfo.Controls.Add(this.labelGhostFile);
            this.groupBoxGhostInfo.Controls.Add(this.labelGhostPlaybackStart);
            this.groupBoxGhostInfo.Controls.Add(this.labelBaseGlobalTimer);
            this.groupBoxGhostInfo.Controls.Add(this.numericUpDownStartOfPlayback);
            this.groupBoxGhostInfo.Location = new System.Drawing.Point(9, 343);
            this.groupBoxGhostInfo.Name = "groupBoxGhostInfo";
            this.groupBoxGhostInfo.Size = new System.Drawing.Size(499, 108);
            this.groupBoxGhostInfo.TabIndex = 1;
            this.groupBoxGhostInfo.TabStop = false;
            this.groupBoxGhostInfo.Text = "Ghost Info";
            // 
            // checkTransparentGhosts
            // 
            this.checkTransparentGhosts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkTransparentGhosts.AutoSize = true;
            this.checkTransparentGhosts.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkTransparentGhosts.Checked = true;
            this.checkTransparentGhosts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTransparentGhosts.Location = new System.Drawing.Point(374, 15);
            this.checkTransparentGhosts.Name = "checkTransparentGhosts";
            this.checkTransparentGhosts.Size = new System.Drawing.Size(119, 17);
            this.checkTransparentGhosts.TabIndex = 6;
            this.checkTransparentGhosts.Text = "Transparent Ghosts";
            this.checkTransparentGhosts.UseVisualStyleBackColor = true;
            // 
            // buttonGhostColor
            // 
            this.buttonGhostColor.Enabled = false;
            this.buttonGhostColor.Location = new System.Drawing.Point(156, 11);
            this.buttonGhostColor.Name = "buttonGhostColor";
            this.buttonGhostColor.Size = new System.Drawing.Size(49, 23);
            this.buttonGhostColor.TabIndex = 5;
            this.buttonGhostColor.Text = "Color";
            this.buttonGhostColor.UseVisualStyleBackColor = true;
            this.buttonGhostColor.Click += new System.EventHandler(this.buttonGhostColor_Click);
            // 
            // textBoxGhostName
            // 
            this.textBoxGhostName.Location = new System.Drawing.Point(50, 13);
            this.textBoxGhostName.Name = "textBoxGhostName";
            this.textBoxGhostName.Size = new System.Drawing.Size(100, 20);
            this.textBoxGhostName.TabIndex = 4;
            this.textBoxGhostName.TextChanged += new System.EventHandler(this.textBoxGhostName_TextChanged);
            // 
            // labelNumFrames
            // 
            this.labelNumFrames.AutoSize = true;
            this.labelNumFrames.Location = new System.Drawing.Point(6, 53);
            this.labelNumFrames.Name = "labelNumFrames";
            this.labelNumFrames.Size = new System.Drawing.Size(93, 13);
            this.labelNumFrames.TabIndex = 3;
            this.labelNumFrames.Text = "Number of frames:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(6, 16);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "Name:";
            // 
            // labelGhostFile
            // 
            this.labelGhostFile.AutoSize = true;
            this.labelGhostFile.Location = new System.Drawing.Point(6, 40);
            this.labelGhostFile.Name = "labelGhostFile";
            this.labelGhostFile.Size = new System.Drawing.Size(26, 13);
            this.labelGhostFile.TabIndex = 3;
            this.labelGhostFile.Text = "File:";
            // 
            // labelGhostPlaybackStart
            // 
            this.labelGhostPlaybackStart.AutoSize = true;
            this.labelGhostPlaybackStart.Location = new System.Drawing.Point(6, 66);
            this.labelGhostPlaybackStart.Name = "labelGhostPlaybackStart";
            this.labelGhostPlaybackStart.Size = new System.Drawing.Size(117, 13);
            this.labelGhostPlaybackStart.TabIndex = 3;
            this.labelGhostPlaybackStart.Text = "Original Playback Start:";
            // 
            // labelBaseGlobalTimer
            // 
            this.labelBaseGlobalTimer.AutoSize = true;
            this.labelBaseGlobalTimer.Location = new System.Drawing.Point(6, 88);
            this.labelBaseGlobalTimer.Name = "labelBaseGlobalTimer";
            this.labelBaseGlobalTimer.Size = new System.Drawing.Size(153, 13);
            this.labelBaseGlobalTimer.TabIndex = 3;
            this.labelBaseGlobalTimer.Text = "Start Playback at Global Timer:";
            // 
            // numericUpDownStartOfPlayback
            // 
            this.numericUpDownStartOfPlayback.Location = new System.Drawing.Point(165, 86);
            this.numericUpDownStartOfPlayback.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericUpDownStartOfPlayback.Name = "numericUpDownStartOfPlayback";
            this.numericUpDownStartOfPlayback.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownStartOfPlayback.TabIndex = 2;
            this.numericUpDownStartOfPlayback.ValueChanged += new System.EventHandler(this.numericUpDownStartOfPlayback_ValueChanged);
            // 
            // buttonSaveGhost
            // 
            this.buttonSaveGhost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveGhost.Location = new System.Drawing.Point(365, 89);
            this.buttonSaveGhost.Name = "buttonSaveGhost";
            this.buttonSaveGhost.Size = new System.Drawing.Size(143, 23);
            this.buttonSaveGhost.TabIndex = 0;
            this.buttonSaveGhost.Text = "Save Selected";
            this.buttonSaveGhost.UseVisualStyleBackColor = true;
            this.buttonSaveGhost.Click += new System.EventHandler(this.buttonSaveGhost_Click);
            // 
            // buttonTutorialRecord
            // 
            this.buttonTutorialRecord.Location = new System.Drawing.Point(9, 52);
            this.buttonTutorialRecord.Name = "buttonTutorialRecord";
            this.buttonTutorialRecord.Size = new System.Drawing.Size(146, 23);
            this.buttonTutorialRecord.TabIndex = 7;
            this.buttonTutorialRecord.Text = "Recording Ghosts";
            this.buttonTutorialRecord.UseVisualStyleBackColor = true;
            this.buttonTutorialRecord.Click += new System.EventHandler(this.buttonTutorialRecord_Click);
            // 
            // groupGhostHack
            // 
            this.groupGhostHack.Controls.Add(this.labelHackActiveState);
            this.groupGhostHack.Controls.Add(this.buttonDisableGhostHack);
            this.groupGhostHack.Controls.Add(this.buttonEnableGhostHack);
            this.groupGhostHack.Location = new System.Drawing.Point(3, 3);
            this.groupGhostHack.Name = "groupGhostHack";
            this.groupGhostHack.Size = new System.Drawing.Size(161, 164);
            this.groupGhostHack.TabIndex = 3;
            this.groupGhostHack.TabStop = false;
            this.groupGhostHack.Text = "Ghost Hack";
            // 
            // labelHackActiveState
            // 
            this.labelHackActiveState.AutoSize = true;
            this.labelHackActiveState.Location = new System.Drawing.Point(6, 82);
            this.labelHackActiveState.Name = "labelHackActiveState";
            this.labelHackActiveState.Size = new System.Drawing.Size(131, 13);
            this.labelHackActiveState.TabIndex = 3;
            this.labelHackActiveState.Text = "Ghost hack is not enabled";
            // 
            // buttonDisableGhostHack
            // 
            this.buttonDisableGhostHack.Location = new System.Drawing.Point(6, 48);
            this.buttonDisableGhostHack.Name = "buttonDisableGhostHack";
            this.buttonDisableGhostHack.Size = new System.Drawing.Size(143, 23);
            this.buttonDisableGhostHack.TabIndex = 0;
            this.buttonDisableGhostHack.Text = "Disable Ghost Hack";
            this.buttonDisableGhostHack.UseVisualStyleBackColor = true;
            this.buttonDisableGhostHack.Click += new System.EventHandler(this.buttonDisableGhostHack_Click);
            // 
            // buttonEnableGhostHack
            // 
            this.buttonEnableGhostHack.Location = new System.Drawing.Point(6, 19);
            this.buttonEnableGhostHack.Name = "buttonEnableGhostHack";
            this.buttonEnableGhostHack.Size = new System.Drawing.Size(143, 23);
            this.buttonEnableGhostHack.TabIndex = 0;
            this.buttonEnableGhostHack.Text = "Enable Ghost Hack";
            this.buttonEnableGhostHack.UseVisualStyleBackColor = true;
            this.buttonEnableGhostHack.Click += new System.EventHandler(this.buttonEnableGhostHack_Click);
            // 
            // watchVariablePanelGhost
            // 
            this.watchVariablePanelGhost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.watchVariablePanelGhost.DataPath = "Config/GhostData.xml";
            this.watchVariablePanelGhost.Location = new System.Drawing.Point(6, 19);
            this.watchVariablePanelGhost.Name = "watchVariablePanelGhost";
            this.watchVariablePanelGhost.Size = new System.Drawing.Size(209, 432);
            this.watchVariablePanelGhost.TabIndex = 4;
            // 
            // groupBoxVariables
            // 
            this.groupBoxVariables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVariables.Controls.Add(this.watchVariablePanelGhost);
            this.groupBoxVariables.Location = new System.Drawing.Point(691, 3);
            this.groupBoxVariables.Name = "groupBoxVariables";
            this.groupBoxVariables.Size = new System.Drawing.Size(221, 457);
            this.groupBoxVariables.TabIndex = 5;
            this.groupBoxVariables.TabStop = false;
            this.groupBoxVariables.Text = "Variables";
            // 
            // groupBoxHelp
            // 
            this.groupBoxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxHelp.Controls.Add(this.buttonTutorialFileWatch);
            this.groupBoxHelp.Controls.Add(this.buttonTutorialPlayback);
            this.groupBoxHelp.Controls.Add(this.buttonTutorialNotes);
            this.groupBoxHelp.Controls.Add(this.buttonTutorialRecord);
            this.groupBoxHelp.Location = new System.Drawing.Point(3, 173);
            this.groupBoxHelp.Name = "groupBoxHelp";
            this.groupBoxHelp.Size = new System.Drawing.Size(161, 287);
            this.groupBoxHelp.TabIndex = 8;
            this.groupBoxHelp.TabStop = false;
            this.groupBoxHelp.Text = "Help";
            // 
            // buttonTutorialFileWatch
            // 
            this.buttonTutorialFileWatch.Location = new System.Drawing.Point(9, 110);
            this.buttonTutorialFileWatch.Name = "buttonTutorialFileWatch";
            this.buttonTutorialFileWatch.Size = new System.Drawing.Size(146, 23);
            this.buttonTutorialFileWatch.TabIndex = 7;
            this.buttonTutorialFileWatch.Text = "Using File Watchers";
            this.buttonTutorialFileWatch.UseVisualStyleBackColor = true;
            this.buttonTutorialFileWatch.Click += new System.EventHandler(this.buttonTutorialFileWatch_Click);
            // 
            // buttonTutorialPlayback
            // 
            this.buttonTutorialPlayback.Location = new System.Drawing.Point(9, 81);
            this.buttonTutorialPlayback.Name = "buttonTutorialPlayback";
            this.buttonTutorialPlayback.Size = new System.Drawing.Size(146, 23);
            this.buttonTutorialPlayback.TabIndex = 7;
            this.buttonTutorialPlayback.Text = "Playing Ghosts back";
            this.buttonTutorialPlayback.UseVisualStyleBackColor = true;
            this.buttonTutorialPlayback.Click += new System.EventHandler(this.buttonTutorialPlayback_Click);
            // 
            // buttonTutorialNotes
            // 
            this.buttonTutorialNotes.Location = new System.Drawing.Point(9, 19);
            this.buttonTutorialNotes.Name = "buttonTutorialNotes";
            this.buttonTutorialNotes.Size = new System.Drawing.Size(146, 23);
            this.buttonTutorialNotes.TabIndex = 7;
            this.buttonTutorialNotes.Text = "General Notes";
            this.buttonTutorialNotes.UseVisualStyleBackColor = true;
            this.buttonTutorialNotes.Click += new System.EventHandler(this.buttonTutorialNotes_Click);
            // 
            // GhostTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBoxHelp);
            this.Controls.Add(this.groupBoxVariables);
            this.Controls.Add(this.groupBoxGhosts);
            this.Controls.Add(this.groupGhostHack);
            this.Name = "GhostTab";
            this.groupBoxGhosts.ResumeLayout(false);
            this.groupBoxGhostInfo.ResumeLayout(false);
            this.groupBoxGhostInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartOfPlayback)).EndInit();
            this.groupGhostHack.ResumeLayout(false);
            this.groupGhostHack.PerformLayout();
            this.groupBoxVariables.ResumeLayout(false);
            this.groupBoxHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxGhosts;
        private System.Windows.Forms.ListBox listBoxGhosts;
        private System.Windows.Forms.Button buttonLoadGhost;
        private System.Windows.Forms.GroupBox groupBoxGhostInfo;
        private System.Windows.Forms.Label labelBaseGlobalTimer;
        private System.Windows.Forms.NumericUpDown numericUpDownStartOfPlayback;
        private System.Windows.Forms.Button buttonSaveGhost;
        private System.Windows.Forms.GroupBox groupGhostHack;
        private System.Windows.Forms.Button buttonDisableGhostHack;
        private System.Windows.Forms.Button buttonEnableGhostHack;
        public System.Windows.Forms.Label labelNumFrames;
        public System.Windows.Forms.Label labelGhostFile;
        public System.Windows.Forms.Label labelGhostPlaybackStart;
        public System.Windows.Forms.Label labelHackActiveState;
        private System.Windows.Forms.TextBox textBoxGhostName;
        public System.Windows.Forms.Label labelName;
        private Controls.WatchVariableFlowLayoutPanel watchVariablePanelGhost;
        private System.Windows.Forms.Button buttonWatchGhostFile;
        private System.Windows.Forms.Button buttonTutorialRecord;
        private System.Windows.Forms.GroupBox groupBoxVariables;
        private System.Windows.Forms.GroupBox groupBoxHelp;
        private System.Windows.Forms.Button buttonTutorialPlayback;
        private System.Windows.Forms.Button buttonTutorialNotes;
        private System.Windows.Forms.Button buttonTutorialFileWatch;
        private System.Windows.Forms.Button buttonGhostColor;
        private System.Windows.Forms.CheckBox checkTransparentGhosts;
        private System.Windows.Forms.Button buttonMarioColor;
    }
}
