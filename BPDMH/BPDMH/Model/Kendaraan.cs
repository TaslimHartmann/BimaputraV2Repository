//using BPDMH.Interfaces;
//
//namespace BPDMH.Model
//{
//    public class Kendaraan : Repository<Kendaraan>, IColumnNames
//    {
//        private string _kdKendaraan;
//        private string _noPol;
//        private readonly IColumnNames _iColumnNames;
//        private IColumnNames iColumnNames;
//        public string KdKendaraan { get; set; }
//        public string NoPol { get; set; }
//        public string[] ColumnNames { get; set; }
//
//        public Kendaraan()
//        {
//            
//        }
//
//        public Kendaraan(string kdKendaraan, string noPol)
//        {
//            _kdKendaraan = kdKendaraan;
//            _noPol = noPol;
//        }
//
//        public string[] GetColumnNames()
//        {
//            return new[] { "KdKnd", "NoPolisi", "Jenis", "Keterangan"};
//        }
//
//        
//    }
//}