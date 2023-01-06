namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose
{
    partial class ScoringFunc
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
            this.pbExpand = new System.Windows.Forms.PictureBox();
            this.pbMoveDown = new System.Windows.Forms.PictureBox();
            this.pbMoveUp = new System.Windows.Forms.PictureBox();
            this.pbRemove = new System.Windows.Forms.PictureBox();
            this.groupBoxControllers = new System.Windows.Forms.GroupBox();
            this.panelControllers = new System.Windows.Forms.FlowLayoutPanel();
            this.labelName = new System.Windows.Forms.Label();
            this.watchVariablePanelParameters = new STROOP.Controls.WatchVariablePanel();
            this.variablePanelBaseValues = new STROOP.Controls.WatchVariablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMoveDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMoveUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove)).BeginInit();
            this.groupBoxControllers.SuspendLayout();
            this.panelControllers.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbExpand
            // 
            this.pbExpand.Image = global::STROOP.Properties.Resources.image_left;
            this.pbExpand.InitialImage = global::STROOP.Properties.Resources.image_left;
            this.pbExpand.Location = new System.Drawing.Point(299, 38);
            this.pbExpand.Name = "pbExpand";
            this.pbExpand.Size = new System.Drawing.Size(29, 29);
            this.pbExpand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExpand.TabIndex = 0;
            this.pbExpand.TabStop = false;
            this.pbExpand.Click += new System.EventHandler(this.pbExpand_Click);
            // 
            // pbMoveDown
            // 
            this.pbMoveDown.Image = global::STROOP.Properties.Resources.Down_Arrow;
            this.pbMoveDown.InitialImage = global::STROOP.Properties.Resources.Down_Arrow;
            this.pbMoveDown.Location = new System.Drawing.Point(334, 38);
            this.pbMoveDown.Name = "pbMoveDown";
            this.pbMoveDown.Size = new System.Drawing.Size(29, 29);
            this.pbMoveDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMoveDown.TabIndex = 0;
            this.pbMoveDown.TabStop = false;
            // 
            // pbMoveUp
            // 
            this.pbMoveUp.Image = global::STROOP.Properties.Resources.Up_Arrow;
            this.pbMoveUp.InitialImage = global::STROOP.Properties.Resources.Up_Arrow;
            this.pbMoveUp.Location = new System.Drawing.Point(334, 3);
            this.pbMoveUp.Name = "pbMoveUp";
            this.pbMoveUp.Size = new System.Drawing.Size(29, 29);
            this.pbMoveUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMoveUp.TabIndex = 0;
            this.pbMoveUp.TabStop = false;
            // 
            // pbRemove
            // 
            this.pbRemove.Image = global::STROOP.Properties.Resources.Red_X;
            this.pbRemove.InitialImage = global::STROOP.Properties.Resources.Red_X;
            this.pbRemove.Location = new System.Drawing.Point(299, 3);
            this.pbRemove.Name = "pbRemove";
            this.pbRemove.Size = new System.Drawing.Size(29, 29);
            this.pbRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRemove.TabIndex = 0;
            this.pbRemove.TabStop = false;
            this.pbRemove.Click += new System.EventHandler(this.pbRemove_Click);
            // 
            // groupBoxControllers
            // 
            this.groupBoxControllers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxControllers.Controls.Add(this.panelControllers);
            this.groupBoxControllers.Location = new System.Drawing.Point(6, 19);
            this.groupBoxControllers.Name = "groupBoxControllers";
            this.groupBoxControllers.Size = new System.Drawing.Size(287, 48);
            this.groupBoxControllers.TabIndex = 1;
            this.groupBoxControllers.TabStop = false;
            this.groupBoxControllers.Text = "Controllers";
            // 
            // panelControllers
            // 
            this.panelControllers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControllers.AutoScroll = true;
            this.panelControllers.Controls.Add(this.variablePanelBaseValues);
            this.panelControllers.Location = new System.Drawing.Point(0, 11);
            this.panelControllers.Name = "panelControllers";
            this.panelControllers.Size = new System.Drawing.Size(287, 37);
            this.panelControllers.TabIndex = 0;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(3, 3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(74, 13);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "Method Name";
            // 
            // watchVariablePanelParameters
            // 
            this.watchVariablePanelParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.watchVariablePanelParameters.AutoScroll = true;
            this.watchVariablePanelParameters.DataPath = null;
            this.watchVariablePanelParameters.elementNameWidth = null;
            this.watchVariablePanelParameters.elementValueWidth = null;
            this.watchVariablePanelParameters.Location = new System.Drawing.Point(3, 73);
            this.watchVariablePanelParameters.Name = "watchVariablePanelParameters";
            this.watchVariablePanelParameters.Size = new System.Drawing.Size(360, 167);
            this.watchVariablePanelParameters.TabIndex = 2;
            // 
            // variablePanelBaseValues
            // 
            this.variablePanelBaseValues.AutoScroll = true;
            this.variablePanelBaseValues.DataPath = null;
            this.variablePanelBaseValues.elementNameWidth = 40;
            this.variablePanelBaseValues.elementValueWidth = 50;
            this.variablePanelBaseValues.Location = new System.Drawing.Point(3, 3);
            this.variablePanelBaseValues.Name = "variablePanelBaseValues";
            this.variablePanelBaseValues.Size = new System.Drawing.Size(200, 28);
            this.variablePanelBaseValues.TabIndex = 0;
            // 
            // ScoringFunc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbRemove);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.pbExpand);
            this.Controls.Add(this.watchVariablePanelParameters);
            this.Controls.Add(this.groupBoxControllers);
            this.Controls.Add(this.pbMoveUp);
            this.Controls.Add(this.pbMoveDown);
            this.Name = "ScoringFunc";
            this.Size = new System.Drawing.Size(366, 243);
            ((System.ComponentModel.ISupportInitialize)(this.pbExpand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMoveDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMoveUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove)).EndInit();
            this.groupBoxControllers.ResumeLayout(false);
            this.panelControllers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbExpand;
        private System.Windows.Forms.PictureBox pbMoveDown;
        private System.Windows.Forms.PictureBox pbMoveUp;
        private System.Windows.Forms.PictureBox pbRemove;
        private System.Windows.Forms.GroupBox groupBoxControllers;
        private System.Windows.Forms.Label labelName;
        public System.Windows.Forms.FlowLayoutPanel panelControllers;
        public Controls.WatchVariablePanel watchVariablePanelParameters;
        private Controls.WatchVariablePanel variablePanelBaseValues;
    }
}
