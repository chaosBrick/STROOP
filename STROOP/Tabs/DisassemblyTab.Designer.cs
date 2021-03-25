namespace STROOP.Tabs
{
    partial class DisassemblyTab
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
            this.textBoxDisAddress = new System.Windows.Forms.TextBox();
            this.buttonDisMore = new System.Windows.Forms.Button();
            this.buttonDisGo = new System.Windows.Forms.Button();
            this.labelDisStart = new System.Windows.Forms.Label();
            this.richTextBoxDissasembly = new STROOP.Controls.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // textBoxDisAddress
            // 
            this.textBoxDisAddress.Location = new System.Drawing.Point(72, 3);
            this.textBoxDisAddress.Name = "textBoxDisAddress";
            this.textBoxDisAddress.Size = new System.Drawing.Size(93, 20);
            this.textBoxDisAddress.TabIndex = 12;
            // 
            // buttonDisMore
            // 
            this.buttonDisMore.Location = new System.Drawing.Point(236, 2);
            this.buttonDisMore.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDisMore.Name = "buttonDisMore";
            this.buttonDisMore.Size = new System.Drawing.Size(50, 20);
            this.buttonDisMore.TabIndex = 11;
            this.buttonDisMore.Text = "More";
            this.buttonDisMore.UseVisualStyleBackColor = true;
            this.buttonDisMore.Visible = false;
            // 
            // buttonDisGo
            // 
            this.buttonDisGo.Location = new System.Drawing.Point(170, 2);
            this.buttonDisGo.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDisGo.Name = "buttonDisGo";
            this.buttonDisGo.Size = new System.Drawing.Size(62, 20);
            this.buttonDisGo.TabIndex = 10;
            this.buttonDisGo.Text = "Go";
            this.buttonDisGo.UseVisualStyleBackColor = true;
            // 
            // labelDisStart
            // 
            this.labelDisStart.AutoSize = true;
            this.labelDisStart.Location = new System.Drawing.Point(1, 6);
            this.labelDisStart.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDisStart.Name = "labelDisStart";
            this.labelDisStart.Size = new System.Drawing.Size(73, 13);
            this.labelDisStart.TabIndex = 9;
            this.labelDisStart.Text = "Start Address:";
            // 
            // richTextBoxDissasembly
            // 
            this.richTextBoxDissasembly.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDissasembly.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxDissasembly.Location = new System.Drawing.Point(2, 26);
            this.richTextBoxDissasembly.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBoxDissasembly.Name = "richTextBoxDissasembly";
            this.richTextBoxDissasembly.ReadOnly = true;
            this.richTextBoxDissasembly.Size = new System.Drawing.Size(187, 46);
            this.richTextBoxDissasembly.TabIndex = 8;
            this.richTextBoxDissasembly.Text = "";
            // 
            // DisassemblyTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxDisAddress);
            this.Controls.Add(this.buttonDisMore);
            this.Controls.Add(this.buttonDisGo);
            this.Controls.Add(this.labelDisStart);
            this.Controls.Add(this.richTextBoxDissasembly);
            this.Name = "DisassemblyTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDisAddress;
        private System.Windows.Forms.Button buttonDisMore;
        private System.Windows.Forms.Button buttonDisGo;
        private System.Windows.Forms.Label labelDisStart;
        private Controls.RichTextBoxEx richTextBoxDissasembly;
    }
}
