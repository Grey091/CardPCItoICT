using SYNOPEX_ICT.Stored;
using SYNOPEX_ICT.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SYNOPEX_ICT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new ScreenWorkVM(navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowVM(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
