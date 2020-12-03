using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using RoboChase.Model;
using RoboChase.ViewModel;
using RoboChase.View;
using Microsoft.Win32;

namespace RoboChase
{

    public partial class App : Application
    {
        #region Fields

        private RoboChaseModel _model;
        private RoboChaseViewModel _viewModel;
        private MainWindow _view;

        #endregion

        #region Constructors

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        #endregion

        #region Application event handlers

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new RoboChaseModel();

            
            _viewModel = new RoboChaseViewModel(_model);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);

            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //_model.AdvanceTime();
        }

        #endregion


        

        #region ViewModel event handlers
        private async void ViewModel_LoadGame(object sender, System.EventArgs e)
        {
            string filePath = string.Empty;
            
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "CRAZY files (*.crazy)|*.crazy";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Load Robo Chase Files";
                if (openFileDialog.ShowDialog() == true)
                {
                    await _model.loadFromFileAsync(openFileDialog.FileName);

                    filePath = openFileDialog.FileName;

                    _viewModel.LoadEnabled = "False";
                    _viewModel.SaveEnabled = "False";
                    _viewModel.PlayEnabled = "True";
                    _viewModel.PauseEnabled = "False";

                    _viewModel.CurrentGameStatus = "Game has loaded. Press play to continue";

                }
            }
            catch (Exception)
            {
                MessageBox.Show("A fájl betöltése sikertelen!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _viewModel.LoadDone();

        }


        private async void ViewModel_SaveGame(object sender, EventArgs e)
        {
            _viewModel.LoadEnabled = "False";
            _viewModel.SaveEnabled = "False";
            _viewModel.PlayEnabled = "True";
            _viewModel.PauseEnabled = "False";

            try
            {
                SaveFileDialog dialog = new SaveFileDialog(); 
                dialog.Filter = "Crazy files (*.crazy)|*.crazy";
                dialog.DefaultExt = "crazy*";
                dialog.AddExtension = true;
                if (dialog.ShowDialog() == true)
                {
                    if (!await _model.saveGameAsync(dialog.FileName))
                    {
                        MessageBox.Show("Cannot save file!!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Cannot save file!", "RoboChase", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBox.Show("Successfull Save!", "RoboChase", MessageBoxButton.OK, MessageBoxImage.Information);

        }

       
        private void ViewModel_ExitGame(object sender, System.EventArgs e)
        {
            _view.Close();
        }

        #endregion

    }
}
