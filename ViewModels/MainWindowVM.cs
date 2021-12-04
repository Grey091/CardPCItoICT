using SYNOPEX_ICT.Commands;
using SYNOPEX_ICT.Services;
using SYNOPEX_ICT.Stored;
using SYNOPEX_ICT.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SYNOPEX_ICT.ViewModels
{
    [Serializable]
    class MainWindowVM : BaseVM
    {
        #region Properties
        private readonly NavigationStore _navigationStore;
        public BaseVM CurrentViewModel => _navigationStore.CurrentViewModel;
        #endregion

        #region Commands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand NavigateAnalysisCadModeCommand { get; }
        public ICommand NavigateScreenWorkCommand { get; }
        public ICommand NavigateInputCoordinatesCommand { get; }        
        public ICommand NavigateIOWindowCommand { get; }
        public ICommand NavigateResultsWindowCommand { get; }
        #endregion
        public MainWindowVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateScreenWorkCommand = new NavigateCommand<ScreenWorkVM>(new NavigationService<ScreenWorkVM>(
               navigationStore, () => new ScreenWorkVM(navigationStore)));

            NavigateAnalysisCadModeCommand = new NavigateCommand<AnalysisCadVM>(new NavigationService<AnalysisCadVM>(
               navigationStore, () => new AnalysisCadVM(navigationStore)));

            NavigateInputCoordinatesCommand = new NavigateCommand<InputCoordinatesVM>(new NavigationService<InputCoordinatesVM>(
              navigationStore, () => new InputCoordinatesVM(navigationStore)));
            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {                
                Application.Current.Shutdown();
            });

            NavigateResultsWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ResultsWindow wd = new ResultsWindow(); wd.ShowDialog(); });

            NavigateIOWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { DigitalIO wd = new DigitalIO(); wd.ShowDialog(); });
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public MainWindowVM()
        {
            #region Commands
            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //MessageBox.Show("1");
                Application.Current.Shutdown();
            });
            #endregion
        }

       
    }
}
