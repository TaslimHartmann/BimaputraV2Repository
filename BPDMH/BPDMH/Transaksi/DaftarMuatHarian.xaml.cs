using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using BPDMH.DataSet;
using ExportToExcel;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.MessageBox;

namespace BPDMH.Transaksi
{
    /// <summary>
    /// Interaction logic for DaftarMuatHarian1.xaml
    /// </summary>
    public partial class DaftarMuatHarian : Window
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

        public DaftarMuatHarian()
        {
            InitializeComponent();
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
                .Select(k => new {k.KaryawanId, k.Nama});
            var karySource = (CollectionViewSource) this.FindResource("KaryawanLookup");
            karySource.Source = karyawanList;
        }

        private void GetCabangLookup()
        {
            var cabangList = _bpdmhContext.GetTable<Cabang>()
                .Select(c => new {c.CabangId, c.NmCabang}).OrderBy(c => c.CabangId);
            var cabangSource = (CollectionViewSource) this.FindResource("CabangLookup");
            cabangSource.Source = cabangList;
        }

        private void GetKendaraanLookup()
        {
            var kendaraanList = _bpdmhContext.GetTable<Kendaraan>()
                .Select(k => new {k.KendaraanId, k.Jenis});
            var kendaraanSource = (CollectionViewSource) this.FindResource("KendaraanLookup");
            kendaraanSource.Source = kendaraanList;
        }

        private void ListHeader_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic pengH = ListHeader.SelectedItem;
            if (pengH == null) return;
            ListDetail.ItemsSource = _bpdmhContext.GetTDByPengId(pengH.PengirimanId);
        }

        private void BtnGetData_Click(object sender, RoutedEventArgs e)
        {
            ListHeader.ItemsSource = null;
            ListDetail.ItemsSource = null;
           // GetValue(CboCabang, TglKirim1);
//            var sour = BpdmhContext.GetTrn();
//            ListHeader.ItemsSource = sour;
            var listTrn = _bpdmhContext.GetTrn().OrderBy(c => c.NoSeri).ToList();
            ListHeader.ItemsSource = new BindingList<GetTrnResult>(listTrn);
        }

        private void GetValue(ComboBox cboCabang, DatePicker tglPicker)
        {
            var sor = Enumerable.Empty<GetTHResult>();
            if (CboCabang.SelectedItem != null && TglPicker.SelectedDate != null)
                sor = _bpdmhContext.GetTH().Where(c => c.CabangId == cboCabang.SelectedValue.ToString() && c.TglInput == TglPicker.SelectedDate);
             else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate == null)
                sor = _bpdmhContext.GetTH().Where(c => c.CabangId == cboCabang.SelectedValue.ToString());
            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate != null)
                sor = _bpdmhContext.GetTH().Where(c => c.TglInput == tglPicker.SelectedDate);
            else
                sor = _bpdmhContext.GetTH();
            
            var getThResults = sor as GetTHResult[] ?? sor.ToArray();
            if (!getThResults.Any())
                DisplayNoData();
            else
            {
                ListHeader.SelectedIndex = 0;
                SetItemSource(getThResults);
            }
        }

        private void SetItemSource(IList<GetTHResult> getThResults)
        {
            ListHeader.ItemsSource = getThResults.ToList();
            ListDetail.ItemsSource = _bpdmhContext.GetTDByPengId(getThResults[0].PengirimanId);
        }

        private void DisplayNoData()
        {
            MessageBox.Show("Data tidak ditemukan", "Informasi");
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            var s = new ExportToExcel<GetTrnResult, GetTrnResults>();
            var view = CollectionViewSource.GetDefaultView(ListHeader.ItemsSource);
            s.dataToPrint = (BindingList<GetTrnResult>) view.SourceCollection;
            s.GenerateReport();
//
//            var s = new ExportToExcel<GetTDByPengIdResult, GetTDByPengIdResults>();
//            var view = CollectionViewSource.GetDefaultView(ListDetail.ItemsSource);
//            s.dataToPrint = (BindingList<GetTDByPengIdResult>)view.SourceCollection;
//            s.GenerateReport();

        }
    }
}
