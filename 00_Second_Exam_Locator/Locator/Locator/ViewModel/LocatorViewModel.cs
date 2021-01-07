using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using Locator.Model;

namespace Locator.ViewModel
{
    public class LocatorViewModel : ViewModelBase
    {


        private LocatorModel _model;
        public DelegateCommand NewGame { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand SwitchMode { get; private set; }
        public ObservableCollection<Field> Fields { get; set; }
        public int BoardSize { get { return (_model.isInGame()) ? _model.getSize() : 0; } }
        private String _currentGameSatus;

        public String CurrentGameStatus { 
            get { return _currentGameSatus; }
            set { if (value != _currentGameSatus) {
                    _currentGameSatus = value;
                    OnPropertyChanged();
                }
            }}

        public event EventHandler ExitGame;

        public LocatorViewModel(LocatorModel model)
        {
            _model = model;
            Fields = new ObservableCollection<Field>(); 

            _model.refreshBoard += new EventHandler<EventArgs>(Model_DrawBoard);
            _model.refreshField += new EventHandler<FieldRefreshEventArgs>(Model_FieldRefresh);
            _model.OnGameOver += new EventHandler<int>(Model_GameOver);
            _model.StatusStrip += new EventHandler<String>(Model_StatusStrip);

            NewGame = new DelegateCommand(param => OnNewGame(Int32.Parse(param.ToString())));
            ExitCommand = new DelegateCommand(param => OnExitGame());
            SwitchMode = new DelegateCommand(param => OnSwitchMode());

            CurrentGameStatus = "Bomb mode";
            drawBoard();
        }

        private void drawBoard()
        {
            Fields.Clear();
            for (Int32 i = 0; i < _model.getSize(); i++)
                for (Int32 j = 0; j < _model.getSize(); j++)
                {
                    Field toAdd = new Field
                    {
                        Text = (_model.getBoard()[i, j]).ToString(),
                        X = i,
                        Y = j,
                        Number = i * _model.getSize() + j,
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                    };

                    GridButtonStyler(toAdd);
                    Fields.Add(toAdd);
                }
        }

        private void GridButtonStyler(Field btn)
        {
            int x = btn.X;
            int y = btn.Y;

            if (_model.getBoard()[btn.X,btn.Y] == 0) btn.Text = "";
            else if (_model.getBoard()[btn.X, btn.Y] == 1) btn.Text = "";
            else if (_model.getBoard()[btn.X, btn.Y] == 2) btn.Text = "";
            else if (_model.getBoard()[btn.X, btn.Y] == 3) btn.Text = "O";
            else if (_model.getBoard()[btn.X, btn.Y] == 4) btn.Text = "X";





        }
        private void StepGame(Int32 index)
        {
            Field field = Fields[index];
            _model.Step(field.Position);
        }

        private void Model_GameOver(object sender, int e)
        {
            MessageBox.Show("Congrat, You won! Points:  " + e);
        }

        private void Model_FieldRefresh(object sender, FieldRefreshEventArgs e)
        {
            GridButtonStyler(Fields[e.Position.X * _model.getSize() + e.Position.Y]);
            OnPropertyChanged();
        }
        private void Model_DrawBoard(object sender, EventArgs e)
        {
            drawBoard();

        }
         private void Model_StatusStrip(object sender, String s)
        {
            CurrentGameStatus = s;
        }

        private void OnNewGame(int size)
        {
            _model.newGame(size);
            drawBoard();
            OnPropertyChanged("BoardSize");
        }

        private void OnSwitchMode()
        {
            _model.SwitchMode();
        }

        private void OnExitGame()
        {
            ExitGame?.Invoke(this, EventArgs.Empty);
        }
    }
}

