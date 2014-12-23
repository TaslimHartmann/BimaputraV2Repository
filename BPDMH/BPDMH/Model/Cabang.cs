using System.Collections.Generic;
using BPDMH.Interfaces;

namespace BPDMH.Model
{
    public class Cabang : ICrud<Cabang>, IColumnNames
    {
        public string KdCabang { get; set; }
        public string NmCabang { get; set; }
        public string Telp { get; set; }
        public string Fax { get; set; }
        public string KtPerson { get; set; }
        public string Alamat { get; set; }
        private readonly string[] _columnNames = new string[] { "KdCabang", "NmCabang", "Telp", "Fax", "KtPerson", "Alamat"};

        public string[] ColumnNames
        {
            get { return _columnNames; }
        }
    

        public Cabang()
        {
        }

        public void Save(string tableName, Cabang model)
        {
            
            string colNames = null;
            foreach (var colName in model._columnNames)
            {
                colNames = colName + ", ";
            }

            if (colNames == null) return;
            colNames = colNames.Remove(colNames.Length -1, 1) + "";
            var sql = string.Format("INSERT into {0} VALUES ({1})", tableName, colNames);
        }

        public void Update(string tableName, Cabang model)
        {
            string colNames = null;
            foreach (var colName in model._columnNames)
            {
                colNames = colName + "= "+ colName + ", ";
            }

            if (colNames == null) return;
            colNames = colNames.Remove(colNames.Length - 1, 1) + "";
            var sql = string.Format("UPDATE {0} SET {1} WHERE Kode={2}", tableName, colNames, model._columnNames[0]);
        }

        public void DeleteById(string tableName, Cabang model)
        {
            var sql = string.Format("DELETE FROM {0} WHERE Kode={1}", tableName, model._columnNames[0]); 
        }

        public List<Cabang> GetAll(Cabang model)
        {
//            var sql = string.Format("SELECT * FROM {0}", model);
//            return List<Cabang>;
            throw new System.NotImplementedException();
        }

        public Cabang GetById(Cabang model)
        {
//            var sql = string.Format("SELECT * FROM {0}", model);
//            return Cabang;
            throw new System.NotImplementedException();
        }

        public string[] GetColumnNames()
        {
            return _columnNames;
        }
    }
}