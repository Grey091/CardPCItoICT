using SYNOPEX_ICT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYNOPEX_ICT.Models
{
    public class FPCB : BaseVM
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
        private string _Judge;
        public string Judge
        {
            get => _Judge;
            set
            {
                _Judge = value;
                OnPropertyChanged("Judge");
            }
        }
        private string _shortError;
        public string ShortError
        {
            get => _shortError;
            set
            {
                _shortError = value;
                OnPropertyChanged("ShortError");
            }
        }
        private string _openError;
        public string OpenError
        {
            get => _openError;
            set
            {
                _openError = value;
                OnPropertyChanged("OpenError");
            }
        }

        ~FPCB() { }
    }
}
