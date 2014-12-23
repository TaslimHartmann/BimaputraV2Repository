using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using AdvancedReportViewer.Utils;
using BPDMH.DataSet;
using BPDMH.Model;
using BPDMH.Tools;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Application = System.Windows.Forms.Application;

namespace BPDMH.Report
{
    /// <summary>
    /// Interaction logic for DMHReport.xaml
    /// </summary>
    public partial class DMHReport
    {
        private DCBPDMHDataContext _bpdmhContext;
        private string plgId;
        public string PlgId { get; set; }
        private string pnrId;
        public string PnrId { get; set; }
        private CollectionViewSource _custLis;
        private bool isPengirimUpdated;
        public bool IsPengirimUpdated { get; set; }
        private bool isPenerimaUpdated;
        public bool IsPenerimaUpdated { get; set; }
        private CollectionViewSource _masterViewSource;
        private CollectionViewSource _detailViewSource;
        private BindingListCollectionView _masterView;
        private BindingListCollectionView _detailView;
        private bool isReloaded;
        private int _errors;
        private CollectionViewSource customerListBy;
        private object penerimaListBy;
        private SaveFileDialog saveFileDialog;


        public DMHReport()
        {
            InitializeComponent();
            var sidepanel = DMHReportsViewer.FindName("btnToggleSidePanel") as ToggleButton;
            if (sidepanel != null)
            {
                DMHReportsViewer.ViewChange += (x, y) => sidepanel.IsChecked = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _bpdmhContext = new DCBPDMHDataContext();
            //            TglKirim1.SelectedDate = DateTime.Now.Date;
            TglPicker.DisplayDate = DateTime.Today;
            //            TglKirim1.Text = DateTime.Today.ToShortDateString();

            GetKendaraanLookup();
            GetCabangLookup();
            GetKaryawanLookup();
        }

        private void GetKaryawanLookup()
        {
            var karyawanList = _bpdmhContext.GetTable<Karyawan>()
                .OrderBy(k => k.Nama)
                .Select(k => new { k.KaryawanId, k.Nama });
            var karySource = (CollectionViewSource)this.FindResource("KaryawanLookup");
            karySource.Source = karyawanList;
        }

        private void GetCabangLookup()
        {
            var cabangList = _bpdmhContext.GetTable<BPDMH.DataSet.Cabang>()
               .Select(c => new { c.CabangId, c.NmCabang }).OrderBy(c => c.CabangId);
            var cabangSource = (CollectionViewSource)this.FindResource("CabangLookup");
            cabangSource.Source = cabangList;
        }

        private void GetKendaraanLookup()
        {
            var kendaraanList = _bpdmhContext.GetTable<Kendaraan>()
                .Select(k => new { k.KendaraanId, k.Jenis });
            var kendaraanSource = (CollectionViewSource)this.FindResource("KendaraanLookup");
            kendaraanSource.Source = kendaraanList;
        }

        private void BtnGetData_Click(object sender, RoutedEventArgs e)
        {
            var reportdocument = new ReportDocument();

            var dmhDesign = new DMHReportDesign();

            //reportdocument.SetDataSource(BpdmhContext.GetTH());


            reportdocument.Load("D:/PersonalProject/BP_DMH/BPDMH/BPDMH/Report/DMHReportDesign.rpt");
            var connectionString = ConfigurationManager.ConnectionStrings["BPDMH.Properties.Settings.BPDMHConnectionString"].ConnectionString.Split(';');
            var crDbConnection = new ConnectionInfo
            {
                ServerName = connectionString[0].Replace('\\', '/').After("="),
                DatabaseName = connectionString[1].After("="),
                UserID = connectionString[3].After("="),
                Password = connectionString[4].After("=")
            };

            reportdocument.SetDatabaseLogon(crDbConnection.UserID, crDbConnection.Password, crDbConnection.ServerName, crDbConnection.DatabaseName, true);

            //            var abc = trans.TdByPengIdResults.ToList();
            //            DMHReportsViewer.ViewerCore.ReportSource = reportdocument;
            var bisa = new DaftarMuatHarianList();
            //            GridTest.ItemsSource = bisa;
            // ListTest.ItemsSource = bisa;
            getDatas();
        }

        private void getDatas()
        {
            var thResult = _bpdmhContext.GetTransaksiH();
            var daftarMuatList = new List<DaftarMuatHarian>();
            foreach (var getThResult in thResult.ToList())
            {
                var daftarMuatHarian = new DaftarMuatHarian()
                {
                    PengirimanId = getThResult.TrnPengirimanId,
                    NoSeri = getThResult.NoSeri,
                    TglInput = (DateTime)getThResult.TglInput,
//                    PengirimId = getThResult.PengirimId,
                    NamaPengirim = getThResult.NamaPengirim,
                    AlamatPengirim = getThResult.AlamatPengirim,
//                    PenerimaId = getThResult.PenerimaId,
                    NamaPenerima = getThResult.NamaPenerima,
                    AlamatPenerima = getThResult.AlamatPenerima,
                    CabangId = getThResult.CabangId,
                    NamaCabang = getThResult.NmCabang,
                    AlamatCabang = getThResult.Alamat,
                    PembayaranId = getThResult.PembayaranId,
                    KetBayar = getThResult.Keterangan,
                    KendaraanId = getThResult.KendaraanId,
                    NoPolisi = getThResult.NoPolisi,
                    Jenis = getThResult.Jenis,
                    KaryawanId = getThResult.KaryawanId,
                    Checker = getThResult.Nama,
                    BiayaPenerus = getThResult.BiayaPenerus,
                    Biaya = getThResult.Biaya,
                    TdByPengIdResults = GetDetailData(getThResult)
                };
                daftarMuatList.Add(daftarMuatHarian);
            }
//            ListTest.ItemsSource = daftarMuatList;
            var listCollection = new ListCollectionView(daftarMuatList);
            if (listCollection.GroupDescriptions != null)
                listCollection.GroupDescriptions.Add(new PropertyGroupDescription("NoSeri"));
            GridTest.ItemsSource = listCollection;
        }

        private List<GetTransaksiDByPengIdResult> GetDetailData(GetTransaksiHResult getThResult)
        {
            var detailList = new List<GetTransaksiDByPengIdResult>();
            var getTdPengIdResult = new GetTransaksiDByPengIdResult();
            var TdResult = _bpdmhContext.GetTDByPengId(getThResult.TrnPengirimanId);
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

        private void DMHReportsViewer_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void DMHReportsViewer_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            var reportdocument = new ReportDocument();
            reportdocument.Load(Application.StartupPath + "\\DMHReportDesign.rpt");
            var crDbConnection = new ConnectionInfo
            {
                IntegratedSecurity = true,
                DatabaseName = "BPDMH",
                ServerName = "TASLIMS_LAPTOP/SQLEXPRESS",
                Password = "th1478",
                UserID = "thartmann"
            };

            //            var crDatabase = reportdocument.Database;
            //
            //            foreach (Table oCrTable in crDatabase.Tables)
            //            {
            //                var loginInfo = oCrTable.LogOnInfo;
            //                loginInfo.ConnectionInfo = crDbConnection;
            //                oCrTable.ApplyLogOnInfo(loginInfo);
            //            }

            reportdocument.SetDatabaseLogon(crDbConnection.UserID, crDbConnection.Password, crDbConnection.ServerName, crDbConnection.DatabaseName, true);
            DMHReportsViewer.ViewerCore.ReportSource = reportdocument;
        }
    }
}
