using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Locator.Model;
using Locator.ViewModel;
using Microsoft.Win32;

namespace Locator
{

    public partial class App : Application
    {
        #region Fields

        private LocatorModel _model;
        private LocatorViewModel _viewModel;
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
            _model = new LocatorModel();

            _viewModel = new LocatorViewModel(_model);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);

            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();
        }

        #endregion


        #region ViewModel event handlers

        private void ViewModel_ExitGame(object sender, System.EventArgs e)
        {
            _view.Close();
        }

        #endregion

    }
}
