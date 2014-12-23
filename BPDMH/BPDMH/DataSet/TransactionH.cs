using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BPDMH.Annotations;

namespace BPDMH.DataSet
{
    public class TransactionH : INotifyPropertyChanged
    {
        private int _pengirimanId;
        private string _noSeri;
        private DateTime _tglInput;
        private string _pengirimId;
        private string _namaPengirim;
        private string _alamatPengirim;
        private string _penerimaId;
        private string _namaPenerima;
        private string _alamatPenerima;
        private string _cabangId;
        private string _nmCabang;
        private string _alamatCabang;
        private string _pembayaranId;
        private string _ketBayar;
        private string _kendaraanId;
        private string _noPolisi;
        private string _jenis;
        private string _karyawanId;
        private string _checker;
        private decimal _biaya;

        private ObservableCollection<TransactionD> _trnDetails = new ObservableCollection<TransactionD>();

        //        private GetTHResult _thResult;
        //        private List<GetTDResult> _tdResult;
        //        private GetTHByPengIdResult _thResultById;
        //        private List<GetTDByPengIdResult> _tdResulstByIds;
        //        private TransactionH _transactionH;
        //        private List<TransactionH> _transactions;
        //        private DCBPDMHDataContext dc = new DCBPDMHDataContext();

        public TransactionH()
        {
            //            var dc = new DCBPDMHDataContext();
            //            _thResult = dc.GetTH().SingleOrDefault();
            //            _tdResult = dc.GetTD().ToList();
        }

        public TransactionH(string pengirimanId)
        {
            //            var dc = new DCBPDMHDataContext();
            //            _thResultById = dc.GetTHByPengId(pengirimanId).SingleOrDefault();
            //            _tdResulstByIds = dc.GetTDByPengId(pengirimanId).ToList();

        }


        public event PropertyChangedEventHandler PropertyChanged;

        public int PengirimanId
        {
            get { return _pengirimanId; }
            set
            {
                _pengirimanId = value;
                OnPropertyChanged("PenerimaanId");
            }
        }

        public string NoSeri
        {
            get { return _noSeri; }
            set
            {
                _noSeri = value;
                OnPropertyChanged("NoSeri");
            }
        }

        public DateTime TglInput
        {
            get { return _tglInput; }
            set
            {
                _tglInput = value;
                OnPropertyChanged("TglInput");
            }
        }

        public string PengirimId
        {
            get { return _pengirimId; }
            set
            {
                _pengirimId = value;
                OnPropertyChanged("PengirimId");
            }
        }

        public string NamaPengirim
        {
            get { return _namaPengirim; }
            set
            {
                _namaPengirim = value;
                OnPropertyChanged("NamaPengirim");
            }
        }

        public string AlamatPengirim
        {
            get { return _alamatPengirim; }
            set
            {
                _alamatPengirim = value;
                OnPropertyChanged("AlamatPengirim");
            }
        }

        public string PenerimaId
        {
            get { return _penerimaId; }
            set
            {
                _penerimaId = value;
                OnPropertyChanged("PenerimaId");
            }
        }

        public string NamaPenerima
        {
            get { return _namaPenerima; }
            set
            {
                _namaPenerima = value;
                OnPropertyChanged("NamaPenerima");
            }
        }

        public string AlamatPenerima
        {
            get { return _alamatPenerima; }
            set
            {
                _alamatPenerima = value;
                OnPropertyChanged("AlamatPenerima");
            }
        }

        public string CabangId
        {
            get { return _cabangId; }
            set
            {
                _cabangId = value;
                OnPropertyChanged("CabangId");
            }
        }

        public string NmCabang
        {
            get { return _nmCabang; }
            set
            {
                _nmCabang = value;
                OnPropertyChanged("NmCabang");
            }
        }

        public string AlamatCabang
        {
            get { return _alamatCabang; }
            set
            {
                _alamatCabang = value;
                OnPropertyChanged("AlamatCabang");
            }
        }

        public string PembayaranId
        {
            get { return _pembayaranId; }
            set
            {
                _pembayaranId = value;
                OnPropertyChanged("PembayaranId");
            }
        }

        public string KetBayar
        {
            get { return _ketBayar; }
            set
            {
                _ketBayar = value;
                OnPropertyChanged("KetBayar");
            }
        }

        public string KendaraanId
        {
            get { return _kendaraanId; }
            set
            {
                _kendaraanId = value;
                OnPropertyChanged("KendaraanId");
            }
        }

        public string NoPolisi
        {
            get { return _noPolisi; }
            set
            {
                _noPolisi = value;
                OnPropertyChanged("NoPolisi");
            }
        }

        public string Jenis
        {
            get { return _jenis; }
            set
            {
                _jenis = value;
                OnPropertyChanged("Jenis");
            }
        }

        public string KaryawanId
        {
            get { return _karyawanId; }
            set
            {
                _karyawanId = value;
                OnPropertyChanged("KaryawanId");
            }
        }

        public string Checker
        {
            get { return _checker; }
            set
            {
                _checker = value;
                OnPropertyChanged("Checker");
            }
        }

        public ObservableCollection<TransactionD> TrnDetails
        {
            get { return _trnDetails; }
        }

        public decimal Biaya
        {
            get { return _biaya; }
            set
            {
                _biaya = value;
                OnPropertyChanged("Biaya");
            }
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}