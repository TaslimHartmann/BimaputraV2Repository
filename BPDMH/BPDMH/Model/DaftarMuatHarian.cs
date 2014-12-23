using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using BPDMH.DataSet;

namespace BPDMH.Model
{
    public class DaftarMuatHarian
    {
        public int ColumnRows
        {
            get { return 21; }
        }

        public DaftarMuatHarian()
        {
        }

        public DaftarMuatHarian(int pengirimanId, string noSeri, DateTime? tglInput,
            string namaPengirim, string alamatPengirim, string namaPenerima,
            string alamatPenerima, string cabangId, string namaCabang, string alamatCabang,
            string pembayaranId, string ketBayar, string kendaraanId, string noPolisi, string jenis,
            string karyawanId, string checker, decimal? biayaPenerus, decimal? biaya, List<GetTransaksiDByPengIdResult> tdByPengIdResults)
        {
            _pengirimanId = pengirimanId;
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
        }

        //        public DaftarMuatHarian()
        //        {
        //            ThResult = _dataContext.GetTH();
        //            foreach (var getThResult in ThResult)
        //            {
        //                PenerimaanId = getThResult.PenerimaanId;
        //                NoSeri = getThResult.NoSeri;
        //                if (getThResult.TglInput != null) TglInput = (DateTime)getThResult.TglInput;
        //                PengirimId = getThResult.PengirimId;
        //                NamaPengirim = getThResult.NamaPengirim;
        //                AlamatPengirim = getThResult.AlamatPengirim;
        //                PenerimaId = getThResult.PenerimaId;
        //                NamaPenerima = getThResult.NamaPenerima;
        //                AlamatPenerima = getThResult.AlamatPenerima;
        //                CabangId = getThResult.CabangId;
        //                NamaCabang = getThResult.NmCabang;
        //                AlamatCabang = getThResult.AlamatCabang;
        //                PembayaranId = PembayaranId;
        //                KetBayar = getThResult.KetBayar;
        //                KendaraanId = getThResult.KendaraanId;
        //                NoPolisi = getThResult.NoPolisi;
        //                Jenis = getThResult.Jenis;
        //                KaryawanId = getThResult.KaryawanId;
        //                Checker = getThResult.Checker;
        //                if (getThResult.Biaya != null) Biaya = (decimal)getThResult.Biaya;
        //
        //                _thResults.Add(getThResult);
        //
        //                TdResult = _dataContext.GetTDByPengId(getThResult.PenerimaanId);
        //                foreach (var getThResultForDetail in TdResult)
        //                {
        //                    getTdPengIdResult.JmlColie = getThResultForDetail.JmlColie;
        //                    getTdPengIdResult.PembungkusId = getThResultForDetail.PembungkusId;
        //                    getTdPengIdResult.KetBungkus = getThResultForDetail.KetBungkus;
        //                    getTdPengIdResult.NamaBarang = getThResultForDetail.NamaBarang;
        //                    getTdPengIdResult.Berat = getThResultForDetail.Berat;
        //
        //                    _tdByPengIdResults.Add(TdResult);
        //                }
        //            }
        //
        //        }

        private int _pengirimanId;
        public int PengirimanId { get; set; }
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

        public List<GetTransaksiDByPengIdResult> TdByPengIdResults { get; set; }

        public List<GetTHResult> ThResults
        {
            get { return _thResults; }
            set { _thResults = value; }
        }
    }

    public class DaftarMuatHarianList : IEnumerable
    {
        private DCBPDMHDataContext _dataContext = new DCBPDMHDataContext();
        private DaftarMuatHarian[] daftarMuatHarianList;
        private int position = -1;
        private List<DaftarMuatHarian> daftarMuatList;
        private DaftarMuatHarian daftarMuatHarian;
        public ISingleResult<GetTransaksiDByPengIdResult> TdResult { get; set; }

        public ISingleResult<GetTransaksiHResult> ThResult { get; set; }
        private GetTransaksiDByPengIdResult getTdPengIdResult = new GetTransaksiDByPengIdResult();
        private string NoSeri { get; set; }

        //Create internal array in constructor.
        public DaftarMuatHarianList()
        {
            daftarMuatList = new List<DaftarMuatHarian>();
            {
                ThResult = _dataContext.GetTransaksiH();
                foreach (var getThResult in ThResult.ToList())
                {
                    daftarMuatHarian = new DaftarMuatHarian(getThResult.TrnPengirimanId, getThResult.NoSeri,
                        getThResult.TglInput, getThResult.NamaPengirim,
                        getThResult.AlamatPengirim, getThResult.NamaPenerima,
                        getThResult.AlamatPenerima, getThResult.CabangId, getThResult.NmCabang,
                        getThResult.Alamat, getThResult.PembayaranId, getThResult.Keterangan,
                        getThResult.KendaraanId, getThResult.NoPolisi, getThResult.Jenis,
                        getThResult.KaryawanId, getThResult.Nama, getThResult.BiayaPenerus, getThResult.Biaya,
                        GetDetailData(getThResult));
                    NoSeri = daftarMuatHarian.NoSeri;
                    daftarMuatList.Add(daftarMuatHarian);
                    //_thResults.Add(getThResult);

                    //GetDetailData(getThResult);
                }
            }
        }

        private List<GetTransaksiDByPengIdResult> GetDetailData(GetTransaksiHResult getThResult)
        {
            var detailList = new List<GetTransaksiDByPengIdResult>();

            TdResult = _dataContext.GetTransaksiDByPengId(getThResult.TrnPengirimanId);
            foreach (var getThResultForDetail in TdResult)
            {
                getTdPengIdResult.JmlColie = getThResultForDetail.JmlColie;
                getTdPengIdResult.PembungkusId = getThResultForDetail.PembungkusId;
                getTdPengIdResult.KetBungkus = getThResultForDetail.KetBungkus;
                getTdPengIdResult.NamaBarang = getThResultForDetail.NamaBarang;
                getTdPengIdResult.Berat = getThResultForDetail.Berat;
                detailList.Add(getTdPengIdResult);
                // _tdByPengIdResults.Add(TdResult);
            }
            return detailList;
        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            foreach (object o in daftarMuatList)
            {
                // Lets check for end of list (its bad code since we used arrays)
                if (o == null)
                {
                    break;
                }

                // Return the current element and then on next function call 
                // resume from next element rather than starting all over again;
                yield return o;
            }
        }

        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < daftarMuatList.Count);
        }

        //IEnumerable
        public void Reset()
        { position = 0; }

        //IEnumerable
        public object Current
        {
            get { return daftarMuatList[position]; }
        }
    }

    public class DaftarMuatHarians : BindingList<DaftarMuatHarian>
    {
    }
}
