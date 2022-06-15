namespace STROOP.Tabs
{
    partial class TasTab
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
            this.splitContainerTas = new STROOP.BetterSplitContainer();
            this.buttonTasPasteSchedule = new System.Windows.Forms.Button();
            this.buttonTasStoreAngle = new System.Windows.Forms.Button();
            this.buttonTasTakeAngle = new System.Windows.Forms.Button();
            this.buttonTasTakePosition = new System.Windows.Forms.Button();
            this.buttonTasStorePosition = new System.Windows.Forms.Button();
            this.watchVariablePanelTas = new STROOP.Controls.WatchVariablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTas)).BeginInit();
            this.splitContainerTas.Panel1.SuspendLayout();
            this.splitContainerTas.Panel2.SuspendLayout();
            this.splitContainerTas.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerTas
            // 
            this.splitContainerTas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerTas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTas.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerTas.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTas.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerTas.Name = "splitContainerTas";
            // 
            // splitContainerTas.Panel1
            // 
            this.splitContainerTas.Panel1.AutoScroll = true;
            this.splitContainerTas.Panel1.Controls.Add(this.buttonTasPasteSchedule);
            this.splitContainerTas.Panel1.Controls.Add(this.buttonTasStoreAngle);
            this.splitContainerTas.Panel1.Controls.Add(this.buttonTasTakeAngle);
            this.splitContainerTas.Panel1.Controls.Add(this.buttonTasTakePosition);
            this.splitContainerTas.Panel1.Controls.Add(this.buttonTasStorePosition);
            this.splitContainerTas.Panel1MinSize = 0;
            // 
            // splitContainerTas.Panel2
            // 
            this.splitContainerTas.Panel2.Controls.Add(this.watchVariablePanelTas);
            this.splitContainerTas.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this.splitContainerTas.Panel2MinSize = 0;
            this.splitContainerTas.Size = new System.Drawing.Size(915, 463);
            this.splitContainerTas.SplitterDistance = 212;
            this.splitContainerTas.SplitterWidth = 1;
            this.splitContainerTas.TabIndex = 20;
            // 
            // buttonTasPasteSchedule
            // 
            this.buttonTasPasteSchedule.Location = new System.Drawing.Point(2, 54);
            this.buttonTasPasteSchedule.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTasPasteSchedule.Name = "buttonTasPasteSchedule";
            this.buttonTasPasteSchedule.Size = new System.Drawing.Size(189, 25);
            this.buttonTasPasteSchedule.TabIndex = 37;
            this.buttonTasPasteSchedule.Text = "Paste Schedule";
            this.buttonTasPasteSchedule.UseVisualStyleBackColor = true;
            // 
            // buttonTasStoreAngle
            // 
            this.buttonTasStoreAngle.Location = new System.Drawing.Point(2, 28);
            this.buttonTasStoreAngle.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTasStoreAngle.Name = "buttonTasStoreAngle";
            this.buttonTasStoreAngle.Size = new System.Drawing.Size(93, 25);
            this.buttonTasStoreAngle.TabIndex = 37;
            this.buttonTasStoreAngle.Text = "Store Angle";
            this.buttonTasStoreAngle.UseVisualStyleBackColor = true;
            // 
            // buttonTasTakeAngle
            // 
            this.buttonTasTakeAngle.Location = new System.Drawing.Point(98, 28);
            this.buttonTasTakeAngle.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTasTakeAngle.Name = "buttonTasTakeAngle";
            this.buttonTasTakeAngle.Size = new System.Drawing.Size(93, 25);
            this.buttonTasTakeAngle.TabIndex = 37;
            this.buttonTasTakeAngle.Text = "Take Angle";
            this.buttonTasTakeAngle.UseVisualStyleBackColor = true;
            // 
            // buttonTasTakePosition
            // 
            this.buttonTasTakePosition.Location = new System.Drawing.Point(98, 2);
            this.buttonTasTakePosition.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTasTakePosition.Name = "buttonTasTakePosition";
            this.buttonTasTakePosition.Size = new System.Drawing.Size(93, 25);
            this.buttonTasTakePosition.TabIndex = 37;
            this.buttonTasTakePosition.Text = "Take Position";
            this.buttonTasTakePosition.UseVisualStyleBackColor = true;
            // 
            // buttonTasStorePosition
            // 
            this.buttonTasStorePosition.Location = new System.Drawing.Point(2, 2);
            this.buttonTasStorePosition.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTasStorePosition.Name = "buttonTasStorePosition";
            this.buttonTasStorePosition.Size = new System.Drawing.Size(93, 25);
            this.buttonTasStorePosition.TabIndex = 37;
            this.buttonTasStorePosition.Text = "Store Position";
            this.buttonTasStorePosition.UseVisualStyleBackColor = true;
            // 
            // watchVariablePanelTas
            // 
            this.watchVariablePanelTas.DataPath = "Config/TasData.xml";
            this.watchVariablePanelTas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.watchVariablePanelTas.Location = new System.Drawing.Point(2, 2);
            this.watchVariablePanelTas.Margin = new System.Windows.Forms.Padding(0);
            this.watchVariablePanelTas.Name = "watchVariablePanelTas";
            this.watchVariablePanelTas.Size = new System.Drawing.Size(696, 457);
            this.watchVariablePanelTas.TabIndex = 5;
            // 
            // TasTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerTas);
            this.Name = "TasTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.splitContainerTas.Panel1.ResumeLayout(false);
            this.splitContainerTas.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTas)).EndInit();
            this.splitContainerTas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BetterSplitContainer splitContainerTas;
        private System.Windows.Forms.Button buttonTasPasteSchedule;
        private System.Windows.Forms.Button buttonTasStoreAngle;
        private System.Windows.Forms.Button buttonTasTakeAngle;
        private System.Windows.Forms.Button buttonTasTakePosition;
        private System.Windows.Forms.Button buttonTasStorePosition;
        private Controls.WatchVariablePanel watchVariablePanelTas;
    }
}
