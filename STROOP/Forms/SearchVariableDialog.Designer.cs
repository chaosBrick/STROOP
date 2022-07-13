namespace STROOP.Forms
{
    partial class SearchVariableDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSearchQuery = new System.Windows.Forms.TextBox();
            this.lblSearchQuery = new System.Windows.Forms.Label();
            this.gbSearchOptions = new System.Windows.Forms.GroupBox();
            this.chkDollarWildcard = new System.Windows.Forms.CheckBox();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.chkSearchHidden = new System.Windows.Forms.CheckBox();
            this.gbSearchOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSearchQuery
            // 
            this.txtSearchQuery.Location = new System.Drawing.Point(12, 25);
            this.txtSearchQuery.Name = "txtSearchQuery";
            this.txtSearchQuery.Size = new System.Drawing.Size(175, 20);
            this.txtSearchQuery.TabIndex = 0;
            // 
            // lblSearchQuery
            // 
            this.lblSearchQuery.AutoSize = true;
            this.lblSearchQuery.Location = new System.Drawing.Point(12, 9);
            this.lblSearchQuery.Name = "lblSearchQuery";
            this.lblSearchQuery.Size = new System.Drawing.Size(56, 13);
            this.lblSearchQuery.TabIndex = 1;
            this.lblSearchQuery.Text = "Search for";
            // 
            // gbSearchOptions
            // 
            this.gbSearchOptions.Controls.Add(this.chkSearchHidden);
            this.gbSearchOptions.Controls.Add(this.chkDollarWildcard);
            this.gbSearchOptions.Controls.Add(this.chkCaseSensitive);
            this.gbSearchOptions.Location = new System.Drawing.Point(12, 51);
            this.gbSearchOptions.Name = "gbSearchOptions";
            this.gbSearchOptions.Size = new System.Drawing.Size(175, 85);
            this.gbSearchOptions.TabIndex = 2;
            this.gbSearchOptions.TabStop = false;
            this.gbSearchOptions.Text = "Search Options";
            // 
            // chkDollarWildcard
            // 
            this.chkDollarWildcard.AutoSize = true;
            this.chkDollarWildcard.Location = new System.Drawing.Point(6, 39);
            this.chkDollarWildcard.Name = "chkDollarWildcard";
            this.chkDollarWildcard.Size = new System.Drawing.Size(110, 17);
            this.chkDollarWildcard.TabIndex = 0;
            this.chkDollarWildcard.Text = "Use $ as wildcard";
            this.chkDollarWildcard.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(6, 19);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(94, 17);
            this.chkCaseSensitive.TabIndex = 0;
            this.chkCaseSensitive.Text = "Case sensitive";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // chkSearchHidden
            // 
            this.chkSearchHidden.AutoSize = true;
            this.chkSearchHidden.Location = new System.Drawing.Point(6, 62);
            this.chkSearchHidden.Name = "chkSearchHidden";
            this.chkSearchHidden.Size = new System.Drawing.Size(140, 17);
            this.chkSearchHidden.TabIndex = 0;
            this.chkSearchHidden.Text = "Search hidden variables";
            this.chkSearchHidden.UseVisualStyleBackColor = true;
            // 
            // SearchVariableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 149);
            this.Controls.Add(this.gbSearchOptions);
            this.Controls.Add(this.lblSearchQuery);
            this.Controls.Add(this.txtSearchQuery);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SearchVariableDialog";
            this.Opacity = 0.5D;
            this.ShowInTaskbar = false;
            this.Text = "Search Variables...";
            this.gbSearchOptions.ResumeLayout(false);
            this.gbSearchOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchQuery;
        private System.Windows.Forms.Label lblSearchQuery;
        private System.Windows.Forms.GroupBox gbSearchOptions;
        private System.Windows.Forms.CheckBox chkDollarWildcard;
        private System.Windows.Forms.CheckBox chkCaseSensitive;
        private System.Windows.Forms.CheckBox chkSearchHidden;
    }
}