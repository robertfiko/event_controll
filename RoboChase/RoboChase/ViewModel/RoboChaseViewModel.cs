using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using RoboChase.Model;

namespace RoboChase.ViewModel
{
    public class RoboChaseViewModel : ViewModelBase
    {
        #region Fields

        private RoboChaseModel _model; // modell

        #endregion

        #region Properties

        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand LoadGameCommand { get; private set; }
        public DelegateCommand SaveGameCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public ObservableCollection<Field> Fields { get; set; }

  
        public String GameTime { get { return TimeSpan.FromSeconds(_model.getTime()).ToString("g"); } }

        #endregion

        #region Events

        public event EventHandler NewGame;
        public event EventHandler LoadGame;
        public event EventHandler SaveGame;
        public event EventHandler ExitGame;

        #endregion

        #region Constructors

        public RoboChaseViewModel(RoboChaseModel model)
        {
            // játék csatlakoztatása
            _model = model;

       
            _model.refreshBoard += new EventHandler<EventArgs>(Model_DrawBoard);
            //_model.refreshField += new EventHandler<FieldRefreshEventArgs>(Model_FieldRefresh);
            _model.refreshTime += new EventHandler<EventArgs>(Model_TimeElapsed);
            _model.OnGameOver += new EventHandler<EventArgs>(Model_GameOver);

            // parancsok kezelése
            NewGameCommand = new DelegateCommand(param => OnNewGame());
            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitCommand = new DelegateCommand(param => OnExitGame());


            // játéktábla létrehozása
            Fields = new ObservableCollection<Field>();
            for (Int32 i = 0; i < _model.getSize(); i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.getSize(); j++)
                {
                    Fields.Add(new Field
                    {
                        IsLocked = true,
                        Text = String.Empty,
                        X = i,
                        Y = j,
                        Number = i * _model.getSize() + j, // a gomb sorszáma, amelyet felhasználunk az azonosításhoz
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                        // ha egy mezőre léptek, akkor jelezzük a léptetést, változtatjuk a lépésszámot
                    });
                }
            }

            RefreshTable();
        }

        #endregion

        #region Private methods

        private void RefreshTable()
        {
            foreach (Field field in Fields) // inicializálni kell a mezőket is
            {
                field.Text = _model.getBoard()[field.X, field.Y].ToString();
                //field.IsLocked = _model.Table.IsLocked(field.X, field.Y);
            }

            OnPropertyChanged("GameTime");
            OnPropertyChanged("GameStepCount");
        }

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


        private void Model_DrawBoard(object sender, EventArgs e)
        {
            RefreshTable();
        }

        #endregion

        #region Event methods

        private void OnNewGame()
        {
            if (NewGame != null)
                NewGame(this, EventArgs.Empty);
        }

        private void OnLoadGame()
        {
            if (LoadGame != null)
                LoadGame(this, EventArgs.Empty);
        }

      
        private void OnSaveGame()
        {
            if (SaveGame != null)
                SaveGame(this, EventArgs.Empty);
        }

        
        private void OnExitGame()
        {
            if (ExitGame != null)
                ExitGame(this, EventArgs.Empty);
        }

        #endregion
    }
}

