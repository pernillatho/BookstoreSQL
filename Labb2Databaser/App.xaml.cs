using Labb2Databaser.Managers;
using Labb2Databaser.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Labb2Databaser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationManager _navigationManager;
        private readonly DataManager _dataManager;

        public App()
        {
            _navigationManager = new NavigationManager();
            _dataManager = new DataManager();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationManager.CurrentViewModel = new StartViewModel(_dataManager, _navigationManager);

            var mainWindow = new MainWindow { DataContext = new MainViewModel(_navigationManager, _dataManager) };
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
