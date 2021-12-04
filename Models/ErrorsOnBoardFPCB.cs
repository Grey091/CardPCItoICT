using SYNOPEX_ICT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYNOPEX_ICT.Models
{
    public class ErrorsOnBoardFPCB : BaseVM
    {
        private int _Id;
        public int Id 
        {
            get => _Id; 
            set 
            { 
                _Id = value; 
                OnPropertyChanged("Id");
            } 
        }
        private string _resultEvaluation;
        public string ResultEvaluation 
        {
            get => _resultEvaluation;
            set
            {
                _resultEvaluation = value;
                OnPropertyChanged("ResultEvaluation");
            } 
        }
        private string _shortLocations;
        public string ShortLocations
        {
            get => _shortLocations;
            set
            {
                _shortLocations = value;
                OnPropertyChanged("ShortLocations");
            }
        }
        private string _openLocations;
        public string OpenLocations
        {
            get => _openLocations;
            set
            {
                _openLocations = value;
                OnPropertyChanged("OpenLocations");
            }
        }
        private string _TimeMesuament;
        public string TimeMesuament 
        { 
            get => _TimeMesuament; 
            set
            { 
                _TimeMesuament = value;
                OnPropertyChanged("TimeMesuament"); 
            } 
        }

        ~ErrorsOnBoardFPCB() { }
    }
}
