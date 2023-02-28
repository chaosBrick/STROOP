namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose
{
    partial class Perturbator
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
            this.pbRemove = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.watchVariablePanelParameters = new STROOP.Controls.WatchVariablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove)).BeginInit();
            this.SuspendLayout();
            // 
            // pbExpand
            // 
            this.pbExpand.Image = global::STROOP.Properties.Resources.image_left;
            this.pbExpand.InitialImage = global::STROOP.Properties.Resources.image_left;
            this.pbExpand.Location = new System.Drawing.Point(319, 0);
            this.pbExpand.Name = "pbExpand";
            this.pbExpand.Size = new System.Drawing.Size(20, 21);
            this.pbExpand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExpand.TabIndex = 0;
            this.pbExpand.TabStop = false;
            this.pbExpand.Click += new System.EventHandler(this.pbExpand_Click);
            // 
            // pbRemove
            // 
            this.pbRemove.Image = global::STROOP.Properties.Resources.Red_X;
            this.pbRemove.InitialImage = global::STROOP.Properties.Resources.Red_X;
            this.pbRemove.Location = new System.Drawing.Point(345, 0);
            this.pbRemove.Name = "pbRemove";
            this.pbRemove.Size = new System.Drawing.Size(21, 21);
            this.pbRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRemove.TabIndex = 0;
            this.pbRemove.TabStop = false;
            this.pbRemove.Click += new System.EventHandler(this.pbRemove_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(3, 3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(59, 13);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "Perturbator";
            // 
            // watchVariablePanelParameters
            // 
            this.watchVariablePanelParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.watchVariablePanelParameters.AutoScroll = true;
            this.watchVariablePanelParameters.DataPath = null;
            this.watchVariablePanelParameters.elementNameWidth = null;
            this.watchVariablePanelParameters.elementValueWidth = null;
            this.watchVariablePanelParameters.Location = new System.Drawing.Point(0, 19);
            this.watchVariablePanelParameters.Name = "watchVariablePanelParameters";
            this.watchVariablePanelParameters.Size = new System.Drawing.Size(366, 224);
            this.watchVariablePanelParameters.TabIndex = 2;
            // 
            // Perturbator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbRemove);
            this.Controls.Add(this.pbExpand);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.watchVariablePanelParameters);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Perturbator";
            this.Size = new System.Drawing.Size(366, 243);
            ((System.ComponentModel.ISupportInitialize)(this.pbExpand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbExpand;
        private System.Windows.Forms.PictureBox pbRemove;
        private System.Windows.Forms.Label labelName;
        public Controls.WatchVariablePanel watchVariablePanelParameters;
    }
}
