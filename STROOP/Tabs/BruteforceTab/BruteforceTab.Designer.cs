namespace STROOP.Tabs.BruteforceTab
{
    partial class BruteforceTab
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
            this.btnLoadModule = new System.Windows.Forms.Button();
            this.txtJsonOutput = new System.Windows.Forms.RichTextBox();
            this.watchVariablePanelParams = new STROOP.Controls.WatchVariablePanel();
            this.btnApplyKnownStates = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.labelM64 = new System.Windows.Forms.Label();
            this.btnChooseM64 = new System.Windows.Forms.Button();
            this.txtManualConfig = new System.Windows.Forms.RichTextBox();
            this.lblGeneratedJson = new System.Windows.Forms.Label();
            this.lblCustomJson = new System.Windows.Forms.Label();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.tabData = new System.Windows.Forms.TabControl();
            this.tabJson = new System.Windows.Forms.TabPage();
            this.tabSurface = new System.Windows.Forms.TabPage();
            this.tabData.SuspendLayout();
            this.tabJson.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadModule
            // 
            this.btnLoadModule.Location = new System.Drawing.Point(3, 24);
            this.btnLoadModule.Name = "btnLoadModule";
            this.btnLoadModule.Size = new System.Drawing.Size(91, 23);
            this.btnLoadModule.TabIndex = 0;
            this.btnLoadModule.Text = "Load Module";
            this.btnLoadModule.UseVisualStyleBackColor = true;
            this.btnLoadModule.Click += new System.EventHandler(this.btnLoadModule_Click);
            // 
            // txtJsonOutput
            // 
            this.txtJsonOutput.AcceptsTab = true;
            this.txtJsonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJsonOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJsonOutput.Location = new System.Drawing.Point(3, 173);
            this.txtJsonOutput.Name = "txtJsonOutput";
            this.txtJsonOutput.Size = new System.Drawing.Size(525, 258);
            this.txtJsonOutput.TabIndex = 1;
            this.txtJsonOutput.Text = "";
            // 
            // watchVariablePanelParams
            // 
            this.watchVariablePanelParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.watchVariablePanelParams.AutoScroll = true;
            this.watchVariablePanelParams.DataPath = null;
            this.watchVariablePanelParams.elementNameWidth = null;
            this.watchVariablePanelParams.elementValueWidth = 130;
            this.watchVariablePanelParams.Location = new System.Drawing.Point(100, 24);
            this.watchVariablePanelParams.Name = "watchVariablePanelParams";
            this.watchVariablePanelParams.Size = new System.Drawing.Size(267, 436);
            this.watchVariablePanelParams.TabIndex = 2;
            // 
            // btnApplyKnownStates
            // 
            this.btnApplyKnownStates.Location = new System.Drawing.Point(3, 82);
            this.btnApplyKnownStates.Name = "btnApplyKnownStates";
            this.btnApplyKnownStates.Size = new System.Drawing.Size(91, 23);
            this.btnApplyKnownStates.TabIndex = 0;
            this.btnApplyKnownStates.Text = "Apply State";
            this.btnApplyKnownStates.UseVisualStyleBackColor = true;
            this.btnApplyKnownStates.Click += new System.EventHandler(this.btnApplyKnownStates_Click);
            // 
            // btnRun
            // 
            this.btnRun.Enabled = false;
            this.btnRun.Location = new System.Drawing.Point(3, 111);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(91, 23);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Run!";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // labelM64
            // 
            this.labelM64.AutoSize = true;
            this.labelM64.Location = new System.Drawing.Point(97, 8);
            this.labelM64.Name = "labelM64";
            this.labelM64.Size = new System.Drawing.Size(87, 13);
            this.labelM64.TabIndex = 4;
            this.labelM64.Text = "No m64 selected";
            // 
            // btnChooseM64
            // 
            this.btnChooseM64.Location = new System.Drawing.Point(3, 53);
            this.btnChooseM64.Name = "btnChooseM64";
            this.btnChooseM64.Size = new System.Drawing.Size(91, 23);
            this.btnChooseM64.TabIndex = 5;
            this.btnChooseM64.Text = "Choose .m64";
            this.btnChooseM64.UseVisualStyleBackColor = true;
            this.btnChooseM64.Click += new System.EventHandler(this.btnChooseM64_Click);
            // 
            // txtManualConfig
            // 
            this.txtManualConfig.AcceptsTab = true;
            this.txtManualConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtManualConfig.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.txtManualConfig.Location = new System.Drawing.Point(6, 20);
            this.txtManualConfig.Name = "txtManualConfig";
            this.txtManualConfig.Size = new System.Drawing.Size(522, 134);
            this.txtManualConfig.TabIndex = 6;
            this.txtManualConfig.Text = "";
            // 
            // lblGeneratedJson
            // 
            this.lblGeneratedJson.AutoSize = true;
            this.lblGeneratedJson.Location = new System.Drawing.Point(3, 157);
            this.lblGeneratedJson.Name = "lblGeneratedJson";
            this.lblGeneratedJson.Size = new System.Drawing.Size(85, 13);
            this.lblGeneratedJson.TabIndex = 4;
            this.lblGeneratedJson.Text = "Generated Json:";
            // 
            // lblCustomJson
            // 
            this.lblCustomJson.AutoSize = true;
            this.lblCustomJson.Location = new System.Drawing.Point(3, 4);
            this.lblCustomJson.Name = "lblCustomJson";
            this.lblCustomJson.Size = new System.Drawing.Size(70, 13);
            this.lblCustomJson.TabIndex = 4;
            this.lblCustomJson.Text = "Custom Json:";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveConfig.Location = new System.Drawing.Point(3, 434);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(91, 23);
            this.btnSaveConfig.TabIndex = 7;
            this.btnSaveConfig.Text = "Save Config";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadConfig.Location = new System.Drawing.Point(3, 405);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(91, 23);
            this.btnLoadConfig.TabIndex = 7;
            this.btnLoadConfig.Text = "Load Config";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // tabData
            // 
            this.tabData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabData.Controls.Add(this.tabJson);
            this.tabData.Controls.Add(this.tabSurface);
            this.tabData.Location = new System.Drawing.Point(373, 3);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(539, 460);
            this.tabData.TabIndex = 8;
            // 
            // tabJson
            // 
            this.tabJson.Controls.Add(this.lblCustomJson);
            this.tabJson.Controls.Add(this.txtJsonOutput);
            this.tabJson.Controls.Add(this.lblGeneratedJson);
            this.tabJson.Controls.Add(this.txtManualConfig);
            this.tabJson.Location = new System.Drawing.Point(4, 22);
            this.tabJson.Name = "tabJson";
            this.tabJson.Padding = new System.Windows.Forms.Padding(3);
            this.tabJson.Size = new System.Drawing.Size(531, 434);
            this.tabJson.TabIndex = 0;
            this.tabJson.Text = "Json";
            this.tabJson.UseVisualStyleBackColor = true;
            // 
            // tabSurface
            // 
            this.tabSurface.Location = new System.Drawing.Point(4, 22);
            this.tabSurface.Name = "tabSurface";
            this.tabSurface.Padding = new System.Windows.Forms.Padding(3);
            this.tabSurface.Size = new System.Drawing.Size(531, 434);
            this.tabSurface.TabIndex = 1;
            this.tabSurface.Text = "Surface";
            this.tabSurface.UseVisualStyleBackColor = true;
            // 
            // BruteforceTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabData);
            this.Controls.Add(this.btnLoadConfig);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.btnChooseM64);
            this.Controls.Add(this.labelM64);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.watchVariablePanelParams);
            this.Controls.Add(this.btnApplyKnownStates);
            this.Controls.Add(this.btnLoadModule);
            this.Name = "BruteforceTab";
            this.tabData.ResumeLayout(false);
            this.tabJson.ResumeLayout(false);
            this.tabJson.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadModule;
        private System.Windows.Forms.RichTextBox txtJsonOutput;
        private Controls.WatchVariablePanel watchVariablePanelParams;
        private System.Windows.Forms.Button btnApplyKnownStates;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label labelM64;
        private System.Windows.Forms.Button btnChooseM64;
        private System.Windows.Forms.RichTextBox txtManualConfig;
        private System.Windows.Forms.Label lblGeneratedJson;
        private System.Windows.Forms.Label lblCustomJson;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.TabControl tabData;
        private System.Windows.Forms.TabPage tabJson;
        private System.Windows.Forms.TabPage tabSurface;
    }
}
