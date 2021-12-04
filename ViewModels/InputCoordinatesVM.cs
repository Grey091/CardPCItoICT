using SYNOPEX_ICT.Models;
using SYNOPEX_ICT.Stored;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SYNOPEX_ICT.ViewModels
{
    class InputCoordinatesVM : BaseVM
    {
        #region Properties
        private ObservableCollection<PointCheck> _ListData;
        public ObservableCollection<PointCheck> ListData
        {
            get => _ListData;
            set
            {
                _ListData = value;
                OnPropertyChanged();
            }
        }
        private double _coorJ1X;
        public double coorJ1X
        {
            get => _coorJ1X;
            set { _coorJ1X = value; OnPropertyChanged("coorJ1X"); }
        }
        private double _coorJ1Y;
        public double coorJ1Y
        {
            get => _coorJ1Y;
            set { _coorJ1Y = value; OnPropertyChanged("coorJ1Y"); }
        }
        private double _coorJ2X;
        public double coorJ2X
        {
            get => _coorJ2X;
            set { _coorJ2X = value; OnPropertyChanged("coorJ2X"); }
        }
        private double _coorJ2Y;
        public double coorJ2Y
        {
            get => _coorJ2Y;
            set { _coorJ2Y = value; OnPropertyChanged("coorJ2Y"); }
        }
        private double _coorJ3X;
        public double coorJ3X
        {
            get => _coorJ3X;
            set { _coorJ3X = value; OnPropertyChanged("coorJ3X"); }
        }
        private double _coorJ3Y;
        public double coorJ3Y
        {
            get => _coorJ3Y;
            set { _coorJ3Y = value; OnPropertyChanged("coorJ3Y"); }
        }
        private double _coorJ4X;
        public double coorJ4X
        {
            get => _coorJ4X;
            set { _coorJ4X = value; OnPropertyChanged("coorJ4X"); }
        }
        private double _coorJ4Y;
        public double coorJ4Y
        {
            get => _coorJ4Y;
            set { _coorJ4Y = value; OnPropertyChanged("coorJ4Y"); }
        }
        private double _coorJ5X;
        public double coorJ5X
        {
            get => _coorJ5X;
            set { _coorJ5X = value; OnPropertyChanged("coorJ5X"); }
        }
        private int _coorJ5Y;
        public int coorJ5Y
        {
            get => _coorJ5Y;
            set { _coorJ5Y = value; OnPropertyChanged("coorJ5Y"); }
        }
        #endregion

        #region Commands
        public ICommand SaveDataCommand { get; set; }
        #endregion
        public InputCoordinatesVM()
        {           
            SaveDataCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("sdasdsadsad");
            });
        }
        public InputCoordinatesVM(NavigationStore navigationStore){}
    }
}
