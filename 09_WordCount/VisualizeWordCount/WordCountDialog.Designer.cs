namespace VisualizeWordCount
{
    partial class WordCountDialog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countWordsFileDiaog = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox = new System.Windows.Forms.TextBox();
            this.listBoxCounter = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1010, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileDialogMenuItem,
            this.countWordsFileDiaog});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(71, 36);
            this.fileMenu.Text = "File";
            // 
            // openFileDialogMenuItem
            // 
            this.openFileDialogMenuItem.Name = "openFileDialogMenuItem";
            this.openFileDialogMenuItem.Size = new System.Drawing.Size(319, 44);
            this.openFileDialogMenuItem.Text = "Open file dialog";
            // 
            // countWordsFileDiaog
            // 
            this.countWordsFileDiaog.Name = "countWordsFileDiaog";
            this.countWordsFileDiaog.Size = new System.Drawing.Size(319, 44);
            this.countWordsFileDiaog.Text = "Count words";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(24, 60);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(496, 750);
            this.textBox.TabIndex = 2;
            // 
            // listBoxCounter
            // 
            this.listBoxCounter.FormattingEnabled = true;
            this.listBoxCounter.ItemHeight = 32;
            this.listBoxCounter.Location = new System.Drawing.Point(540, 60);
            this.listBoxCounter.Name = "listBoxCounter";
            this.listBoxCounter.Size = new System.Drawing.Size(448, 740);
            this.listBoxCounter.TabIndex = 3;
            // 
            // WordCountDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 870);
            this.Controls.Add(this.listBoxCounter);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "WordCountDialog";
            this.Text = "Word counter!";
            this.Load += new System.EventHandler(this.WordCountDialog_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openFileDialogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem countWordsFileDiaog;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ListBox listBoxCounter;
    }
}

