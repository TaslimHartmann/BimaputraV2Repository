using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BPDMH.DataSet;

namespace BPDMH.Model
{
    class KbhPengiriman
    {
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

        public List<GetRtrKirimByPengIdResult> RtrKirim { get; set; }

        public List<GetTHResult> ThResults
        {
            get { return _thResults; }
            set { _thResults = value; }
        }

        public KbhPengiriman()
        {
        }

        public KbhPengiriman(int pengirimanId, string noSeri, DateTime? tglInput,
            string namaPengirim, string alamatPengirim, string namaPenerima,
            string alamatPenerima, string cabangId, string namaCabang, string alamatCabang,
            string pembayaranId, string ketBayar, string kendaraanId, string noPolisi, string jenis,
            string karyawanId, string checker, decimal? biayaPenerus, decimal? biaya,
            List<GetTransaksiDByPengIdResult> tdByPengIdResults, List<GetRtrKirimByPengIdResult> rtrTerimaByPengId)
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
            RtrKirim = rtrTerimaByPengId;
        }

    }

    public class LaporanPengirimanList : IEnumerable
    {
        private DCBPDMHDataContext _dataContext = new DCBPDMHDataContext();
        private KbhPengiriman[] lapPengirimans;
        private int position = -1;
        private List<KbhPengiriman> lapPengirimanList;
        private KbhPengiriman lapPengiriman;
        public ISingleResult<GetTransaksiDByPengIdResult> TdResult { get; set; }

        public ISingleResult<GetTransaksiHResult> ThResult { get; set; }
        private GetTransaksiDByPengIdResult getTdPengIdResult = new GetTransaksiDByPengIdResult();
        private GetRtrKirimByPengIdResult getRtrKirim = new GetRtrKirimByPengIdResult();
        public ISingleResult<GetRtrKirimByPengIdResult> RtrKirimResult { get; set; }


        private string NoSeri { get; set; }

        //Create internal array in constructor.
        public LaporanPengirimanList()
        {
            lapPengirimanList = new List<KbhPengiriman>();
            {
                ThResult = _dataContext.GetTransaksiH();
                foreach (var getThResult in ThResult.ToList())
                {
                    lapPengiriman = new KbhPengiriman(getThResult.TrnPengirimanId, getThResult.NoSeri,
                        getThResult.TglInput, getThResult.NamaPengirim,
                        getThResult.AlamatPengirim, getThResult.NamaPenerima,
                        getThResult.AlamatPenerima, getThResult.CabangId, getThResult.NmCabang,
                        getThResult.Alamat, getThResult.PembayaranId, getThResult.Keterangan,
                        getThResult.KendaraanId, getThResult.NoPolisi, getThResult.Jenis,
                        getThResult.KaryawanId, getThResult.Nama, getThResult.BiayaPenerus, getThResult.Biaya,
                        GetDetailData(getThResult), GetReturKirim(getThResult));
                    NoSeri = lapPengiriman.NoSeri;
                    lapPengirimanList.Add(lapPengiriman);
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
            }
            return detailList;
        }

        private List<GetRtrKirimByPengIdResult> GetReturKirim(GetTransaksiHResult getThResult)
        {
            var tdResult = _dataContext.GetRtrKirimByPengId(getThResult.TrnPengirimanId);
            return tdResult.Select(getThResultForDetail => new GetRtrKirimByPengIdResult
            {
                ReturTrnKirimId = getThResultForDetail.ReturTrnKirimId, 
                PengirimanId = getThResultForDetail.PengirimanId,
                TglInput = getThResultForDetail.TglInput, 
                TglTerima = getThResultForDetail.TglTerima, 
                Penerima = getThResultForDetail.Penerima, 
                KaryawanId = getThResultForDetail.KaryawanId, 
                Nama = getThResultForDetail.Nama
            }).ToList();
        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            foreach (object o in lapPengirimanList)
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
            return (position < lapPengirimanList.Count);
        }

        //IEnumerable
        public void Reset()
        { position = 0; }

        //IEnumerable
        public object Current
        {
            get { return lapPengirimanList[position]; }
        }
    }
}
