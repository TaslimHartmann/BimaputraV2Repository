using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BPDMH.Annotations;
using BPDMH.DataSet;

namespace BPDMH.Tools
{
     
    public class CheckBoxCabang : INotifyPropertyChanged
    {
        private string _cabangId;
        private bool _isChecked;
        public Cabang Cabang;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                NotifyPropertyChanged("IsChecked");
            }
        }

        public string CabangId
        {
            get { return _cabangId; }
            set
            {
                _cabangId = value;
                NotifyPropertyChanged("CabangId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string strPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
        }
    }
}
