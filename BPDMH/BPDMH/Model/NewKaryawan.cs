using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPDMH.Model
{
    public class NewKaryawan
    {
        private string _karyawanId;

        private string _nama;

        private string _jabatan;

        private string _telpon;

        private string _alamat;

        private string _password;

        private string _status;
        public NewKaryawan(string karyawanId, string nama, string jabatan, string telpon, string alamat, string password, string status)
        {
            KaryawanId = karyawanId;
            Nama = nama;
            Jabatan = jabatan;
            Telpon = telpon;
            Alamat = alamat;
            Password = password;
            Status = status;
        }

        public string KaryawanId
        {
            get { return _karyawanId; }
            set { _karyawanId = value; }
        }

        public string Nama
        {
            get { return _nama; }
            set { _nama = value; }
        }

        public string Jabatan
        {
            get { return _jabatan; }
            set { _jabatan = value; }
        }

        public string Telpon
        {
            get { return _telpon; }
            set { _telpon = value; }
        }

        public string Alamat
        {
            get { return _alamat; }
            set { _alamat = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
