using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BPDMH.Annotations;

namespace BPDMH.DataSet
{
    public class TransactionD : INotifyPropertyChanged
    {
        private int _pengirimanDId;
        private int _pengirimanId;
        private int _jmlColie;
        private string _pembungkusId;
        private string _ketBungkus;
        private string _namaBarang;
        private string _berat;

        public TransactionD()
        {
        }

        public int PengirimanDId
        {
            get { return _pengirimanDId; }
            set
            {
                _pengirimanDId = value;
                OnPropertyChanged("PengirimanDId");
            }
        }

        public int PengirimanId
        {
            get { return _pengirimanId; }
            set
            {
                _pengirimanId = value;
                OnPropertyChanged("PenerimaanId");
            }
        }

        public int JmlColie
        {
            get { return _jmlColie; }
            set
            {
                _jmlColie = value;
                OnPropertyChanged("JmlColie");
            }
        }
        public string PembungkusId
        {
            get { return _pembungkusId; }
            set
            {
                _pembungkusId = value;
                OnPropertyChanged("PembungkusId");
            }
        }
        public string KetPembungkus
        {
            get
            {
                return _ketBungkus;
            }
            set
            {
                _ketBungkus = value;
                OnPropertyChanged("KetPembungkus");
            }
        }
        public string NamaBarang
        {
            get { return _namaBarang; }
            set
            {
                _namaBarang = value;
                OnPropertyChanged("NamaBarang");
            }
        }
        public string Berat
        {
            get { return _berat; }
            set
            {
                _berat = value;
                OnPropertyChanged("Berat");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
