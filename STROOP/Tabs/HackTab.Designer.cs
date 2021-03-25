namespace STROOP.Tabs
{
    partial class HackTab
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
            this.splitContainerHacks = new STROOP.BetterSplitContainer();
            this.groupBoxHackRam = new System.Windows.Forms.GroupBox();
            this.labelPureInterpretterRequire = new System.Windows.Forms.Label();
            this.checkedListBoxHacks = new System.Windows.Forms.CheckedListBox();
            this.groupBoxHackSpawn = new System.Windows.Forms.GroupBox();
            this.labelSpawnBehavior = new System.Windows.Forms.Label();
            this.textBoxSpawnBehavior = new STROOP.BetterTextbox();
            this.labelSpawnHint = new System.Windows.Forms.Label();
            this.buttonSpawnReset = new System.Windows.Forms.Button();
            this.labelSpawnExtra = new System.Windows.Forms.Label();
            this.labelSpawnGfxId = new System.Windows.Forms.Label();
            this.textBoxSpawnExtra = new STROOP.BetterTextbox();
            this.textBoxSpawnGfxId = new STROOP.BetterTextbox();
            this.buttonHackSpawn = new System.Windows.Forms.Button();
            this.listBoxSpawn = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHacks)).BeginInit();
            this.splitContainerHacks.Panel1.SuspendLayout();
            this.splitContainerHacks.Panel2.SuspendLayout();
            this.splitContainerHacks.SuspendLayout();
            this.groupBoxHackRam.SuspendLayout();
            this.groupBoxHackSpawn.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerHacks
            // 
            this.splitContainerHacks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerHacks.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerHacks.Location = new System.Drawing.Point(0, 0);
            this.splitContainerHacks.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerHacks.Name = "splitContainerHacks";
            // 
            // splitContainerHacks.Panel1
            // 
            this.splitContainerHacks.Panel1.Controls.Add(this.groupBoxHackRam);
            this.splitContainerHacks.Panel1.Padding = new System.Windows.Forms.Padding(2);
            this.splitContainerHacks.Panel1MinSize = 0;
            // 
            // splitContainerHacks.Panel2
            // 
            this.splitContainerHacks.Panel2.Controls.Add(this.groupBoxHackSpawn);
            this.splitContainerHacks.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this.splitContainerHacks.Panel2MinSize = 0;
            this.splitContainerHacks.Size = new System.Drawing.Size(915, 463);
            this.splitContainerHacks.SplitterDistance = 301;
            this.splitContainerHacks.SplitterWidth = 1;
            this.splitContainerHacks.TabIndex = 15;
            // 
            // groupBoxHackRam
            // 
            this.groupBoxHackRam.Controls.Add(this.labelPureInterpretterRequire);
            this.groupBoxHackRam.Controls.Add(this.checkedListBoxHacks);
            this.groupBoxHackRam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxHackRam.Location = new System.Drawing.Point(2, 2);
            this.groupBoxHackRam.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxHackRam.Name = "groupBoxHackRam";
            this.groupBoxHackRam.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxHackRam.Size = new System.Drawing.Size(297, 459);
            this.groupBoxHackRam.TabIndex = 13;
            this.groupBoxHackRam.TabStop = false;
            this.groupBoxHackRam.Text = "RAM Hacks*";
            // 
            // labelPureInterpretterRequire
            // 
            this.labelPureInterpretterRequire.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPureInterpretterRequire.AutoSize = true;
            this.labelPureInterpretterRequire.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPureInterpretterRequire.Location = new System.Drawing.Point(170, -1);
            this.labelPureInterpretterRequire.Name = "labelPureInterpretterRequire";
            this.labelPureInterpretterRequire.Size = new System.Drawing.Size(129, 13);
            this.labelPureInterpretterRequire.TabIndex = 8;
            this.labelPureInterpretterRequire.Text = "*Requires Pure Interpreter";
            // 
            // checkedListBoxHacks
            // 
            this.checkedListBoxHacks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxHacks.CheckOnClick = true;
            this.checkedListBoxHacks.FormattingEnabled = true;
            this.checkedListBoxHacks.Location = new System.Drawing.Point(3, 19);
            this.checkedListBoxHacks.Name = "checkedListBoxHacks";
            this.checkedListBoxHacks.Size = new System.Drawing.Size(291, 379);
            this.checkedListBoxHacks.TabIndex = 9;
            // 
            // groupBoxHackSpawn
            // 
            this.groupBoxHackSpawn.Controls.Add(this.labelSpawnBehavior);
            this.groupBoxHackSpawn.Controls.Add(this.textBoxSpawnBehavior);
            this.groupBoxHackSpawn.Controls.Add(this.labelSpawnHint);
            this.groupBoxHackSpawn.Controls.Add(this.buttonSpawnReset);
            this.groupBoxHackSpawn.Controls.Add(this.labelSpawnExtra);
            this.groupBoxHackSpawn.Controls.Add(this.labelSpawnGfxId);
            this.groupBoxHackSpawn.Controls.Add(this.textBoxSpawnExtra);
            this.groupBoxHackSpawn.Controls.Add(this.textBoxSpawnGfxId);
            this.groupBoxHackSpawn.Controls.Add(this.buttonHackSpawn);
            this.groupBoxHackSpawn.Controls.Add(this.listBoxSpawn);
            this.groupBoxHackSpawn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxHackSpawn.Location = new System.Drawing.Point(2, 2);
            this.groupBoxHackSpawn.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxHackSpawn.Name = "groupBoxHackSpawn";
            this.groupBoxHackSpawn.Size = new System.Drawing.Size(609, 459);
            this.groupBoxHackSpawn.TabIndex = 0;
            this.groupBoxHackSpawn.TabStop = false;
            this.groupBoxHackSpawn.Text = "Spawner";
            // 
            // labelSpawnBehavior
            // 
            this.labelSpawnBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSpawnBehavior.AutoSize = true;
            this.labelSpawnBehavior.Location = new System.Drawing.Point(3, 411);
            this.labelSpawnBehavior.Name = "labelSpawnBehavior";
            this.labelSpawnBehavior.Size = new System.Drawing.Size(52, 13);
            this.labelSpawnBehavior.TabIndex = 27;
            this.labelSpawnBehavior.Text = "Behavior:";
            // 
            // textBoxSpawnBehavior
            // 
            this.textBoxSpawnBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSpawnBehavior.Location = new System.Drawing.Point(57, 408);
            this.textBoxSpawnBehavior.Name = "textBoxSpawnBehavior";
            this.textBoxSpawnBehavior.Size = new System.Drawing.Size(138, 20);
            this.textBoxSpawnBehavior.TabIndex = 26;
            // 
            // labelSpawnHint
            // 
            this.labelSpawnHint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSpawnHint.AutoSize = true;
            this.labelSpawnHint.Location = new System.Drawing.Point(315, 438);
            this.labelSpawnHint.Name = "labelSpawnHint";
            this.labelSpawnHint.Size = new System.Drawing.Size(127, 13);
            this.labelSpawnHint.TabIndex = 25;
            this.labelSpawnHint.Text = "(Press L button to spawn)";
            // 
            // buttonSpawnReset
            // 
            this.buttonSpawnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSpawnReset.Location = new System.Drawing.Point(200, 434);
            this.buttonSpawnReset.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSpawnReset.Name = "buttonSpawnReset";
            this.buttonSpawnReset.Size = new System.Drawing.Size(110, 21);
            this.buttonSpawnReset.TabIndex = 24;
            this.buttonSpawnReset.Text = "Reset (Turn Off)";
            this.buttonSpawnReset.UseVisualStyleBackColor = true;
            // 
            // labelSpawnExtra
            // 
            this.labelSpawnExtra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSpawnExtra.AutoSize = true;
            this.labelSpawnExtra.Location = new System.Drawing.Point(109, 437);
            this.labelSpawnExtra.Name = "labelSpawnExtra";
            this.labelSpawnExtra.Size = new System.Drawing.Size(34, 13);
            this.labelSpawnExtra.TabIndex = 23;
            this.labelSpawnExtra.Text = "Extra:";
            // 
            // labelSpawnGfxId
            // 
            this.labelSpawnGfxId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSpawnGfxId.AutoSize = true;
            this.labelSpawnGfxId.Location = new System.Drawing.Point(6, 438);
            this.labelSpawnGfxId.Name = "labelSpawnGfxId";
            this.labelSpawnGfxId.Size = new System.Drawing.Size(45, 13);
            this.labelSpawnGfxId.TabIndex = 22;
            this.labelSpawnGfxId.Text = "GFX ID:";
            // 
            // textBoxSpawnExtra
            // 
            this.textBoxSpawnExtra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSpawnExtra.Location = new System.Drawing.Point(149, 434);
            this.textBoxSpawnExtra.Name = "textBoxSpawnExtra";
            this.textBoxSpawnExtra.Size = new System.Drawing.Size(46, 20);
            this.textBoxSpawnExtra.TabIndex = 21;
            // 
            // textBoxSpawnGfxId
            // 
            this.textBoxSpawnGfxId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSpawnGfxId.Location = new System.Drawing.Point(57, 434);
            this.textBoxSpawnGfxId.Name = "textBoxSpawnGfxId";
            this.textBoxSpawnGfxId.Size = new System.Drawing.Size(46, 20);
            this.textBoxSpawnGfxId.TabIndex = 20;
            // 
            // buttonHackSpawn
            // 
            this.buttonHackSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonHackSpawn.Location = new System.Drawing.Point(200, 407);
            this.buttonHackSpawn.Margin = new System.Windows.Forms.Padding(2);
            this.buttonHackSpawn.Name = "buttonHackSpawn";
            this.buttonHackSpawn.Size = new System.Drawing.Size(110, 21);
            this.buttonHackSpawn.TabIndex = 19;
            this.buttonHackSpawn.Text = "Set Spawn Type";
            this.buttonHackSpawn.UseVisualStyleBackColor = true;
            // 
            // listBoxSpawn
            // 
            this.listBoxSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSpawn.FormattingEnabled = true;
            this.listBoxSpawn.Location = new System.Drawing.Point(6, 19);
            this.listBoxSpawn.Name = "listBoxSpawn";
            this.listBoxSpawn.Size = new System.Drawing.Size(603, 381);
            this.listBoxSpawn.Sorted = true;
            this.listBoxSpawn.TabIndex = 12;
            // 
            // HackTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerHacks);
            this.Name = "HackTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.splitContainerHacks.Panel1.ResumeLayout(false);
            this.splitContainerHacks.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHacks)).EndInit();
            this.splitContainerHacks.ResumeLayout(false);
            this.groupBoxHackRam.ResumeLayout(false);
            this.groupBoxHackRam.PerformLayout();
            this.groupBoxHackSpawn.ResumeLayout(false);
            this.groupBoxHackSpawn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BetterSplitContainer splitContainerHacks;
        private System.Windows.Forms.GroupBox groupBoxHackRam;
        private System.Windows.Forms.Label labelPureInterpretterRequire;
        private System.Windows.Forms.CheckedListBox checkedListBoxHacks;
        private System.Windows.Forms.GroupBox groupBoxHackSpawn;
        private System.Windows.Forms.Label labelSpawnBehavior;
        private BetterTextbox textBoxSpawnBehavior;
        private System.Windows.Forms.Label labelSpawnHint;
        private System.Windows.Forms.Button buttonSpawnReset;
        private System.Windows.Forms.Label labelSpawnExtra;
        private System.Windows.Forms.Label labelSpawnGfxId;
        private BetterTextbox textBoxSpawnExtra;
        private BetterTextbox textBoxSpawnGfxId;
        private System.Windows.Forms.Button buttonHackSpawn;
        private System.Windows.Forms.ListBox listBoxSpawn;
    }
}
