namespace STROOP.Tabs
{
    partial class SoundTab
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
            this.splitContainerSound = new STROOP.BetterSplitContainer();
            this.splitContainerSoundMusic = new STROOP.BetterSplitContainer();
            this.listBoxSoundMusic = new System.Windows.Forms.ListBox();
            this.textBoxSoundMusic = new STROOP.BetterTextbox();
            this.buttonSoundPlayMusic = new System.Windows.Forms.Button();
            this.splitContainerSoundSoundEffect = new STROOP.BetterSplitContainer();
            this.listBoxSoundSoundEffect = new System.Windows.Forms.ListBox();
            this.textBoxSoundSoundEffect = new STROOP.BetterTextbox();
            this.buttonSoundPlaySoundEffect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSound)).BeginInit();
            this.splitContainerSound.Panel1.SuspendLayout();
            this.splitContainerSound.Panel2.SuspendLayout();
            this.splitContainerSound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSoundMusic)).BeginInit();
            this.splitContainerSoundMusic.Panel1.SuspendLayout();
            this.splitContainerSoundMusic.Panel2.SuspendLayout();
            this.splitContainerSoundMusic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSoundSoundEffect)).BeginInit();
            this.splitContainerSoundSoundEffect.Panel1.SuspendLayout();
            this.splitContainerSoundSoundEffect.Panel2.SuspendLayout();
            this.splitContainerSoundSoundEffect.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerSound
            // 
            this.splitContainerSound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerSound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSound.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerSound.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSound.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerSound.Name = "splitContainerSound";
            // 
            // splitContainerSound.Panel1
            // 
            this.splitContainerSound.Panel1.AutoScroll = true;
            this.splitContainerSound.Panel1.Controls.Add(this.splitContainerSoundMusic);
            this.splitContainerSound.Panel1MinSize = 0;
            // 
            // splitContainerSound.Panel2
            // 
            this.splitContainerSound.Panel2.Controls.Add(this.splitContainerSoundSoundEffect);
            this.splitContainerSound.Panel2MinSize = 0;
            this.splitContainerSound.Size = new System.Drawing.Size(915, 463);
            this.splitContainerSound.SplitterDistance = 422;
            this.splitContainerSound.SplitterWidth = 1;
            this.splitContainerSound.TabIndex = 36;
            // 
            // splitContainerSoundMusic
            // 
            this.splitContainerSoundMusic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerSoundMusic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSoundMusic.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerSoundMusic.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSoundMusic.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerSoundMusic.Name = "splitContainerSoundMusic";
            this.splitContainerSoundMusic.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerSoundMusic.Panel1
            // 
            this.splitContainerSoundMusic.Panel1.AutoScroll = true;
            this.splitContainerSoundMusic.Panel1.Controls.Add(this.listBoxSoundMusic);
            this.splitContainerSoundMusic.Panel1MinSize = 0;
            // 
            // splitContainerSoundMusic.Panel2
            // 
            this.splitContainerSoundMusic.Panel2.Controls.Add(this.textBoxSoundMusic);
            this.splitContainerSoundMusic.Panel2.Controls.Add(this.buttonSoundPlayMusic);
            this.splitContainerSoundMusic.Panel2MinSize = 0;
            this.splitContainerSoundMusic.Size = new System.Drawing.Size(422, 463);
            this.splitContainerSoundMusic.SplitterDistance = 437;
            this.splitContainerSoundMusic.SplitterWidth = 1;
            this.splitContainerSoundMusic.TabIndex = 35;
            // 
            // listBoxSoundMusic
            // 
            this.listBoxSoundMusic.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxSoundMusic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSoundMusic.FormattingEnabled = true;
            this.listBoxSoundMusic.Location = new System.Drawing.Point(0, 0);
            this.listBoxSoundMusic.Name = "listBoxSoundMusic";
            this.listBoxSoundMusic.Size = new System.Drawing.Size(420, 435);
            this.listBoxSoundMusic.TabIndex = 19;
            // 
            // textBoxSoundMusic
            // 
            this.textBoxSoundMusic.Location = new System.Drawing.Point(6, 7);
            this.textBoxSoundMusic.Name = "textBoxSoundMusic";
            this.textBoxSoundMusic.Size = new System.Drawing.Size(102, 20);
            this.textBoxSoundMusic.TabIndex = 33;
            this.textBoxSoundMusic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonSoundPlayMusic
            // 
            this.buttonSoundPlayMusic.Location = new System.Drawing.Point(113, 1);
            this.buttonSoundPlayMusic.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSoundPlayMusic.Name = "buttonSoundPlayMusic";
            this.buttonSoundPlayMusic.Size = new System.Drawing.Size(136, 30);
            this.buttonSoundPlayMusic.TabIndex = 18;
            this.buttonSoundPlayMusic.Text = "Play Music";
            this.buttonSoundPlayMusic.UseVisualStyleBackColor = true;
            // 
            // splitContainerSoundSoundEffect
            // 
            this.splitContainerSoundSoundEffect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerSoundSoundEffect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSoundSoundEffect.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerSoundSoundEffect.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSoundSoundEffect.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerSoundSoundEffect.Name = "splitContainerSoundSoundEffect";
            this.splitContainerSoundSoundEffect.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerSoundSoundEffect.Panel1
            // 
            this.splitContainerSoundSoundEffect.Panel1.AutoScroll = true;
            this.splitContainerSoundSoundEffect.Panel1.Controls.Add(this.listBoxSoundSoundEffect);
            this.splitContainerSoundSoundEffect.Panel1MinSize = 0;
            // 
            // splitContainerSoundSoundEffect.Panel2
            // 
            this.splitContainerSoundSoundEffect.Panel2.Controls.Add(this.textBoxSoundSoundEffect);
            this.splitContainerSoundSoundEffect.Panel2.Controls.Add(this.buttonSoundPlaySoundEffect);
            this.splitContainerSoundSoundEffect.Panel2MinSize = 0;
            this.splitContainerSoundSoundEffect.Size = new System.Drawing.Size(492, 463);
            this.splitContainerSoundSoundEffect.SplitterDistance = 437;
            this.splitContainerSoundSoundEffect.SplitterWidth = 1;
            this.splitContainerSoundSoundEffect.TabIndex = 35;
            // 
            // listBoxSoundSoundEffect
            // 
            this.listBoxSoundSoundEffect.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxSoundSoundEffect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSoundSoundEffect.FormattingEnabled = true;
            this.listBoxSoundSoundEffect.Location = new System.Drawing.Point(0, 0);
            this.listBoxSoundSoundEffect.Name = "listBoxSoundSoundEffect";
            this.listBoxSoundSoundEffect.Size = new System.Drawing.Size(490, 435);
            this.listBoxSoundSoundEffect.TabIndex = 19;
            // 
            // textBoxSoundSoundEffect
            // 
            this.textBoxSoundSoundEffect.Location = new System.Drawing.Point(6, 7);
            this.textBoxSoundSoundEffect.Name = "textBoxSoundSoundEffect";
            this.textBoxSoundSoundEffect.Size = new System.Drawing.Size(102, 20);
            this.textBoxSoundSoundEffect.TabIndex = 33;
            this.textBoxSoundSoundEffect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonSoundPlaySoundEffect
            // 
            this.buttonSoundPlaySoundEffect.Location = new System.Drawing.Point(113, 1);
            this.buttonSoundPlaySoundEffect.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSoundPlaySoundEffect.Name = "buttonSoundPlaySoundEffect";
            this.buttonSoundPlaySoundEffect.Size = new System.Drawing.Size(136, 30);
            this.buttonSoundPlaySoundEffect.TabIndex = 18;
            this.buttonSoundPlaySoundEffect.Text = "Play Sound Effect";
            this.buttonSoundPlaySoundEffect.UseVisualStyleBackColor = true;
            // 
            // SoundTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerSound);
            this.Name = "SoundTab";
            this.Size = new System.Drawing.Size(915, 463);
            this.splitContainerSound.Panel1.ResumeLayout(false);
            this.splitContainerSound.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSound)).EndInit();
            this.splitContainerSound.ResumeLayout(false);
            this.splitContainerSoundMusic.Panel1.ResumeLayout(false);
            this.splitContainerSoundMusic.Panel2.ResumeLayout(false);
            this.splitContainerSoundMusic.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSoundMusic)).EndInit();
            this.splitContainerSoundMusic.ResumeLayout(false);
            this.splitContainerSoundSoundEffect.Panel1.ResumeLayout(false);
            this.splitContainerSoundSoundEffect.Panel2.ResumeLayout(false);
            this.splitContainerSoundSoundEffect.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSoundSoundEffect)).EndInit();
            this.splitContainerSoundSoundEffect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BetterSplitContainer splitContainerSound;
        private BetterSplitContainer splitContainerSoundMusic;
        private System.Windows.Forms.ListBox listBoxSoundMusic;
        private BetterTextbox textBoxSoundMusic;
        private System.Windows.Forms.Button buttonSoundPlayMusic;
        private BetterSplitContainer splitContainerSoundSoundEffect;
        private System.Windows.Forms.ListBox listBoxSoundSoundEffect;
        private BetterTextbox textBoxSoundSoundEffect;
        private System.Windows.Forms.Button buttonSoundPlaySoundEffect;
    }
}
