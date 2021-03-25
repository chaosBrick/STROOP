namespace STROOP.Tabs
{
    partial class ScriptTab
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
            this.splitContainerScript = new STROOP.BetterSplitContainer();
            this.splitContainerScriptLeft = new STROOP.BetterSplitContainer();
            this.checkBoxScriptRunContinuously = new System.Windows.Forms.CheckBox();
            this.buttonScriptExamples = new System.Windows.Forms.Button();
            this.buttonScriptRunOnce = new System.Windows.Forms.Button();
            this.buttonScriptInstructions = new System.Windows.Forms.Button();
            this.richTextBoxScript = new STROOP.Controls.RichTextBoxEx();
            this.watchVariablePanelScript = new STROOP.Controls.WatchVariableFlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerScript)).BeginInit();
            this.splitContainerScript.Panel1.SuspendLayout();
            this.splitContainerScript.Panel2.SuspendLayout();
            this.splitContainerScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerScriptLeft)).BeginInit();
            this.splitContainerScriptLeft.Panel1.SuspendLayout();
            this.splitContainerScriptLeft.Panel2.SuspendLayout();
            this.splitContainerScriptLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerScript
            // 
            this.splitContainerScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerScript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerScript.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerScript.Location = new System.Drawing.Point(0, 0);
            this.splitContainerScript.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerScript.Name = "splitContainerScript";
            // 
            // splitContainerScript.Panel1
            // 
            this.splitContainerScript.Panel1.AutoScroll = true;
            this.splitContainerScript.Panel1.Controls.Add(this.splitContainerScriptLeft);
            this.splitContainerScript.Panel1MinSize = 0;
            // 
            // splitContainerScript.Panel2
            // 
            this.splitContainerScript.Panel2.Controls.Add(this.watchVariablePanelScript);
            this.splitContainerScript.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this.splitContainerScript.Panel2MinSize = 0;
            this.splitContainerScript.Size = new System.Drawing.Size(915, 463);
            this.splitContainerScript.SplitterDistance = 457;
            this.splitContainerScript.SplitterWidth = 1;
            this.splitContainerScript.TabIndex = 40;
            // 
            // splitContainerScriptLeft
            // 
            this.splitContainerScriptLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerScriptLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerScriptLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerScriptLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerScriptLeft.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerScriptLeft.Name = "splitContainerScriptLeft";
            this.splitContainerScriptLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerScriptLeft.Panel1
            // 
            this.splitContainerScriptLeft.Panel1.AutoScroll = true;
            this.splitContainerScriptLeft.Panel1.Controls.Add(this.checkBoxScriptRunContinuously);
            this.splitContainerScriptLeft.Panel1.Controls.Add(this.buttonScriptExamples);
            this.splitContainerScriptLeft.Panel1.Controls.Add(this.buttonScriptRunOnce);
            this.splitContainerScriptLeft.Panel1.Controls.Add(this.buttonScriptInstructions);
            this.splitContainerScriptLeft.Panel1MinSize = 0;
            // 
            // splitContainerScriptLeft.Panel2
            // 
            this.splitContainerScriptLeft.Panel2.Controls.Add(this.richTextBoxScript);
            this.splitContainerScriptLeft.Panel2MinSize = 0;
            this.splitContainerScriptLeft.Size = new System.Drawing.Size(457, 463);
            this.splitContainerScriptLeft.SplitterDistance = 46;
            this.splitContainerScriptLeft.SplitterWidth = 1;
            this.splitContainerScriptLeft.TabIndex = 38;
            // 
            // checkBoxScriptRunContinuously
            // 
            this.checkBoxScriptRunContinuously.AutoSize = true;
            this.checkBoxScriptRunContinuously.Location = new System.Drawing.Point(7, 15);
            this.checkBoxScriptRunContinuously.Name = "checkBoxScriptRunContinuously";
            this.checkBoxScriptRunContinuously.Size = new System.Drawing.Size(109, 17);
            this.checkBoxScriptRunContinuously.TabIndex = 37;
            this.checkBoxScriptRunContinuously.Text = "Run Continuously";
            this.checkBoxScriptRunContinuously.UseVisualStyleBackColor = true;
            // 
            // buttonScriptExamples
            // 
            this.buttonScriptExamples.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonScriptExamples.Location = new System.Drawing.Point(339, 9);
            this.buttonScriptExamples.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScriptExamples.Name = "buttonScriptExamples";
            this.buttonScriptExamples.Size = new System.Drawing.Size(105, 28);
            this.buttonScriptExamples.TabIndex = 20;
            this.buttonScriptExamples.Text = "Examples";
            this.buttonScriptExamples.UseVisualStyleBackColor = true;
            // 
            // buttonScriptRunOnce
            // 
            this.buttonScriptRunOnce.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonScriptRunOnce.Location = new System.Drawing.Point(121, 9);
            this.buttonScriptRunOnce.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScriptRunOnce.Name = "buttonScriptRunOnce";
            this.buttonScriptRunOnce.Size = new System.Drawing.Size(105, 28);
            this.buttonScriptRunOnce.TabIndex = 20;
            this.buttonScriptRunOnce.Text = "Run Once";
            this.buttonScriptRunOnce.UseVisualStyleBackColor = true;
            // 
            // buttonScriptInstructions
            // 
            this.buttonScriptInstructions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonScriptInstructions.Location = new System.Drawing.Point(230, 9);
            this.buttonScriptInstructions.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScriptInstructions.Name = "buttonScriptInstructions";
            this.buttonScriptInstructions.Size = new System.Drawing.Size(105, 28);
            this.buttonScriptInstructions.TabIndex = 20;
            this.buttonScriptInstructions.Text = "Instructions";
            this.buttonScriptInstructions.UseVisualStyleBackColor = true;
            // 
            // richTextBoxScript
            // 
            this.richTextBoxScript.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxScript.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxScript.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxScript.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxScript.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBoxScript.Name = "richTextBoxScript";
            this.richTextBoxScript.Size = new System.Drawing.Size(455, 414);
            this.richTextBoxScript.TabIndex = 9;
            this.richTextBoxScript.Text = "";
            // 
            // watchVariablePanelScript
            // 
            this.watchVariablePanelScript.AutoScroll = true;
            this.watchVariablePanelScript.DataPath = "Config/ScriptData.xml";
            this.watchVariablePanelScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.watchVariablePanelScript.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.watchVariablePanelScript.Location = new System.Drawing.Point(2, 2);
            this.watchVariablePanelScript.Margin = new System.Windows.Forms.Padding(0);
            this.watchVariablePanelScript.Name = "watchVariablePanelScript";
            this.watchVariablePanelScript.Size = new System.Drawing.Size(451, 457);
            this.watchVariablePanelScript.TabIndex = 7;
            // 
            // ScriptTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerScript);
            this.Name = "ScriptTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.splitContainerScript.Panel1.ResumeLayout(false);
            this.splitContainerScript.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerScript)).EndInit();
            this.splitContainerScript.ResumeLayout(false);
            this.splitContainerScriptLeft.Panel1.ResumeLayout(false);
            this.splitContainerScriptLeft.Panel1.PerformLayout();
            this.splitContainerScriptLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerScriptLeft)).EndInit();
            this.splitContainerScriptLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BetterSplitContainer splitContainerScript;
        private BetterSplitContainer splitContainerScriptLeft;
        private System.Windows.Forms.CheckBox checkBoxScriptRunContinuously;
        private System.Windows.Forms.Button buttonScriptExamples;
        private System.Windows.Forms.Button buttonScriptRunOnce;
        private System.Windows.Forms.Button buttonScriptInstructions;
        private Controls.RichTextBoxEx richTextBoxScript;
        internal Controls.WatchVariableFlowLayoutPanel watchVariablePanelScript;
    }
}
