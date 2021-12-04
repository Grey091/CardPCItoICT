using SYNOPEX_ICT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYNOPEX_ICT.Models
{
    public class ErrorFPCB :  BaseVM
    {
        private ErrorType _errorType;
        public ErrorType ErrorTypes
        {
            get => _errorType;
            set
            {
                _errorType = value;
                OnPropertyChanged("ErrorTypes");
            }
        }

        private int _location;
        public int Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }

        ~ErrorFPCB() { }
    }
}
