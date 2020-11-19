using System;
using System.Drawing;
 using System.Windows.Forms;
using System.Timers;
using System.Collections.Generic;

using CrazyBot.Model;
using CrazyBot.Persistance;
using System.Threading.Tasks;

namespace CrazyBot.View
{
    public partial class CrazyBotView : Form
    {

        #region Fields
        private GridButton[,] buttons;
        private CrazyBotModel model;

        private Image noWallTexture;
        private Image WallTexture;
        private Image cannotWallTexture;

        private Image robotTexture;
        private Image magnetTexture;


        #endregion

        public CrazyBotView()
        {
            InitializeComponent();
            //Subscribe to new game event handling
            seven.Click += onSevenClicked;
            eleven.Click += onElevenClicked;
            fifteen.Click += onFifteenClicked;

            play.Click += onPlay;
            pause.Click += onPause;
            save.Click += onSave;
            load.Click += onLoad;

            buttons = null;

            //Generating model and subscribing to its events
            model = new CrazyBotModel();
            model.refreshBoard += new EventHandler<EventArgs>(drawBoard);
            model.refreshField += new EventHandler<FieldRefreshEventArgs>(refreshField);
            model.refreshTime += new EventHandler<EventArgs>(TimeElapsed);
            model.OnGameOver += new EventHandler<EventArgs>(gameOver);

            noWallTexture = Resource.no_wall;
            robotTexture = Resource.robot;
            cannotWallTexture = Resource.cannot_wall;
            WallTexture = Resource.wall;
            magnetTexture = Resource.Magnet;

            play.Enabled = false;
            pause.Enabled = false;
            save.Enabled = false;


            boardGrid.Visible = false;
            this.BackgroundImage = Resource.robochase;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.CenterToScreen();



        }

        #region New Game Event Handlers & New Game Method & Button Styler
        public void onSevenClicked(object obj, EventArgs e)
        {
            model.newGame(7);
            newGame();
        }

        public void onElevenClicked(object obj, EventArgs e)
        {
            model.newGame(11);
            newGame();
        }

        public void onFifteenClicked(object obj, EventArgs e)
        {
            model.newGame(15);
            newGame();
        }

        public void onPlay(object obj, EventArgs e)
        {
            foreach (var btn in buttons)
            {
                btn.Enabled = true;
            }
            model.play();
            play.Enabled = false;
            pause.Enabled = true;
            save.Enabled = false;
            load.Enabled = false;
        }

        public void onPause(object obj, EventArgs e)
        {
            model.pause();
            foreach (var btn in buttons)
            {
                btn.Enabled = false;
            }
            play.Enabled = true;
            pause.Enabled = false;
            load.Enabled = true;
            save.Enabled = true;
            setStatusBarPaused();
            MessageBox.Show("Game is paused!\nTo continue, use the Game Menu or press Ctrl + Alt + Space!");


            
        }

        public void onSave(object obj, EventArgs e)
        {
            
            play.Enabled = true;
            pause.Enabled = false;


            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Crazy files (*.crazy)|*.crazy";
            dialog.DefaultExt = "crazy*";
            dialog.AddExtension = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    model.saveGame(dialog.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot save game!" + Environment.NewLine + "Wrong path or have no write right!.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Successfull save!");
            }



        }

        public void onLoad(object obj, EventArgs e)
        {
            string filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "CRAZY files (*.crazy)|*.crazy";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                model.loadFromFile(filePath);
                boardGrid.Visible = true;

                pause.Enabled = false;
                play.Enabled = true;
                save.Enabled = false;
                load.Enabled = false;

                statusBar.Text = "Game has loaded. Press play to continue";
            }
            

            

        }

        private void newGame()
        {
            TimeElapsed(this, new EventArgs());
            boardGrid.Visible = true;

            pause.Enabled = true;
            play.Enabled = false;
            save.Enabled = false;
            load.Enabled = false;
        }

