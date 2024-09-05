using STROOP.Controls;

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
            this.splitContainerHacks = new BetterSplitContainer();
            this.groupBoxHackRam = new System.Windows.Forms.GroupBox();
            this.labelInGameFunctionCalls = new System.Windows.Forms.Label();
            this.labelInjectLevelScriptCommand = new System.Windows.Forms.Label();
            this.textBoxInGameFunctionCall = new System.Windows.Forms.TextBox();
            this.buttonBrowseInGameFunctionCallFile = new System.Windows.Forms.Button();
            this.textBoxLevelScriptCommand = new System.Windows.Forms.TextBox();
            this.buttonRunInGameFunctionCall = new System.Windows.Forms.Button();
            this.buttonBrowseLevelScriptCommandsFile = new System.Windows.Forms.Button();
            this.buttonRunLevelscriptCommand = new System.Windows.Forms.Button();
            this.buttonInjectDirect = new System.Windows.Forms.Button();
            this.labelInjectFileAddress = new System.Windows.Forms.Label();
            this.textBoxInjectFileAddress = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonInjectFile = new System.Windows.Forms.Button();
            this.labelPureInterpretterRequire = new System.Windows.Forms.Label();
            this.checkedListBoxHacks = new System.Windows.Forms.CheckedListBox();
            this.groupBoxHackSpawn = new System.Windows.Forms.GroupBox();
            this.labelSpawnBehavior = new System.Windows.Forms.Label();
            this.textBoxSpawnBehavior = new BetterTextbox();
            this.labelSpawnHint = new System.Windows.Forms.Label();
            this.buttonSpawnReset = new System.Windows.Forms.Button();
            this.labelSpawnExtra = new System.Windows.Forms.Label();
            this.labelSpawnGfxId = new System.Windows.Forms.Label();
            this.textBoxSpawnExtra = new BetterTextbox();
            this.textBoxSpawnGfxId = new BetterTextbox();
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
            this.groupBoxHackRam.Controls.Add(this.labelInGameFunctionCalls);
            this.groupBoxHackRam.Controls.Add(this.labelInjectLevelScriptCommand);
            this.groupBoxHackRam.Controls.Add(this.textBoxInGameFunctionCall);
            this.groupBoxHackRam.Controls.Add(this.buttonBrowseInGameFunctionCallFile);
            this.groupBoxHackRam.Controls.Add(this.textBoxLevelScriptCommand);
            this.groupBoxHackRam.Controls.Add(this.buttonRunInGameFunctionCall);
            this.groupBoxHackRam.Controls.Add(this.buttonBrowseLevelScriptCommandsFile);
            this.groupBoxHackRam.Controls.Add(this.buttonRunLevelscriptCommand);
            this.groupBoxHackRam.Controls.Add(this.buttonInjectDirect);
            this.groupBoxHackRam.Controls.Add(this.labelInjectFileAddress);
            this.groupBoxHackRam.Controls.Add(this.textBoxInjectFileAddress);
            this.groupBoxHackRam.Controls.Add(this.textBox1);
            this.groupBoxHackRam.Controls.Add(this.buttonInjectFile);
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
            // labelInGameFunctionCalls
            // 
            this.labelInGameFunctionCalls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInGameFunctionCalls.AutoSize = true;
            this.labelInGameFunctionCalls.Location = new System.Drawing.Point(4, 324);
            this.labelInGameFunctionCalls.Name = "labelInGameFunctionCalls";
            this.labelInGameFunctionCalls.Size = new System.Drawing.Size(114, 13);
            this.labelInGameFunctionCalls.TabIndex = 32;
            this.labelInGameFunctionCalls.Text = "In-Game Function Call:";
            // 
            // labelInjectLevelScriptCommand
            // 
            this.labelInjectLevelScriptCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInjectLevelScriptCommand.AutoSize = true;
            this.labelInjectLevelScriptCommand.Location = new System.Drawing.Point(4, 363);
            this.labelInjectLevelScriptCommand.Name = "labelInjectLevelScriptCommand";
            this.labelInjectLevelScriptCommand.Size = new System.Drawing.Size(121, 13);
            this.labelInjectLevelScriptCommand.TabIndex = 32;
            this.labelInjectLevelScriptCommand.Text = "Level Script Commands:";
            // 
            // textBoxInGameFunctionCall
            // 
            this.textBoxInGameFunctionCall.Location = new System.Drawing.Point(6, 340);
            this.textBoxInGameFunctionCall.Name = "textBoxInGameFunctionCall";
            this.textBoxInGameFunctionCall.Size = new System.Drawing.Size(114, 20);
            this.textBoxInGameFunctionCall.TabIndex = 31;
            // 
            // buttonBrowseInGameFunctionCallFile
            // 
            this.buttonBrowseInGameFunctionCallFile.Location = new System.Drawing.Point(126, 338);
            this.buttonBrowseInGameFunctionCallFile.Name = "buttonBrowseInGameFunctionCallFile";
            this.buttonBrowseInGameFunctionCallFile.Size = new System.Drawing.Size(41, 23);
            this.buttonBrowseInGameFunctionCallFile.TabIndex = 30;
            this.buttonBrowseInGameFunctionCallFile.Text = "...";
            this.buttonBrowseInGameFunctionCallFile.UseVisualStyleBackColor = true;
            this.buttonBrowseInGameFunctionCallFile.Click += new System.EventHandler(this.buttonBrowseInGameFunctionCallFile_Click);
            // 
            // textBoxLevelScriptCommand
            // 
            this.textBoxLevelScriptCommand.Location = new System.Drawing.Point(6, 379);
            this.textBoxLevelScriptCommand.Name = "textBoxLevelScriptCommand";
            this.textBoxLevelScriptCommand.Size = new System.Drawing.Size(114, 20);
            this.textBoxLevelScriptCommand.TabIndex = 31;
            // 
            // buttonRunInGameFunctionCall
            // 
            this.buttonRunInGameFunctionCall.Location = new System.Drawing.Point(173, 338);
            this.buttonRunInGameFunctionCall.Name = "buttonRunInGameFunctionCall";
            this.buttonRunInGameFunctionCall.Size = new System.Drawing.Size(94, 23);
            this.buttonRunInGameFunctionCall.TabIndex = 30;
            this.buttonRunInGameFunctionCall.Text = "Run Function";
            this.buttonRunInGameFunctionCall.UseVisualStyleBackColor = true;
            this.buttonRunInGameFunctionCall.Click += new System.EventHandler(this.buttonRunInGameFunctionCall_Click);
            // 
            // buttonBrowseLevelScriptCommandsFile
            // 
            this.buttonBrowseLevelScriptCommandsFile.Location = new System.Drawing.Point(126, 377);
            this.buttonBrowseLevelScriptCommandsFile.Name = "buttonBrowseLevelScriptCommandsFile";
            this.buttonBrowseLevelScriptCommandsFile.Size = new System.Drawing.Size(41, 23);
            this.buttonBrowseLevelScriptCommandsFile.TabIndex = 30;
            this.buttonBrowseLevelScriptCommandsFile.Text = "...";
            this.buttonBrowseLevelScriptCommandsFile.UseVisualStyleBackColor = true;
            this.buttonBrowseLevelScriptCommandsFile.Click += new System.EventHandler(this.buttonBrowseLevelScriptCommandsFile_Click);
            // 
            // buttonRunLevelscriptCommand
            // 
            this.buttonRunLevelscriptCommand.Location = new System.Drawing.Point(173, 377);
            this.buttonRunLevelscriptCommand.Name = "buttonRunLevelscriptCommand";
            this.buttonRunLevelscriptCommand.Size = new System.Drawing.Size(94, 23);
            this.buttonRunLevelscriptCommand.TabIndex = 30;
            this.buttonRunLevelscriptCommand.Text = "Run Commands";
            this.buttonRunLevelscriptCommand.UseVisualStyleBackColor = true;
            this.buttonRunLevelscriptCommand.Click += new System.EventHandler(this.buttonRunLevelscriptCommand_Click);
            // 
            // buttonInjectDirect
            // 
            this.buttonInjectDirect.Location = new System.Drawing.Point(192, 418);
            this.buttonInjectDirect.Name = "buttonInjectDirect";
            this.buttonInjectDirect.Size = new System.Drawing.Size(75, 23);
            this.buttonInjectDirect.TabIndex = 29;
            this.buttonInjectDirect.Text = "Inject Bytes";
            this.buttonInjectDirect.UseVisualStyleBackColor = true;
            this.buttonInjectDirect.Click += new System.EventHandler(this.buttonInjectDirect_Click);
            // 
            // labelInjectFileAddress
            // 
            this.labelInjectFileAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInjectFileAddress.AutoSize = true;
            this.labelInjectFileAddress.Location = new System.Drawing.Point(3, 405);
            this.labelInjectFileAddress.Name = "labelInjectFileAddress";
            this.labelInjectFileAddress.Size = new System.Drawing.Size(116, 13);
            this.labelInjectFileAddress.TabIndex = 28;
            this.labelInjectFileAddress.Text = "Inject at RAM Address:";
            // 
            // textBoxInjectFileAddress
            // 
            this.textBoxInjectFileAddress.Location = new System.Drawing.Point(6, 421);
            this.textBoxInjectFileAddress.Name = "textBoxInjectFileAddress";
            this.textBoxInjectFileAddress.Size = new System.Drawing.Size(99, 20);
            this.textBoxInjectFileAddress.TabIndex = 12;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(-17, -17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 11;
            // 
            // buttonInjectFile
            // 
            this.buttonInjectFile.Location = new System.Drawing.Point(111, 419);
            this.buttonInjectFile.Name = "buttonInjectFile";
            this.buttonInjectFile.Size = new System.Drawing.Size(75, 23);
            this.buttonInjectFile.TabIndex = 10;
            this.buttonInjectFile.Text = "Inject File";
            this.buttonInjectFile.UseVisualStyleBackColor = true;
            this.buttonInjectFile.Click += new System.EventHandler(this.buttonInjectFile_Click);
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
            this.checkedListBoxHacks.Size = new System.Drawing.Size(291, 304);
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
        private System.Windows.Forms.Label labelInjectFileAddress;
        private System.Windows.Forms.TextBox textBoxInjectFileAddress;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonInjectFile;
        private System.Windows.Forms.Button buttonInjectDirect;
        private System.Windows.Forms.TextBox textBoxLevelScriptCommand;
        private System.Windows.Forms.Button buttonBrowseLevelScriptCommandsFile;
        private System.Windows.Forms.Button buttonRunLevelscriptCommand;
        private System.Windows.Forms.Label labelInGameFunctionCalls;
        private System.Windows.Forms.Label labelInjectLevelScriptCommand;
        private System.Windows.Forms.TextBox textBoxInGameFunctionCall;
        private System.Windows.Forms.Button buttonBrowseInGameFunctionCallFile;
        private System.Windows.Forms.Button buttonRunInGameFunctionCall;
    }
}
