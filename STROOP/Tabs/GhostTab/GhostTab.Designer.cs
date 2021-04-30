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
            this.buttonTutorial = new System.Windows.Forms.Button();
            this.buttonDiscardOld = new System.Windows.Forms.CheckBox();
            this.buttonInstantReplayMode = new System.Windows.Forms.CheckBox();
            this.listBoxGhosts = new System.Windows.Forms.ListBox();
            this.buttonWatchGhostFile = new System.Windows.Forms.Button();
            this.buttonRecordGhost = new System.Windows.Forms.Button();
            this.buttonLoadGhost = new System.Windows.Forms.Button();
            this.groupBoxGhostInfo = new System.Windows.Forms.GroupBox();
            this.textBoxGhostName = new System.Windows.Forms.TextBox();
            this.labelMissionTimes = new System.Windows.Forms.Label();
            this.labelNumFrames = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelGhostFile = new System.Windows.Forms.Label();
            this.labelMissionStarts = new System.Windows.Forms.Label();
            this.labelGhostPlaybackStart = new System.Windows.Forms.Label();
            this.labelBaseGlobalTimer = new System.Windows.Forms.Label();
            this.numericUpDownStartOfPlayback = new System.Windows.Forms.NumericUpDown();
            this.buttonSaveGhost = new System.Windows.Forms.Button();
            this.groupGhostHack = new System.Windows.Forms.GroupBox();
            this.labelHackActiveState = new System.Windows.Forms.Label();
            this.buttonDisableGhostHack = new System.Windows.Forms.Button();
            this.buttonEnableGhostHack = new System.Windows.Forms.Button();
            this.watchVariablePanelGhost = new STROOP.Controls.WatchVariableFlowLayoutPanel();
            this.groupBoxGhosts.SuspendLayout();
            this.groupBoxGhostInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartOfPlayback)).BeginInit();
            this.groupGhostHack.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxGhosts
            // 
            this.groupBoxGhosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGhosts.Controls.Add(this.buttonTutorial);
            this.groupBoxGhosts.Controls.Add(this.buttonDiscardOld);
            this.groupBoxGhosts.Controls.Add(this.buttonInstantReplayMode);
            this.groupBoxGhosts.Controls.Add(this.listBoxGhosts);
            this.groupBoxGhosts.Controls.Add(this.buttonWatchGhostFile);
            this.groupBoxGhosts.Controls.Add(this.buttonRecordGhost);
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
            // buttonTutorial
            // 
            this.buttonTutorial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTutorial.Location = new System.Drawing.Point(365, 41);
            this.buttonTutorial.Name = "buttonTutorial";
            this.buttonTutorial.Size = new System.Drawing.Size(60, 23);
            this.buttonTutorial.TabIndex = 7;
            this.buttonTutorial.Text = "Tutorial";
            this.buttonTutorial.UseVisualStyleBackColor = true;
            this.buttonTutorial.Click += new System.EventHandler(this.buttonTutorial_Click);
            // 
            // buttonDiscardOld
            // 
            this.buttonDiscardOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDiscardOld.Appearance = System.Windows.Forms.Appearance.Button;
            this.buttonDiscardOld.AutoSize = true;
            this.buttonDiscardOld.Location = new System.Drawing.Point(365, 239);
            this.buttonDiscardOld.Name = "buttonDiscardOld";
            this.buttonDiscardOld.Size = new System.Drawing.Size(72, 23);
            this.buttonDiscardOld.TabIndex = 6;
            this.buttonDiscardOld.Text = "Discard Old";
            this.buttonDiscardOld.UseVisualStyleBackColor = true;
            // 
            // buttonInstantReplayMode
            // 
            this.buttonInstantReplayMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInstantReplayMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.buttonInstantReplayMode.AutoSize = true;
            this.buttonInstantReplayMode.Location = new System.Drawing.Point(365, 210);
            this.buttonInstantReplayMode.Name = "buttonInstantReplayMode";
            this.buttonInstantReplayMode.Size = new System.Drawing.Size(115, 23);
            this.buttonInstantReplayMode.TabIndex = 6;
            this.buttonInstantReplayMode.Text = "Instant Replay Mode";
            this.buttonInstantReplayMode.UseVisualStyleBackColor = true;
            this.buttonInstantReplayMode.CheckedChanged += new System.EventHandler(this.buttonInstantReplayMode_CheckedChanged);
            // 
            // listBoxGhosts
            // 
            this.listBoxGhosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxGhosts.FormattingEnabled = true;
            this.listBoxGhosts.Location = new System.Drawing.Point(9, 12);
            this.listBoxGhosts.Name = "listBoxGhosts";
            this.listBoxGhosts.Size = new System.Drawing.Size(350, 277);
            this.listBoxGhosts.TabIndex = 5;
            this.listBoxGhosts.SelectedIndexChanged += new System.EventHandler(this.listBoxGhosts_SelectedIndexChanged);
            // 
            // buttonWatchGhostFile
            // 
            this.buttonWatchGhostFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWatchGhostFile.Location = new System.Drawing.Point(365, 12);
            this.buttonWatchGhostFile.Name = "buttonWatchGhostFile";
            this.buttonWatchGhostFile.Size = new System.Drawing.Size(143, 23);
            this.buttonWatchGhostFile.TabIndex = 0;
            this.buttonWatchGhostFile.Text = "Watch Ghost File";
            this.buttonWatchGhostFile.UseVisualStyleBackColor = true;
            this.buttonWatchGhostFile.Click += new System.EventHandler(this.buttonWatchGhostFile_Click);
            // 
            // buttonRecordGhost
            // 
            this.buttonRecordGhost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRecordGhost.Location = new System.Drawing.Point(365, 82);
            this.buttonRecordGhost.Name = "buttonRecordGhost";
            this.buttonRecordGhost.Size = new System.Drawing.Size(143, 23);
            this.buttonRecordGhost.TabIndex = 0;
            this.buttonRecordGhost.Text = "Record Ghost";
            this.buttonRecordGhost.UseVisualStyleBackColor = true;
            this.buttonRecordGhost.Click += new System.EventHandler(this.buttonRecordGhost_Click);
            // 
            // buttonLoadGhost
            // 
            this.buttonLoadGhost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadGhost.Location = new System.Drawing.Point(365, 111);
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
            this.groupBoxGhostInfo.Controls.Add(this.textBoxGhostName);
            this.groupBoxGhostInfo.Controls.Add(this.labelMissionTimes);
            this.groupBoxGhostInfo.Controls.Add(this.labelNumFrames);
            this.groupBoxGhostInfo.Controls.Add(this.labelName);
            this.groupBoxGhostInfo.Controls.Add(this.labelGhostFile);
            this.groupBoxGhostInfo.Controls.Add(this.labelMissionStarts);
            this.groupBoxGhostInfo.Controls.Add(this.labelGhostPlaybackStart);
            this.groupBoxGhostInfo.Controls.Add(this.labelBaseGlobalTimer);
            this.groupBoxGhostInfo.Controls.Add(this.numericUpDownStartOfPlayback);
            this.groupBoxGhostInfo.Location = new System.Drawing.Point(9, 292);
            this.groupBoxGhostInfo.Name = "groupBoxGhostInfo";
            this.groupBoxGhostInfo.Size = new System.Drawing.Size(499, 159);
            this.groupBoxGhostInfo.TabIndex = 1;
            this.groupBoxGhostInfo.TabStop = false;
            this.groupBoxGhostInfo.Text = "Ghost Info";
            // 
            // textBoxGhostName
            // 
            this.textBoxGhostName.Location = new System.Drawing.Point(50, 13);
            this.textBoxGhostName.Name = "textBoxGhostName";
            this.textBoxGhostName.Size = new System.Drawing.Size(100, 20);
            this.textBoxGhostName.TabIndex = 4;
            this.textBoxGhostName.TextChanged += new System.EventHandler(this.textBoxGhostName_TextChanged);
            // 
            // labelMissionTimes
            // 
            this.labelMissionTimes.AutoSize = true;
            this.labelMissionTimes.Location = new System.Drawing.Point(6, 105);
            this.labelMissionTimes.Name = "labelMissionTimes";
            this.labelMissionTimes.Size = new System.Drawing.Size(113, 13);
            this.labelMissionTimes.TabIndex = 3;
            this.labelMissionTimes.Text = "Time to Star Dance(s):";
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
            // labelMissionStarts
            // 
            this.labelMissionStarts.AutoSize = true;
            this.labelMissionStarts.Location = new System.Drawing.Point(6, 92);
            this.labelMissionStarts.Name = "labelMissionStarts";
            this.labelMissionStarts.Size = new System.Drawing.Size(107, 13);
            this.labelMissionStarts.TabIndex = 3;
            this.labelMissionStarts.Text = "Mission Start Time(s):";
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
            this.labelBaseGlobalTimer.Location = new System.Drawing.Point(6, 129);
            this.labelBaseGlobalTimer.Name = "labelBaseGlobalTimer";
            this.labelBaseGlobalTimer.Size = new System.Drawing.Size(153, 13);
            this.labelBaseGlobalTimer.TabIndex = 3;
            this.labelBaseGlobalTimer.Text = "Start Playback at Global Timer:";
            // 
            // numericUpDownStartOfPlayback
            // 
            this.numericUpDownStartOfPlayback.Location = new System.Drawing.Point(165, 127);
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
            this.buttonSaveGhost.Location = new System.Drawing.Point(365, 140);
            this.buttonSaveGhost.Name = "buttonSaveGhost";
            this.buttonSaveGhost.Size = new System.Drawing.Size(143, 23);
            this.buttonSaveGhost.TabIndex = 0;
            this.buttonSaveGhost.Text = "Save Selected";
            this.buttonSaveGhost.UseVisualStyleBackColor = true;
            this.buttonSaveGhost.Click += new System.EventHandler(this.buttonSaveGhost_Click);
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
            this.watchVariablePanelGhost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.watchVariablePanelGhost.DataPath = "Config/GhostData.xml";
            this.watchVariablePanelGhost.Location = new System.Drawing.Point(691, 3);
            this.watchVariablePanelGhost.Name = "watchVariablePanelGhost";
            this.watchVariablePanelGhost.Size = new System.Drawing.Size(221, 457);
            this.watchVariablePanelGhost.TabIndex = 4;
            // 
            // GhostTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.watchVariablePanelGhost);
            this.Controls.Add(this.groupBoxGhosts);
            this.Controls.Add(this.groupGhostHack);
            this.Name = "GhostTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.groupBoxGhosts.ResumeLayout(false);
            this.groupBoxGhosts.PerformLayout();
            this.groupBoxGhostInfo.ResumeLayout(false);
            this.groupBoxGhostInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartOfPlayback)).EndInit();
            this.groupGhostHack.ResumeLayout(false);
            this.groupGhostHack.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxGhosts;
        private System.Windows.Forms.ListBox listBoxGhosts;
        private System.Windows.Forms.Button buttonRecordGhost;
        private System.Windows.Forms.Button buttonLoadGhost;
        private System.Windows.Forms.GroupBox groupBoxGhostInfo;
        private System.Windows.Forms.Label labelBaseGlobalTimer;
        private System.Windows.Forms.NumericUpDown numericUpDownStartOfPlayback;
        private System.Windows.Forms.Button buttonSaveGhost;
        private System.Windows.Forms.GroupBox groupGhostHack;
        private System.Windows.Forms.Button buttonDisableGhostHack;
        private System.Windows.Forms.Button buttonEnableGhostHack;
        public System.Windows.Forms.Label labelMissionTimes;
        public System.Windows.Forms.Label labelNumFrames;
        public System.Windows.Forms.Label labelGhostFile;
        public System.Windows.Forms.Label labelMissionStarts;
        public System.Windows.Forms.Label labelGhostPlaybackStart;
        public System.Windows.Forms.Label labelHackActiveState;
        private System.Windows.Forms.CheckBox buttonInstantReplayMode;
        private System.Windows.Forms.CheckBox buttonDiscardOld;
        private System.Windows.Forms.TextBox textBoxGhostName;
        public System.Windows.Forms.Label labelName;
        private Controls.WatchVariableFlowLayoutPanel watchVariablePanelGhost;
        private System.Windows.Forms.Button buttonWatchGhostFile;
        private System.Windows.Forms.Button buttonTutorial;
    }
}
