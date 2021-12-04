using SYNOPEX_ICT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYNOPEX_ICT.Models
{
    public class Monitor : BaseVM
    {
        private int _ID;
        public int ID
        {
            get => _ID;
            set { _ID = value; OnPropertyChanged("ID"); }
        }
        private double _commandPosition;
        public double commandPosition
        {
            get => _commandPosition;
            set { _commandPosition = value; OnPropertyChanged("JigCode"); }
        }
        private double _commandVelocity;
        public double commandVelocity
        {
            get => _commandVelocity;
            set { _commandVelocity = value; OnPropertyChanged("ProductCode"); }
        }
        private double _feedbackPosition;
        public double feedbackPosition
        {
            get => _feedbackPosition;
            set { _feedbackPosition = value; OnPropertyChanged("sequenceLed1"); }
        }
        private double _moveVelocity;
        public double moveVelocity
        {
            get => _moveVelocity;
            set { _moveVelocity = value; OnPropertyChanged("sequenceLed2"); }
        }
        private double _moveAccel;
        public double moveAccel
        {
            get => _moveAccel;
            set { _moveAccel = value; OnPropertyChanged("sequenceLed3"); }
        }
        private double _moveDecel;
        public double moveDecel
        {
            get => _moveDecel;
            set { _moveDecel = value; OnPropertyChanged("sequenceLed4"); }
        }
    }
}
