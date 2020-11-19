namespace CrazyBot.View
{
    partial class CrazyBotView
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
            this.components = new System.ComponentModel.Container();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.boardGrid = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newGameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.seven = new System.Windows.Forms.ToolStripMenuItem();
            this.eleven = new System.Windows.Forms.ToolStripMenuItem();
            this.fifteen = new System.Windows.Forms.ToolStripMenuItem();
            this.gameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pause = new System.Windows.Forms.ToolStripMenuItem();
            this.play = new System.Windows.Forms.ToolStripMenuItem();
            this.load = new System.Windows.Forms.ToolStripMenuItem();
            this.save = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // boardGrid
            // 
            this.boardGrid.ColumnCount = 2;
            this.boardGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.boardGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.boardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardGrid.Location = new System.Drawing.Point(0, 40);
            this.boardGrid.Margin = new System.Windows.Forms.Padding(3, 3, 3, 100);
            this.boardGrid.Name = "boardGrid";
            this.boardGrid.Padding = new System.Windows.Forms.Padding(0, 0, 0, 60);
            this.boardGrid.RowCount = 2;
            this.boardGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.boardGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.boardGrid.Size = new System.Drawing.Size(1551, 946);
            this.boardGrid.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameMenu,
            this.gameMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1551, 40);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newGameMenu
            // 
            this.newGameMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seven,
            this.eleven,
            this.fifteen});
            this.newGameMenu.Name = "newGameMenu";
            this.newGameMenu.Size = new System.Drawing.Size(149, 36);
            this.newGameMenu.Text = "New game";
            // 
            // seven
            // 
            this.seven.Name = "seven";
            this.seven.Size = new System.Drawing.Size(279, 44);
            this.seven.Text = "Size: 7 x 7";
            // 
            // eleven
            // 
            this.eleven.Name = "eleven";
            this.eleven.Size = new System.Drawing.Size(279, 44);
            this.eleven.Text = "Size: 11 x 11";
            // 
            // fifteen
            // 
            this.fifteen.Name = "fifteen";
            this.fifteen.Size = new System.Drawing.Size(279, 44);
            this.fifteen.Text = "Size: 15 x 15";
            // 
            // gameMenu
            // 
            this.gameMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pause,
            this.play,
            this.load,
            this.save});
            this.gameMenu.Name = "gameMenu";
            this.gameMenu.ShowShortcutKeys = false;
            this.gameMenu.Size = new System.Drawing.Size(166, 36);
            this.gameMenu.Text = "Game Menu";
            // 
            // pause
            // 
            this.pause.Name = "pause";
            this.pause.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.pause.Size = new System.Drawing.Size(365, 44);
            this.pause.Text = "Pause";
            // 
            // play
            // 
            this.play.Name = "play";
            this.play.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Space)));
            this.play.Size = new System.Drawing.Size(365, 44);
            this.play.Text = "Play";
            // 
            // load
            // 
            this.load.Name = "load";
            this.load.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.load.Size = new System.Drawing.Size(365, 44);
            this.load.Text = "Load";
            // 
            // save
            // 
            this.save.Name = "save";
            this.save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.save.Size = new System.Drawing.Size(365, 44);
            this.save.Text = "Save";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 944);
            this.statusStrip1.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1551, 42);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "No game is in progress";
            // 
            // statusBar
            // 
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(261, 32);
            this.statusBar.Text = "No game is in progress";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(198, 44);
            this.toolStripMenuItem1.Text = "7 x 7";
            // 
            // CrazyBotView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1551, 986);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.boardGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CrazyBotView";
            this.Text = "RoboChase";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TableLayoutPanel boardGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newGameMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem seven;
        private System.Windows.Forms.ToolStripMenuItem eleven;
        private System.Windows.Forms.ToolStripMenuItem fifteen;
        private System.Windows.Forms.ToolStripMenuItem gameMenu;
        private System.Windows.Forms.ToolStripMenuItem pause;
        private System.Windows.Forms.ToolStripMenuItem play;
        private System.Windows.Forms.ToolStripMenuItem load;
        private System.Windows.Forms.ToolStripMenuItem save;
    }
}