        private GridButton GridButtonStyler(GridButton btn)
        {
            int x = btn.X;
            int y = btn.Y;

            buttons[x, y].Text = "";
            if (model.getBoard()[x, y] == FieldType.NO_WALL)        buttons[x, y].BackgroundImage = noWallTexture;
            else if (model.getBoard()[x, y] == FieldType.WALL)           buttons[x, y].BackgroundImage = WallTexture;
            else if (model.getBoard()[x, y] == FieldType.CANNOT_WALL)    buttons[x, y].BackgroundImage = cannotWallTexture;
            else if (model.getBoard()[x, y] == FieldType.MAGNET)
            {
                Image imageBackground = noWallTexture;
                int size = Convert.ToInt32(imageBackground.Width * 0.8);
                Image imageOverlay = new Bitmap(magnetTexture, new Size(size, size));

                Image img = new Bitmap(imageBackground.Width, imageBackground.Height);
                using (Graphics gr = Graphics.FromImage(img))
                {
                    gr.DrawImage(imageBackground, new Point(0, 0));
                    gr.DrawImage(imageOverlay, new Point(Convert.ToInt32(imageBackground.Size.Width * 0.1), Convert.ToInt32(imageBackground.Size.Height * 0.1)));
                }

                btn.BackgroundImage = img;
            }


            if (model.getRobotPos().Equals(new Position(x,y)))
            {
                Image imageBackground = (model.getFieldTypeOnRobot() == FieldType.NO_WALL) ? noWallTexture : cannotWallTexture;
                int size = Convert.ToInt32(imageBackground.Width * 0.8);
                Image imageOverlay = new Bitmap(robotTexture, new Size(size, size));
                
                Image img = new Bitmap(imageBackground.Width, imageBackground.Height);
                using (Graphics gr = Graphics.FromImage(img))
                {  
                    gr.DrawImage(imageBackground, new Point(0, 0));
                    gr.DrawImage(imageOverlay, new Point(Convert.ToInt32(imageBackground.Size.Width*0.1), Convert.ToInt32(imageBackground.Size.Height*0.1)));
                }

                btn.BackgroundImage = img;
            }


            btn.BackgroundImageLayout = ImageLayout.Stretch;

            return btn;



        }



        #endregion

        #region Field and Board operations :: draw, updateField, refreshField etc.
        public void drawBoard(object obj, EventArgs e)
        {

            if (buttons != null)
            {
                foreach (Button button in buttons)
                    boardGrid.Controls.Remove(button);
            }
            buttons = new GridButton[model.getSize(), model.getSize()];

            boardGrid.RowCount = model.getSize(); ;
            boardGrid.ColumnCount = model.getSize();
            for (int i = 0; i < model.getSize(); i++)
            {
                
                for (int j = 0; j < model.getSize(); j++)
                {
                    buttons[i, j] = new GridButton(i, j);
                    buttons[i, j].Text = model.getBoard()[i, j].ToString();
                    GridButtonStyler(buttons[i, j]);

                    buttons[i, j].Dock = DockStyle.Fill;
                    buttons[i, j].AutoSize = true;
                    boardGrid.Controls.Add(buttons[i, j], i, j);

                    buttons[i, j].Click += gridButtonClicked;
                }
            }
            boardGrid.RowStyles.Clear();
            boardGrid.ColumnStyles.Clear();
            for (int i = 0; i < model.getSize(); i++)
            {
                boardGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 1 / 15f));
            }
            for (int i = 0; i < model.getSize(); i++)
            {
                boardGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1 / 15f));
            }
            boardGrid.Dock = DockStyle.Fill;



        }

        private void gameOver(object obj, EventArgs e)
        {
            void update()
            {
                foreach (var btn in buttons)
                {
                    btn.Enabled = false;
                }
                boardGrid.Visible = false;
                MessageBox.Show("Congrats! You win this game in: " + TimeSpan.FromSeconds(model.getTime()).ToString("mm':'ss"));
                statusBar.Text = "No game is in progress";

                pause.Enabled = false;
                play.Enabled = false;
                save.Enabled = false;
                load.Enabled = true;
            }

            if (boardGrid.InvokeRequired)
            {
                boardGrid.Invoke(new MethodInvoker(delegate
                {
                    update();
                }));
            }
            else
            {
                update();
            }
        }

        private void updateFieldSafely(Position p)
        {
            void update()
            {
                buttons[p.X, p.Y].Text = model.getBoard()[p.X, p.Y].ToString();
                GridButtonStyler(buttons[p.X, p.Y]);
            }

            if (boardGrid.InvokeRequired)
            {
                boardGrid.Invoke(new MethodInvoker(delegate
                {
                    update();
                }));
            }
            else
            {
                update();
            }
        }
        public void refreshField(object obj, FieldRefreshEventArgs e)
        {
            updateFieldSafely(e.Position);
        }

        #endregion

        #region Game action handler :: gridButtonClicked, timeElapsed, statusBar updated
        public void gridButtonClicked(object obj, EventArgs e)
        {
            model.invertWall((obj as GridButton).getPosition());
        }
        private void TimeElapsed(object obj, EventArgs e)
        {
            
            void update()
            {
                statusBar.Text = "Time passed: " + TimeSpan.FromSeconds(model.getTime()).ToString("mm':'ss");
            }
            
            if (boardGrid.InvokeRequired)
            {
                boardGrid.Invoke(new MethodInvoker(delegate
                {
                    update();
                }));
            }
            else
            {
                update();
            }
        }
        private void setStatusBarPaused()
        {
            void update()
            {
                statusBar.Text += " | Game is PAUSED";
            }

            if (boardGrid.InvokeRequired)
            {
                boardGrid.Invoke(new MethodInvoker(delegate
                {
                    update();
                }));
            }
            else
            {
                update();
            }
        }



        #endregion


    }
}
