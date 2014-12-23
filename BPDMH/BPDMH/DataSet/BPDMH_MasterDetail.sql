SELECT        dbo.PengirimanH.PengirimanId, dbo.PengirimanH.NoSeri, dbo.PengirimanH.TglInput, dbo.PengirimanH.PengirimId, dbo.Pelanggan.NamaPlg, dbo.Pelanggan.Telp, dbo.PengirimanH.PenerimaId, 
                         dbo.PengirimanH.CabangId, dbo.Cabang.NmCabang, dbo.Cabang.Telp AS Expr1, dbo.PengirimanH.PembayaranId, dbo.PengirimanH.KendaraanId, dbo.Kendaraan.NoPolisi, dbo.PengirimanH.Biaya, 
                         dbo.PengirimanH.Terbilang, dbo.PengirimanH.KaryawanId, dbo.Karyawan.Nama, dbo.Karyawan.Jabatan, dbo.PengirimanD.PengirimanDId, dbo.PengirimanD.JmlColie, dbo.PengirimanD.PembungkusId, 
                         dbo.Pembungkus.Keterangan, dbo.PengirimanD.NamaBarang, dbo.PengirimanD.Berat
FROM            dbo.PengirimanD INNER JOIN
                         dbo.Cabang INNER JOIN
                         dbo.Pelanggan INNER JOIN
                         dbo.PengirimanH ON dbo.Pelanggan.PelangganId = dbo.PengirimanH.PengirimId ON dbo.Cabang.CabangId = dbo.PengirimanH.CabangId INNER JOIN
                         dbo.Kendaraan ON dbo.PengirimanH.KendaraanId = dbo.Kendaraan.KendaraanId INNER JOIN
                         dbo.Karyawan ON dbo.PengirimanH.KaryawanId = dbo.Karyawan.KaryawanId ON dbo.PengirimanD.PengirimanId = dbo.PengirimanH.PengirimanId INNER JOIN
                         dbo.Pembungkus ON dbo.PengirimanD.PembungkusId = dbo.Pembungkus.PembungkusId