using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPDMH.DataSet
{
    class TransactionList
    {
        //private TransactionH _transactionH;
        private List<TransactionH> _transactions;
        private DCBPDMHDataContext dataContext = new DCBPDMHDataContext();
        private GetTHResult _trnH;
        private GetTDResult _transactionD;
        private  ObservableCollection<TransactionH> _transactionH;
        private List<TransactionH> _trnHList;
 
        public TransactionList()
        {
            _transactionH = new ObservableCollection<TransactionH>();
            var thById = dataContext.GetTH();
            
            foreach (var getTdResult in thById)
            {
                _trnH = new GetTHResult();
                _trnH.PengirimanId  = getTdResult.PengirimanId;
                _trnH.NoSeri = getTdResult.NoSeri;
                _trnH.TglInput = (DateTime) getTdResult.TglInput;
                _trnH.PengirimId = getTdResult.PengirimId;
                _trnH.NamaPengirim = getTdResult.NamaPengirim;
                _trnH.AlamatPengirim = getTdResult.AlamatPengirim;
                _trnH.PenerimaId = getTdResult.PenerimaId;
                _trnH.NamaPenerima = getTdResult.NamaPenerima;
                _trnH.AlamatPenerima = getTdResult.AlamatPenerima;
                _trnH.CabangId = getTdResult.CabangId;
                _trnH.NmCabang = getTdResult.NmCabang;
                _trnH.AlamatCabang = getTdResult.AlamatCabang;
                _trnH.PembayaranId = getTdResult.PembayaranId;
                _trnH.KetBayar = getTdResult.KetBayar;
                _trnH.KendaraanId = getTdResult.KendaraanId;
                _trnH.NoPolisi = getTdResult.NoPolisi;
                _trnH.Jenis = getTdResult.Jenis;
                _trnH.KaryawanId = getTdResult.KendaraanId;
                _trnH.Checker = getTdResult.Checker;
                _trnH.Biaya = (decimal) getTdResult.Biaya;
//                _trnH.TrnDetails.Clear();
                
                var tdById = dataContext.GetTDByPengId(_trnH.PengirimanId);
                foreach (var getTdByPengIdResult in tdById)
                {
                    _transactionD = new GetTDResult();
                    _transactionD.PengirimanDId = getTdByPengIdResult.PengirimanDId;
                    _transactionD.PengirimanId = getTdByPengIdResult.PengirimanId;
                    _transactionD.JmlColie = getTdByPengIdResult.JmlColie;
                    _transactionD.PembungkusId = getTdByPengIdResult.PembungkusId;
                    _transactionD.KetBungkus = getTdByPengIdResult.KetBungkus;
                    _transactionD.NamaBarang = getTdByPengIdResult.NamaBarang;
                    _transactionD.Berat = getTdByPengIdResult.Berat;
                }
            }
        }

        public TransactionList(string id)
        {
//            _transactionH = new ObservableCollection<TransactionH>();
//            var thById = dataContext.GetTHByPengId(id).SingleOrDefault();
//
//            var tdById = dataContext.GetTDByPengId(id).ToList();
//            var transactionH = new TransactionH()
//            {
//                PenerimaanId = thById.PenerimaanId,
//                NoSeri = thById.NoSeri,
//                TglInput = (DateTime)thById.TglInput,
//                PengirimId = thById.PenerimaanId,
//                NamaPengirim = thById.NamaPengirim,
//                PenerimaId = thById.PenerimaId,
//                NamaPenerima = thById.NamaPenerima,
//                CabangId = thById.CabangId,
//                NmCabang = thById.NmCabang,
//                AlamatCabang = thById.AlamatCabang,
//                PembayaranId = thById.PembayaranId,
//                KetBayar = thById.KetBayar,
//                KendaraanId = thById.KendaraanId,
//                NoPolisi = thById.NoPolisi,
//                Jenis = thById.Jenis,
//                KaryawanId = thById.KendaraanId,
//                Checker = thById.Checker,
//                Biaya = (decimal)thById.Biaya
//            };
//
//            transactionH.TrnDetails.Add(new TransactionD(){});
            
            //            var thById = dataContext.GetTHByPengId(id).SingleOrDefault();
            //            if (thById != null)
            //            {
            //                _transactionH.PenerimaanId = thById.PenerimaanId;
            //                _transactionH.NoSeri = thById.NoSeri;
            //                if (thById.TglInput != null) _transactionH.TglInput = (DateTime) thById.TglInput;
            //                _transactionH.PengirimId = thById.PengirimId;
            //                _transactionH.NamaPengirim = thById.NamaPengirim;
            //                _transactionH.PenerimaId = thById.PenerimaId;
            //                _transactionH.NamaPenerima = thById.NamaPenerima;
            //                _transactionH.CabangId = thById.CabangId;
            //                _transactionH.NmCabang = thById.NmCabang;
            //                _transactionH.AlamatCabang = thById.AlamatCabang;
            //                _transactionH.PembayaranId = thById.PembayaranId;
            //                _transactionH.KetBayar = thById.KetBayar;
            //                _transactionH.KendaraanId = thById.KendaraanId;
            //                _transactionH.NoPolisi = thById.NoPolisi;
            //                _transactionH.Jenis = thById.Jenis;
            //                _transactionH.KaryawanId = thById.KaryawanId;
            //                _transactionH.Checker = thById.Checker;
            //                if (thById.Biaya != null) _transactionH.Biaya = (decimal) thById.Biaya;
            //                _transactionH.TrnDetails.Add(GetTransactionDById(id));
            //            }

            //return transactionH;
        }

        public GetTDResult GetTransactionDById(int id)
        {
            _transactionD = new GetTDResult();
            var tdById = dataContext.GetTDByPengId(id).ToList();
            foreach (var getTdByPengIdResult in tdById)
            {
                _transactionD.PengirimanDId = getTdByPengIdResult.PengirimanDId;
                _transactionD.PengirimanId = getTdByPengIdResult.PengirimanId;
                _transactionD.JmlColie = getTdByPengIdResult.JmlColie;
                _transactionD.PembungkusId = getTdByPengIdResult.PembungkusId;
                _transactionD.KetBungkus = getTdByPengIdResult.KetBungkus;
                _transactionD.NamaBarang = getTdByPengIdResult.NamaBarang;
                _transactionD.Berat = getTdByPengIdResult.Berat;
            }

            return _transactionD;
        }

        public ObservableCollection<TransactionH> TransactionHs
        {
            get { return _transactionH; }
        }
    }
}
