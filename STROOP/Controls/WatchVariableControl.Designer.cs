namespace STROOP.Controls
{
    partial class WatchVariableControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tableLayoutPanel = new STROOP.Controls.BorderedTableLayoutPanel();
            this._namePanel = new System.Windows.Forms.Panel();
            this._pinPictureBox = new System.Windows.Forms.PictureBox();
            this._lockPictureBox = new System.Windows.Forms.PictureBox();
            this._nameTextBox = new STROOP.CarretlessTextBox();
            this._valueControlContainer = new System.Windows.Forms.Panel();
            this._tableLayoutPanel.SuspendLayout();
            this._namePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pinPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._lockPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this._tableLayoutPanel.BorderColor = System.Drawing.Color.Black;
            this._tableLayoutPanel.BorderWidth = 1F;
            this._tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this._tableLayoutPanel.ColumnCount = 2;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this._tableLayoutPanel.Controls.Add(this._namePanel, 0, 0);
            this._tableLayoutPanel.Controls.Add(this._valueControlContainer, 1, 0);
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 1;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanel.ShowBorder = false;
            this._tableLayoutPanel.Size = new System.Drawing.Size(205, 22);
            this._tableLayoutPanel.TabIndex = 0;
            // 
            // _namePanel
            // 
            this._namePanel.BackColor = System.Drawing.Color.Transparent;
            this._namePanel.Controls.Add(this._pinPictureBox);
            this._namePanel.Controls.Add(this._lockPictureBox);
            this._namePanel.Controls.Add(this._nameTextBox);
            this._namePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._namePanel.Location = new System.Drawing.Point(1, 1);
            this._namePanel.Margin = new System.Windows.Forms.Padding(0);
            this._namePanel.Name = "_namePanel";
            this._namePanel.Size = new System.Drawing.Size(120, 20);
            this._namePanel.TabIndex = 0;
            // 
            // _pinPictureBox
            // 
            this._pinPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._pinPictureBox.Image = global::STROOP.Properties.Resources.img_pin;
            this._pinPictureBox.Location = new System.Drawing.Point(109, 3);
            this._pinPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this._pinPictureBox.Name = "_pinPictureBox";
            this._pinPictureBox.Size = new System.Drawing.Size(10, 17);
            this._pinPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._pinPictureBox.TabIndex = 2;
            this._pinPictureBox.TabStop = false;
            this._pinPictureBox.Visible = false;
            // 
            // _lockPictureBox
            // 
            this._lockPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._lockPictureBox.Image = global::STROOP.Properties.Resources.img_lock;
            this._lockPictureBox.Location = new System.Drawing.Point(104, 2);
            this._lockPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this._lockPictureBox.Name = "_lockPictureBox";
            this._lockPictureBox.Size = new System.Drawing.Size(16, 18);
            this._lockPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._lockPictureBox.TabIndex = 1;
            this._lockPictureBox.TabStop = false;
            this._lockPictureBox.Visible = false;
            // 
            // _nameTextBox
            // 
            this._nameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._nameTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this._nameTextBox.Location = new System.Drawing.Point(4, 4);
            this._nameTextBox.Name = "_nameTextBox";
            this._nameTextBox.ReadOnly = true;
            this._nameTextBox.Size = new System.Drawing.Size(200, 13);
            this._nameTextBox.TabIndex = 0;
            // 
            // _valueControlContainer
            // 
            this._valueControlContainer.Location = new System.Drawing.Point(125, 4);
            this._valueControlContainer.Name = "valueControlContainer";
            this._valueControlContainer.Size = new System.Drawing.Size(77, 14);
            this._valueControlContainer.TabIndex = 1;
            // 
            // WatchVariableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._tableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WatchVariableControl";
            this.Size = new System.Drawing.Size(205, 22);
            this._tableLayoutPanel.ResumeLayout(false);
            this._namePanel.ResumeLayout(false);
            this._namePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pinPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._lockPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BorderedTableLayoutPanel _tableLayoutPanel;
        private System.Windows.Forms.Panel _namePanel;
        private CarretlessTextBox _nameTextBox;
        private System.Windows.Forms.PictureBox _pinPictureBox;
        private System.Windows.Forms.PictureBox _lockPictureBox;
        private System.Windows.Forms.Panel _valueControlContainer;
    }
}
