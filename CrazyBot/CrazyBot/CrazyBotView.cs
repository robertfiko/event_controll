using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrazyBot
{
    public partial class CrazyBotView : Form
    {

        private GridButton[,] buttons;
        private CrazyBotModel model;
        private Image NoWallImage;
        public CrazyBotView()
        {
            InitializeComponent();
            seven.Click += onSevenClicked;
            eleven.Click += onElevenClicked;

            fifteen.Click += onFifteenClicked;

            buttons = null;

            model = new CrazyBotModel();
            model.refreshBoard += drawBoard;
            model.refreshField += refreshField;

            NoWallImage = Image.FromFile("stone.png");

        }

        public void onSevenClicked(object obj, EventArgs e)
        {
            model.newGame(7);
        }

        public void onElevenClicked(object obj, EventArgs e)
        {
            model.newGame(11);
        }

        public void onFifteenClicked(object obj, EventArgs e)
        {
            model.newGame(15);
        }

        public void gridButtonClicked(object obj, EventArgs e)
        {
            model.invertWall((obj as GridButton).X, (obj as GridButton).Y);
        }
        
        public void refreshField(object obj, FieldRefreshEventArgs e)
        {
             boardGrid.GetControlFromPosition(e.Y,e.X).Text = model.getBoard()[e.X, e.Y].ToString(); 
        }

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
                    buttons[i, j] = new GridButton(i,j);
                    buttons[i, j].Text = model.getBoard()[i,j].ToString();
                    if (model.getBoard()[i, j] == FieldType.NO_WALL) buttons[i, j].Image = NoWallImage;
                    buttons[i, j].Dock = DockStyle.Fill;
                    //labels[i, j].Size = new System.Drawing.Size(100, 100);
                    buttons[i, j].AutoSize = true;
                    boardGrid.Controls.Add(buttons[i, j], j, i);

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
    }
}
