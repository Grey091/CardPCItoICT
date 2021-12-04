using SYNOPEX_ICT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYNOPEX_ICT.Models
{
    public class PointCheck :BaseVM
    {
        private double _X;
        public double Xcoordinates
        {
            get => _X;
            set { _X = value; OnPropertyChanged("Xcoordinates"); }
        }
        private double _Y;
        public double Ycoordinates
        {
            get => _Y;
            set { _Y = value; OnPropertyChanged("Ycoordinates"); }
        }
        private int _ID;
        public int ID
        {
            get => _ID;
            set { _ID = value; OnPropertyChanged("ID"); }
        }
    }
}
