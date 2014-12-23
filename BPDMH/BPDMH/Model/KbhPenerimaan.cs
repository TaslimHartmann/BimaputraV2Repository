using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPDMH.DataSet;

namespace BPDMH.Model
{
    public class KbhPenerimaan
    {
        private int _penerimaanId;
        public int PenerimaanId { get; set; }
        private string _noSeri;
        public string NoSeri { get; set; }
        private DateTime _tglInput;
        public DateTime TglInput { get; set; }
        private string _pengirimId;
        public string PengirimId { get; set; }
        private string _namaPengirim;
        public string NamaPengirim { get; set; }
        private string _alamatPengirim;
        public string AlamatPengirim { get; set; }
        private string _penerimaId;
        public string PenerimaId { get; set; }
        private string _namaPenerima;
        public string NamaPenerima { get; set; }
        private string _alamatPenerima;
        public string AlamatPenerima { get; set; }
        private string _cabangId;
        public string CabangId { get; set; }
        private string _namaCabang;
        public string NamaCabang { get; set; }
        private string _alamatCabang;
        public string AlamatCabang { get; set; }
        private string _pembayaranId;
        public string PembayaranId { get; set; }
        private string _ketBayar;
        public string KetBayar { get; set; }
        private string _kendaraanId;
        public string KendaraanId { get; set; }
        private string _noPolisi;
        public string NoPolisi { get; set; }
        private string _jenis;
        public string Jenis { get; set; }
        private string _karyawanId;
        public string KaryawanId { get; set; }
        private string _checker;
        public string Checker { get; set; }

        private decimal? _biayaPenerus;
        public decimal? BiayaPenerus { get; set; }

        private decimal? _biaya;
        public decimal? Biaya { get; set; }

        private GetTDByPengIdResult getTdPengIdResult = new GetTDByPengIdResult();

        public ISingleResult<GetTHResult> ThResult { get; set; }

        private List<GetTHResult> _thResults = new List<GetTHResult>();
        public GetTDByPengIdResult TdResult { get; set; }

        public List<GetTrnPenerimaanDByPengIdResult> TdByPengIdResults { get; set; }

        public List<GetRtrTerimaByPengIdResult> RtrTerima { get; set; }

        public List<GetTHResult> ThResults
        {
            get { return _thResults; }
            set { _thResults = value; }
        }

        public KbhPenerimaan()
        {
        }

        public KbhPenerimaan(int penerimaanId, string noSeri, DateTime? tglInput,
            string namaPengirim, string alamatPengirim, string namaPenerima,
            string alamatPenerima, string cabangId, string namaCabang, string alamatCabang,
            string pembayaranId, string ketBayar, string kendaraanId, string noPolisi, string jenis,
            string karyawanId, string checker, decimal? biayaPenerus, decimal? biaya,
            List<GetTrnPenerimaanDByPengIdResult> tdByPengIdResults, List<GetRtrTerimaByPengIdResult> rtrTerimaByPengId)
        {
            _penerimaanId = penerimaanId;
            _noSeri = noSeri;
            if (tglInput != null) _tglInput = (DateTime)tglInput;
            //            _pengirimId = pengirimId;
            _namaPengirim = namaPengirim;
            _alamatPengirim = alamatPengirim;
            //            _penerimaId = penerimaId;
            _namaPenerima = namaPenerima;
            _alamatPenerima = alamatPenerima;
            _cabangId = cabangId;
            _namaCabang = namaCabang;
            _alamatCabang = alamatCabang;
            _pembayaranId = pembayaranId;
            _ketBayar = ketBayar;
            _kendaraanId = kendaraanId;
            _noPolisi = noPolisi;
            _jenis = jenis;
            _karyawanId = karyawanId;
            _checker = checker;
            _biayaPenerus = biayaPenerus;
            _biaya = biaya;
            TdByPengIdResults = tdByPengIdResults;
            RtrTerima = rtrTerimaByPengId;
        }
    }
}
