using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using RoboChase.Model;

namespace RoboChase.ViewModel
{
    public class RoboChaseViewModel : ViewModelBase
    {



        #region Fields

        private RoboChaseModel _model;

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

        public bool _pauseEnabled;
        public bool _playEnabled;

        public String PauseEnabled { get { return _pauseEnabled ? "True" : "False"; } set { _pauseEnabled = (value == "True"); OnPropertyChanged();  } }
        public String PlayEnabled { get { return _playEnabled ? "True" : "False"; } set { _playEnabled = (value == "True"); OnPropertyChanged(); } }

        #endregion

        #region Events

        public event EventHandler LoadGame;
        public event EventHandler SaveGame;
        public event EventHandler ExitGame;

        #endregion

        #region Constructors

        public RoboChaseViewModel(RoboChaseModel model)
        {
            _model = model;
            Fields = new ObservableCollection<Field>();

            _pauseEnabled = false;
            _pauseEnabled = true;

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

        private void StepGame(Int32 index)
        {
            Field field = Fields[index];

            /*

            _model.Step(field.X, field.Y);

            field.Text = _model.Table[field.X, field.Y] > 0 ? _model.Table[field.X, field.Y].ToString() : String.Empty; // visszaírjuk a szöveget
            OnPropertyChanged("GameStepCount"); // jelezzük a lépésszám változást

            field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
            */
        }

        #endregion

        #region Game event handlers


        private void Model_GameOver(object sender, EventArgs e)
        {
            foreach (Field field in Fields)
            {
                field.IsLocked = true; // minden mezőt lezárunk
            }
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

            OnPropertyChanged();
            //field.IsLocked = _model.Table.IsLocked(field.X, field.Y);

        }

        private void drawBoard()
        {
            Fields.Clear();
            //Fields = new ObservableCollection<Field>();
            for (Int32 i = 0; i < _model.getSize(); i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.getSize(); j++)
                {
                    Fields.Add(new Field
                    {
                        IsLocked = true,
                        Text = (_model.getBoard()[i, j]).ToString() + ", " + i.ToString() + ", " + j.ToString() + " - " + _model.getSize().ToString(),
                        X = i,
                        Y = j,
                        Number = i * _model.getSize() + j, // a gomb sorszáma, amelyet felhasználunk az azonosításhoz
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                        // ha egy mezőre léptek, akkor jelezzük a léptetést, változtatjuk a lépésszámot
                    });
                }
            }

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

        }

        private void OnPlay()
        {
            _model.play();
            PauseEnabled = "True";
            PlayEnabled = "False";
        }

        private void OnNewGame(int size)
        {
            _model.newGame(size);
            drawBoard();
            OnPropertyChanged("BoardSize");
        }
        private void OnLoadGame()
        {
            LoadGame?.Invoke(this, EventArgs.Empty);
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

