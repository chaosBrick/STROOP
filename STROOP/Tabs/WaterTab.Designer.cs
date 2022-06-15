namespace STROOP.Tabs
{
    partial class WaterTab
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
            this.watchVariablePanelWater = new STROOP.Controls.WatchVariablePanel();
            this.SuspendLayout();
            // 
            // watchVariablePanelWater
            // 
            this.watchVariablePanelWater.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.watchVariablePanelWater.DataPath = "Config/WaterData.xml";
            this.watchVariablePanelWater.Location = new System.Drawing.Point(0, 0);
            this.watchVariablePanelWater.Name = "watchVariablePanelWater";
            this.watchVariablePanelWater.Size = new System.Drawing.Size(915, 463);
            this.watchVariablePanelWater.TabIndex = 0;
            // 
            // WaterTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.watchVariablePanelWater);
            this.Name = "WaterTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WatchVariablePanel watchVariablePanelWater;
    }
}
