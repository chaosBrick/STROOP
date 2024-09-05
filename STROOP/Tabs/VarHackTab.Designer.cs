using STROOP.Controls;

namespace STROOP.Tabs
{
    partial class VarHackTab
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
            this.splitContainerVarHack = new BetterSplitContainer();
            this.buttonVarHackApplyVariablesToMemory = new System.Windows.Forms.Button();
            this.buttonEnableDisableRomHack = new BinaryButton();
            this.buttonVarHackClearVariablesInMemory = new System.Windows.Forms.Button();
            this.textBoxYDeltaValue = new BetterTextbox();
            this.textBoxYPosValue = new BetterTextbox();
            this.textBoxYDeltaChange = new BetterTextbox();
            this.textBoxYPosChange = new BetterTextbox();
            this.textBoxXPosChange = new BetterTextbox();
            this.textBoxXPosValue = new BetterTextbox();
            this.labelVarHackYDeltaLabel = new System.Windows.Forms.Label();
            this.labelVarHackYPosLabel = new System.Windows.Forms.Label();
            this.labelVarHackXPosLabel = new System.Windows.Forms.Label();
            this.buttonYDeltaAdd = new System.Windows.Forms.Button();
            this.buttonYDeltaSubtract = new System.Windows.Forms.Button();
            this.buttonYPosAdd = new System.Windows.Forms.Button();
            this.buttonYPosSubtract = new System.Windows.Forms.Button();
            this.buttonXPosAdd = new System.Windows.Forms.Button();
            this.buttonXPosSubtract = new System.Windows.Forms.Button();
            this.buttonSetPositionsAndApplyVariablesToMemory = new System.Windows.Forms.Button();
            this.buttonVarHackAddNewVariable = new System.Windows.Forms.Button();
            this.buttonVarHackShowVariableBytesInBigEndian = new System.Windows.Forms.Button();
            this.buttonVarHackShowVariableBytesInLittleEndian = new System.Windows.Forms.Button();
            this.buttonVarHackOpenVars = new System.Windows.Forms.Button();
            this.buttonVarHackSaveVars = new System.Windows.Forms.Button();
            this.buttonVarHackClearVars = new System.Windows.Forms.Button();
            this.varHackPanel = new STROOP.Controls.VarHackFlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVarHack)).BeginInit();
            this.splitContainerVarHack.Panel1.SuspendLayout();
            this.splitContainerVarHack.Panel2.SuspendLayout();
            this.splitContainerVarHack.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerVarHack
            // 
            this.splitContainerVarHack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerVarHack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerVarHack.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerVarHack.Location = new System.Drawing.Point(0, 0);
            this.splitContainerVarHack.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerVarHack.Name = "splitContainerVarHack";
            // 
            // splitContainerVarHack.Panel1
            // 
            this.splitContainerVarHack.Panel1.AutoScroll = true;
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonVarHackApplyVariablesToMemory);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonEnableDisableRomHack);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonVarHackClearVariablesInMemory);
            this.splitContainerVarHack.Panel1.Controls.Add(this.textBoxYDeltaValue);
            this.splitContainerVarHack.Panel1.Controls.Add(this.textBoxYPosValue);
            this.splitContainerVarHack.Panel1.Controls.Add(this.textBoxYDeltaChange);
            this.splitContainerVarHack.Panel1.Controls.Add(this.textBoxYPosChange);
            this.splitContainerVarHack.Panel1.Controls.Add(this.textBoxXPosChange);
            this.splitContainerVarHack.Panel1.Controls.Add(this.textBoxXPosValue);
            this.splitContainerVarHack.Panel1.Controls.Add(this.labelVarHackYDeltaLabel);
            this.splitContainerVarHack.Panel1.Controls.Add(this.labelVarHackYPosLabel);
            this.splitContainerVarHack.Panel1.Controls.Add(this.labelVarHackXPosLabel);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonYDeltaAdd);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonYDeltaSubtract);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonYPosAdd);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonYPosSubtract);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonXPosAdd);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonXPosSubtract);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonSetPositionsAndApplyVariablesToMemory);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonVarHackAddNewVariable);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonVarHackShowVariableBytesInBigEndian);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonVarHackShowVariableBytesInLittleEndian);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonVarHackOpenVars);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonVarHackSaveVars);
            this.splitContainerVarHack.Panel1.Controls.Add(this.buttonVarHackClearVars);
            this.splitContainerVarHack.Panel1MinSize = 0;
            // 
            // splitContainerVarHack.Panel2
            // 
            this.splitContainerVarHack.Panel2.Controls.Add(this.varHackPanel);
            this.splitContainerVarHack.Panel2MinSize = 0;
            this.splitContainerVarHack.Size = new System.Drawing.Size(915, 463);
            this.splitContainerVarHack.SplitterDistance = 217;
            this.splitContainerVarHack.SplitterWidth = 1;
            this.splitContainerVarHack.TabIndex = 21;
            // 
            // buttonVarHackApplyVariablesToMemory
            // 
            this.buttonVarHackApplyVariablesToMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonVarHackApplyVariablesToMemory.Location = new System.Drawing.Point(7, 332);
            this.buttonVarHackApplyVariablesToMemory.Name = "buttonVarHackApplyVariablesToMemory";
            this.buttonVarHackApplyVariablesToMemory.Size = new System.Drawing.Size(188, 38);
            this.buttonVarHackApplyVariablesToMemory.TabIndex = 4;
            this.buttonVarHackApplyVariablesToMemory.Text = "Apply Variables to Memory";
            this.buttonVarHackApplyVariablesToMemory.UseVisualStyleBackColor = true;
            // 
            // buttonEnableDisableRomHack
            // 
            this.buttonEnableDisableRomHack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEnableDisableRomHack.Location = new System.Drawing.Point(7, 420);
            this.buttonEnableDisableRomHack.Name = "buttonEnableDisableRomHack";
            this.buttonEnableDisableRomHack.Size = new System.Drawing.Size(188, 38);
            this.buttonEnableDisableRomHack.TabIndex = 4;
            this.buttonEnableDisableRomHack.Text = "Enable ROM Hack";
            this.buttonEnableDisableRomHack.UseVisualStyleBackColor = true;
            // 
            // buttonVarHackClearVariablesInMemory
            // 
            this.buttonVarHackClearVariablesInMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonVarHackClearVariablesInMemory.Location = new System.Drawing.Point(7, 376);
            this.buttonVarHackClearVariablesInMemory.Name = "buttonVarHackClearVariablesInMemory";
            this.buttonVarHackClearVariablesInMemory.Size = new System.Drawing.Size(188, 38);
            this.buttonVarHackClearVariablesInMemory.TabIndex = 4;
            this.buttonVarHackClearVariablesInMemory.Text = "Clear Variables in Memory";
            this.buttonVarHackClearVariablesInMemory.UseVisualStyleBackColor = true;
            // 
            // textBoxYDeltaValue
            // 
            this.textBoxYDeltaValue.Location = new System.Drawing.Point(31, 244);
            this.textBoxYDeltaValue.Name = "textBoxYDeltaValue";
            this.textBoxYDeltaValue.Size = new System.Drawing.Size(52, 20);
            this.textBoxYDeltaValue.TabIndex = 32;
            this.textBoxYDeltaValue.Text = "17";
            this.textBoxYDeltaValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxYPosValue
            // 
            this.textBoxYPosValue.Location = new System.Drawing.Point(31, 219);
            this.textBoxYPosValue.Name = "textBoxYPosValue";
            this.textBoxYPosValue.Size = new System.Drawing.Size(52, 20);
            this.textBoxYPosValue.TabIndex = 33;
            this.textBoxYPosValue.Text = "192";
            this.textBoxYPosValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxYDeltaChange
            // 
            this.textBoxYDeltaChange.Location = new System.Drawing.Point(132, 244);
            this.textBoxYDeltaChange.Name = "textBoxYDeltaChange";
            this.textBoxYDeltaChange.Size = new System.Drawing.Size(31, 20);
            this.textBoxYDeltaChange.TabIndex = 34;
            this.textBoxYDeltaChange.Text = "1";
            this.textBoxYDeltaChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxYPosChange
            // 
            this.textBoxYPosChange.Location = new System.Drawing.Point(132, 219);
            this.textBoxYPosChange.Name = "textBoxYPosChange";
            this.textBoxYPosChange.Size = new System.Drawing.Size(31, 20);
            this.textBoxYPosChange.TabIndex = 34;
            this.textBoxYPosChange.Text = "5";
            this.textBoxYPosChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxXPosChange
            // 
            this.textBoxXPosChange.Location = new System.Drawing.Point(132, 193);
            this.textBoxXPosChange.Name = "textBoxXPosChange";
            this.textBoxXPosChange.Size = new System.Drawing.Size(31, 20);
            this.textBoxXPosChange.TabIndex = 34;
            this.textBoxXPosChange.Text = "5";
            this.textBoxXPosChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxXPosValue
            // 
            this.textBoxXPosValue.Location = new System.Drawing.Point(31, 193);
            this.textBoxXPosValue.Name = "textBoxXPosValue";
            this.textBoxXPosValue.Size = new System.Drawing.Size(52, 20);
            this.textBoxXPosValue.TabIndex = 34;
            this.textBoxXPosValue.Text = "10";
            this.textBoxXPosValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelVarHackYDeltaLabel
            // 
            this.labelVarHackYDeltaLabel.AutoSize = true;
            this.labelVarHackYDeltaLabel.Location = new System.Drawing.Point(5, 247);
            this.labelVarHackYDeltaLabel.MinimumSize = new System.Drawing.Size(20, 2);
            this.labelVarHackYDeltaLabel.Name = "labelVarHackYDeltaLabel";
            this.labelVarHackYDeltaLabel.Size = new System.Drawing.Size(24, 13);
            this.labelVarHackYDeltaLabel.TabIndex = 29;
            this.labelVarHackYDeltaLabel.Text = "ΔY:";
            this.labelVarHackYDeltaLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelVarHackYPosLabel
            // 
            this.labelVarHackYPosLabel.AutoSize = true;
            this.labelVarHackYPosLabel.Location = new System.Drawing.Point(5, 222);
            this.labelVarHackYPosLabel.MinimumSize = new System.Drawing.Size(24, 2);
            this.labelVarHackYPosLabel.Name = "labelVarHackYPosLabel";
            this.labelVarHackYPosLabel.Size = new System.Drawing.Size(24, 13);
            this.labelVarHackYPosLabel.TabIndex = 30;
            this.labelVarHackYPosLabel.Text = "Y:";
            this.labelVarHackYPosLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelVarHackXPosLabel
            // 
            this.labelVarHackXPosLabel.AutoSize = true;
            this.labelVarHackXPosLabel.Location = new System.Drawing.Point(5, 196);
            this.labelVarHackXPosLabel.MinimumSize = new System.Drawing.Size(24, 2);
            this.labelVarHackXPosLabel.Name = "labelVarHackXPosLabel";
            this.labelVarHackXPosLabel.Size = new System.Drawing.Size(24, 13);
            this.labelVarHackXPosLabel.TabIndex = 31;
            this.labelVarHackXPosLabel.Text = "X:";
            this.labelVarHackXPosLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonYDeltaAdd
            // 
            this.buttonYDeltaAdd.Location = new System.Drawing.Point(165, 244);
            this.buttonYDeltaAdd.Name = "buttonYDeltaAdd";
            this.buttonYDeltaAdd.Size = new System.Drawing.Size(30, 20);
            this.buttonYDeltaAdd.TabIndex = 4;
            this.buttonYDeltaAdd.Text = "+";
            this.buttonYDeltaAdd.UseVisualStyleBackColor = true;
            // 
            // buttonYDeltaSubtract
            // 
            this.buttonYDeltaSubtract.Location = new System.Drawing.Point(100, 244);
            this.buttonYDeltaSubtract.Name = "buttonYDeltaSubtract";
            this.buttonYDeltaSubtract.Size = new System.Drawing.Size(30, 20);
            this.buttonYDeltaSubtract.TabIndex = 4;
            this.buttonYDeltaSubtract.Text = "-";
            this.buttonYDeltaSubtract.UseVisualStyleBackColor = true;
            // 
            // buttonYPosAdd
            // 
            this.buttonYPosAdd.Location = new System.Drawing.Point(165, 219);
            this.buttonYPosAdd.Name = "buttonYPosAdd";
            this.buttonYPosAdd.Size = new System.Drawing.Size(30, 20);
            this.buttonYPosAdd.TabIndex = 4;
            this.buttonYPosAdd.Text = "+";
            this.buttonYPosAdd.UseVisualStyleBackColor = true;
            // 
            // buttonYPosSubtract
            // 
            this.buttonYPosSubtract.Location = new System.Drawing.Point(100, 219);
            this.buttonYPosSubtract.Name = "buttonYPosSubtract";
            this.buttonYPosSubtract.Size = new System.Drawing.Size(30, 20);
            this.buttonYPosSubtract.TabIndex = 4;
            this.buttonYPosSubtract.Text = "-";
            this.buttonYPosSubtract.UseVisualStyleBackColor = true;
            // 
            // buttonXPosAdd
            // 
            this.buttonXPosAdd.Location = new System.Drawing.Point(165, 193);
            this.buttonXPosAdd.Name = "buttonXPosAdd";
            this.buttonXPosAdd.Size = new System.Drawing.Size(30, 20);
            this.buttonXPosAdd.TabIndex = 4;
            this.buttonXPosAdd.Text = "+";
            this.buttonXPosAdd.UseVisualStyleBackColor = true;
            // 
            // buttonXPosSubtract
            // 
            this.buttonXPosSubtract.Location = new System.Drawing.Point(100, 193);
            this.buttonXPosSubtract.Name = "buttonXPosSubtract";
            this.buttonXPosSubtract.Size = new System.Drawing.Size(30, 20);
            this.buttonXPosSubtract.TabIndex = 4;
            this.buttonXPosSubtract.Text = "-";
            this.buttonXPosSubtract.UseVisualStyleBackColor = true;
            // 
            // buttonSetPositionsAndApplyVariablesToMemory
            // 
            this.buttonSetPositionsAndApplyVariablesToMemory.Location = new System.Drawing.Point(7, 270);
            this.buttonSetPositionsAndApplyVariablesToMemory.Name = "buttonSetPositionsAndApplyVariablesToMemory";
            this.buttonSetPositionsAndApplyVariablesToMemory.Size = new System.Drawing.Size(188, 38);
            this.buttonSetPositionsAndApplyVariablesToMemory.TabIndex = 4;
            this.buttonSetPositionsAndApplyVariablesToMemory.Text = "Set Positions &&\r\nApply Variables to Memory";
            this.buttonSetPositionsAndApplyVariablesToMemory.UseVisualStyleBackColor = true;
            // 
            // buttonVarHackAddNewVariable
            // 
            this.buttonVarHackAddNewVariable.Location = new System.Drawing.Point(7, 8);
            this.buttonVarHackAddNewVariable.Name = "buttonVarHackAddNewVariable";
            this.buttonVarHackAddNewVariable.Size = new System.Drawing.Size(188, 38);
            this.buttonVarHackAddNewVariable.TabIndex = 4;
            this.buttonVarHackAddNewVariable.Text = "Add New Variable";
            this.buttonVarHackAddNewVariable.UseVisualStyleBackColor = true;
            // 
            // buttonVarHackShowVariableBytesInBigEndian
            // 
            this.buttonVarHackShowVariableBytesInBigEndian.Location = new System.Drawing.Point(7, 140);
            this.buttonVarHackShowVariableBytesInBigEndian.Name = "buttonVarHackShowVariableBytesInBigEndian";
            this.buttonVarHackShowVariableBytesInBigEndian.Size = new System.Drawing.Size(188, 38);
            this.buttonVarHackShowVariableBytesInBigEndian.TabIndex = 4;
            this.buttonVarHackShowVariableBytesInBigEndian.Text = "Show Variable Bytes in Big Endian\r\n(for ROM Memory)";
            this.buttonVarHackShowVariableBytesInBigEndian.UseVisualStyleBackColor = true;
            // 
            // buttonVarHackShowVariableBytesInLittleEndian
            // 
            this.buttonVarHackShowVariableBytesInLittleEndian.Location = new System.Drawing.Point(7, 96);
            this.buttonVarHackShowVariableBytesInLittleEndian.Name = "buttonVarHackShowVariableBytesInLittleEndian";
            this.buttonVarHackShowVariableBytesInLittleEndian.Size = new System.Drawing.Size(188, 38);
            this.buttonVarHackShowVariableBytesInLittleEndian.TabIndex = 4;
            this.buttonVarHackShowVariableBytesInLittleEndian.Text = "Show Variable Bytes in Little Endian\r\n(for Process Memory)";
            this.buttonVarHackShowVariableBytesInLittleEndian.UseVisualStyleBackColor = true;
            // 
            // buttonVarHackOpenVars
            // 
            this.buttonVarHackOpenVars.Location = new System.Drawing.Point(7, 52);
            this.buttonVarHackOpenVars.Name = "buttonVarHackOpenVars";
            this.buttonVarHackOpenVars.Size = new System.Drawing.Size(60, 38);
            this.buttonVarHackOpenVars.TabIndex = 4;
            this.buttonVarHackOpenVars.Text = "Open\r\nVars";
            this.buttonVarHackOpenVars.UseVisualStyleBackColor = true;
            // 
            // buttonVarHackSaveVars
            // 
            this.buttonVarHackSaveVars.Location = new System.Drawing.Point(71, 52);
            this.buttonVarHackSaveVars.Name = "buttonVarHackSaveVars";
            this.buttonVarHackSaveVars.Size = new System.Drawing.Size(60, 38);
            this.buttonVarHackSaveVars.TabIndex = 4;
            this.buttonVarHackSaveVars.Text = "Save\r\nVars";
            this.buttonVarHackSaveVars.UseVisualStyleBackColor = true;
            // 
            // buttonVarHackClearVars
            // 
            this.buttonVarHackClearVars.Location = new System.Drawing.Point(135, 52);
            this.buttonVarHackClearVars.Name = "buttonVarHackClearVars";
            this.buttonVarHackClearVars.Size = new System.Drawing.Size(60, 38);
            this.buttonVarHackClearVars.TabIndex = 4;
            this.buttonVarHackClearVars.Text = "Clear\r\nVars";
            this.buttonVarHackClearVars.UseVisualStyleBackColor = true;
            // 
            // varHackPanel
            // 
            this.varHackPanel.AutoScroll = true;
            this.varHackPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.varHackPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.varHackPanel.Location = new System.Drawing.Point(0, 0);
            this.varHackPanel.Margin = new System.Windows.Forms.Padding(0);
            this.varHackPanel.Name = "varHackPanel";
            this.varHackPanel.Padding = new System.Windows.Forms.Padding(2);
            this.varHackPanel.Size = new System.Drawing.Size(695, 461);
            this.varHackPanel.TabIndex = 3;
            // 
            // VarHackTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerVarHack);
            this.Name = "VarHackTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.splitContainerVarHack.Panel1.ResumeLayout(false);
            this.splitContainerVarHack.Panel1.PerformLayout();
            this.splitContainerVarHack.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVarHack)).EndInit();
            this.splitContainerVarHack.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BetterSplitContainer splitContainerVarHack;
        private System.Windows.Forms.Button buttonVarHackApplyVariablesToMemory;
        private BinaryButton buttonEnableDisableRomHack;
        private System.Windows.Forms.Button buttonVarHackClearVariablesInMemory;
        private BetterTextbox textBoxYDeltaValue;
        private BetterTextbox textBoxYPosValue;
        private BetterTextbox textBoxYDeltaChange;
        private BetterTextbox textBoxYPosChange;
        private BetterTextbox textBoxXPosChange;
        private BetterTextbox textBoxXPosValue;
        private System.Windows.Forms.Label labelVarHackYDeltaLabel;
        private System.Windows.Forms.Label labelVarHackYPosLabel;
        private System.Windows.Forms.Label labelVarHackXPosLabel;
        private System.Windows.Forms.Button buttonYDeltaAdd;
        private System.Windows.Forms.Button buttonYDeltaSubtract;
        private System.Windows.Forms.Button buttonYPosAdd;
        private System.Windows.Forms.Button buttonYPosSubtract;
        private System.Windows.Forms.Button buttonXPosAdd;
        private System.Windows.Forms.Button buttonXPosSubtract;
        private System.Windows.Forms.Button buttonSetPositionsAndApplyVariablesToMemory;
        private System.Windows.Forms.Button buttonVarHackAddNewVariable;
        private System.Windows.Forms.Button buttonVarHackShowVariableBytesInBigEndian;
        private System.Windows.Forms.Button buttonVarHackShowVariableBytesInLittleEndian;
        private System.Windows.Forms.Button buttonVarHackOpenVars;
        private System.Windows.Forms.Button buttonVarHackSaveVars;
        private System.Windows.Forms.Button buttonVarHackClearVars;
        private Controls.VarHackFlowLayoutPanel varHackPanel;
    }
}
