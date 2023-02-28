namespace STROOP.Tabs.BruteforceTab.Surfaces.GeneralPurpose
{
    partial class GeneralPurpose
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
            this.flowPanelScoring = new STROOP.Controls.ReorderFlowLayoutPanel();
            this.btnAddMethod = new System.Windows.Forms.Button();
            this.btnAddPerturbator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowPanelScoring
            // 
            this.flowPanelScoring.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowPanelScoring.AutoScroll = true;
            this.flowPanelScoring.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelScoring.Location = new System.Drawing.Point(133, 3);
            this.flowPanelScoring.Name = "flowPanelScoring";
            this.flowPanelScoring.Size = new System.Drawing.Size(639, 394);
            this.flowPanelScoring.TabIndex = 0;
            // 
            // btnAddMethod
            // 
            this.btnAddMethod.Location = new System.Drawing.Point(3, 3);
            this.btnAddMethod.Name = "btnAddMethod";
            this.btnAddMethod.Size = new System.Drawing.Size(124, 23);
            this.btnAddMethod.TabIndex = 1;
            this.btnAddMethod.Text = "Add Scoring Method";
            this.btnAddMethod.UseVisualStyleBackColor = true;
            this.btnAddMethod.Click += new System.EventHandler(this.btnAddMethod_Click);
            // 
            // btnAddPerturbator
            // 
            this.btnAddPerturbator.Location = new System.Drawing.Point(3, 32);
            this.btnAddPerturbator.Name = "btnAddPerturbator";
            this.btnAddPerturbator.Size = new System.Drawing.Size(124, 23);
            this.btnAddPerturbator.TabIndex = 2;
            this.btnAddPerturbator.Text = "Add Perturbator";
            this.btnAddPerturbator.UseVisualStyleBackColor = true;
            this.btnAddPerturbator.Click += new System.EventHandler(this.btnAddPerturbator_Click);
            // 
            // GeneralPurpose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddPerturbator);
            this.Controls.Add(this.flowPanelScoring);
            this.Controls.Add(this.btnAddMethod);
            this.Name = "GeneralPurpose";
            this.Size = new System.Drawing.Size(775, 400);
            this.ResumeLayout(false);

        }

        #endregion

        private STROOP.Controls.ReorderFlowLayoutPanel flowPanelScoring;
        private System.Windows.Forms.Button btnAddMethod;
        private System.Windows.Forms.Button btnAddPerturbator;
    }
}
