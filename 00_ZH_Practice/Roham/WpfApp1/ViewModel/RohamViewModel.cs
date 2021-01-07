using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using Roham.Model;
using Roham.Persistance;

namespace Roham.ViewModel
{
    public class RohamViewModel : ViewModelBase
    {

        #region Fields

        private Roham
            Model _model;

        #endregion

        #region Properties

        public DelegateCommand NewSmallGameCommand { get; private set; }
        public DelegateCommand NewMediumGameCommand { get; private set; }
        public DelegateCommand NewLargeGameCommand { get; private set; }

        public DelegateCommand PauseCommand { get; private set; }
        public DelegateCommand PlayCommand { get; private set; }



        public DelegateCommand LoadGameCommand { get; private set; }
        public DelegateCommand SaveGameCommand { get; private set; }

        public DelegateCommand ExitCommand { get; private set; }
        public ObservableCollection<Field> Fields { get; set; }



        public String GameTime { get {
                if (_model.isInGame())
                    return TimeSpan.FromSeconds(_model.getTime()).ToString("g");
                else
                    return TimeSpan.FromSeconds(0).ToString("g");
            } }

        public int BoardSize { get { return (_model.isInGame()) ? _model.getSize() : 0; } }

        private bool _pauseEnabled;
        private bool _playEnabled;
        private bool _loadEnabled;
        private bool _saveEnabled;
        private String _currentGameSatus;

        public String PauseEnabled { get { return _pauseEnabled ? "True" : "False"; } set { _pauseEnabled = (value == "True"); OnPropertyChanged(); } }
        public String PlayEnabled { get { return _playEnabled ? "True" : "False"; } set { _playEnabled = (value == "True"); OnPropertyChanged(); } }
        public String LoadEnabled { get { return _loadEnabled ? "True" : "False"; } set { _loadEnabled = (value == "True"); OnPropertyChanged(); } }
        public String SaveEnabled { get { return _saveEnabled ? "True" : "False"; } set { _saveEnabled = (value == "True"); OnPropertyChanged(); } }
        public String CurrentGameStatus { 
            get { return _currentGameSatus; }
            set { if (value != _currentGameSatus) {
                    _currentGameSatus = value;
                    OnPropertyChanged();
                }
            }}


        #endregion

        #region Events
        public event EventHandler LoadGame;
        public event EventHandler SaveGame;
        public event EventHandler ExitGame;
        #endregion

        #region Constructors
        public RohamViewModel(RoboChaseModel model)
        {
            _model = model;
            Fields = new ObservableCollection<Field>();

            _pauseEnabled = false;
            _pauseEnabled = false;
            _loadEnabled = true;
            _saveEnabled = false;

            _model.refreshBoard += new EventHandler<EventArgs>(Model_DrawBoard);
            _model.refreshField += new EventHandler<FieldRefreshEventArgs>(Model_FieldRefresh);
            _model.refreshTime += new EventHandler<EventArgs>(Model_TimeElapsed);
            _model.OnGameOver += new EventHandler<EventArgs>(Model_GameOver);

            NewSmallGameCommand = new DelegateCommand(param => OnNewGame(7));
            NewMediumGameCommand = new DelegateCommand(param => OnNewGame(11));
            NewLargeGameCommand = new DelegateCommand(param => OnNewGame(15));

            PauseCommand = new DelegateCommand(param => OnPause());
            PlayCommand = new DelegateCommand(param => OnPlay());

            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitCommand = new DelegateCommand(param => OnExitGame());


        }
        #endregion


        #region Private methods
        private void drawBoard()
        {
            Fields.Clear();
            for (Int32 i = 0; i < _model.getSize(); i++)
                for (Int32 j = 0; j < _model.getSize(); j++)
                {

                    Field toAdd = new Field
                    {
                        Text = (_model.getBoard()[i, j]).ToString() + ", " + i.ToString() + ", " + j.ToString(),
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

            btn.Text += " S";
            if (_model.getBoard()[x, y]      == FieldType.NO_WALL) btn.ImgSrc = "/View/stone.png";
            else if (_model.getBoard()[x, y] == FieldType.WALL) btn.ImgSrc = "/View/brick.png";
            else if (_model.getBoard()[x, y] == FieldType.CANNOT_WALL) btn.ImgSrc = "/View/cobble.png";
            else if (_model.getBoard()[x, y] == FieldType.MAGNET) btn.ImgSrc = "/View/magnet.png";
           
            if (_model.getRobotPos().Equals(new Position(x, y))) btn.ImgSrc = "/View/robot.png";
        }
        private void StepGame(Int32 index)
        {
            Field field = Fields[index];
            _model.invertWall(field.Position);
        }
        #endregion


        #region Game event handlers
        private void Model_GameOver(object sender, EventArgs e)
        {
            MessageBox.Show("Congrat! You won, with time: " + TimeSpan.FromSeconds(_model.getTime()).ToString("g"));
            CurrentGameStatus = "Game Over! You won!";
        }
        private void Model_TimeElapsed(object sender, EventArgs e)
        {
            OnPropertyChanged("GameTime");
        }
        private void Model_FieldRefresh(object sender, FieldRefreshEventArgs e)
        {

            if (e.Position == _model.getRobotPos())
                Fields[e.Position.X * _model.getSize() + e.Position.Y].Text = "robot";

            else
                Fields[e.Position.X * _model.getSize() + e.Position.Y].Text = _model.getBoard()[e.Position.X, e.Position.Y].ToString();

            GridButtonStyler(Fields[e.Position.X * _model.getSize() + e.Position.Y]);
            OnPropertyChanged();

        }
        private void Model_DrawBoard(object sender, EventArgs e)
        {
            drawBoard(); 
        }
        #endregion


        #region Event methods
        private void OnPause()
        {
            _model.pause();
            PauseEnabled = "False";
            PlayEnabled = "True";
            SaveEnabled = "True";
            LoadEnabled = "True";

            CurrentGameStatus = "Paused";
        }
        private void OnPlay()
        {
            _model.play();
            PauseEnabled = "True";
            PlayEnabled = "False";
            SaveEnabled = "False";
            LoadEnabled = "False";

            CurrentGameStatus = null;
        }
        private void OnNewGame(int size)
        {
            _model.newGame(size);
            drawBoard();
            OnPropertyChanged("BoardSize");

            PauseEnabled = "True";
            PlayEnabled = "False";
            SaveEnabled = "False";
            LoadEnabled = "False";
        }
        private void OnLoadGame()
        {
            LoadGame?.Invoke(this, EventArgs.Empty);
        }
        public void LoadDone()
        {
            OnPropertyChanged("BoardSize");
            drawBoard();
        }
        private void OnSaveGame()
        {
            SaveGame?.Invoke(this, EventArgs.Empty);
        }
        private void OnExitGame()
        {
            ExitGame?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}

