using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BPDMH.DataSet;
using BPDMH.MasterData;

namespace BPDMH.Transaksi
{
    /// <summary>
    /// Interaction logic for Latihan.xaml
    /// </summary>
    public partial class Latihan : Window
    {
        private DCBPDMHDataContext bpdmhContext;
        private IEnumerable<PengirimanH> TrnHeader;
        private IEnumerable<GetTDResult> TrnDetail;
        private CollectionViewSource MasterViewSource;
        private CollectionViewSource DetailViewSource;
        private CollectionViewSource custLis;
        private CollectionViewSource CabangViewSource;
        private BindingListCollectionView CabangView;
        private BindingListCollectionView custMaster;
        private BindingListCollectionView MasterView;
        private BindingListCollectionView DetailView;
        public bool isPengirimUpdated = false;
        public bool isPenerimaUpdated = false;
        public bool isPengirimNeedTobeUpdated = false;
        public bool isPenerimaNeedTobeUpdated = false;
        public string plgId;
        public string pnrId;

        private CollectionViewSource cabangList;
        private BindingListCollectionView cabangMaster;
        private IEnumerable<PengirimanH> PengH;
        private List<int> detailLists = new List<int>();
        public Latihan()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bpdmhContext = new DCBPDMHDataContext();

            var customerList = bpdmhContext.GetTable<Pelanggan>()
                                           .Select(a => new { PengirimId = a.PelangganId, a.NamaPlg, a.Alamat });

            var customerListBy = bpdmhContext.GetTable<Pelanggan>()
                                             .Where(c => c.PelangganId == plgId)
                                             .Select(a => new { PengirimId = a.PelangganId, a.NamaPlg, a.Alamat }).ToList();

            var penerimaList = bpdmhContext.GetTable<Pelanggan>()
                                           .Select(a => new { PenerimaId = a.PelangganId, a.NamaPlg, a.Alamat });

            var penerimaListBy = bpdmhContext.GetTable<Pelanggan>()
                                             .Where(c => c.PelangganId == pnrId)
                                             .Select(a => new { PenerimaId = a.PelangganId, a.NamaPlg, a.Alamat }).ToList();

            var kendaraanList = bpdmhContext.GetTable<Kendaraan>()
                                            .Select(k => new { k.KendaraanId, k.Jenis });

            var cabangList = bpdmhContext.GetTable<Cabang>()
                                         .Select(c => new { c.CabangId, c.NmCabang }).OrderBy(c => c.CabangId);


            var karyawanList = bpdmhContext.GetTable<Karyawan>()
                                           .OrderBy(k => k.Nama)
                                           .Select(k => new { k.KaryawanId, k.Nama });

            //            var PembungkusList = bpdmhContext.GetTable<Pembungkus>()
            //                                             .Select(p => p);
            var PembungkusList = from p in bpdmhContext.Pembungkus
                                 select new { p.Keterangan, p.PembungkusId };

            var pembungkusSource = (CollectionViewSource)this.FindResource("PembungkusLookup");
            pembungkusSource.Source = PembungkusList;

            this.custLis = (CollectionViewSource)this.FindResource("PelangganLookup");
            var penerimaSource = (CollectionViewSource)this.FindResource("PenerimaLookup");
            if (isPengirimUpdated || isPenerimaUpdated && pnrId != null)
            {
                custLis.Source = customerListBy;
                penerimaSource.Source = penerimaListBy;
            }
            else if (isPengirimUpdated && pnrId == null)
            {
                custLis.Source = customerListBy;
                penerimaSource.Source = penerimaList;
            }
            else if (isPenerimaUpdated && plgId == null)
            {
                penerimaSource.Source = penerimaListBy;
                custLis.Source = customerList;
            }
            else
            {
                penerimaSource.Source = penerimaList;
                custLis.Source = customerList;
            }

            var kendaraanSource = (CollectionViewSource)this.FindResource("KendaraanLookup");
            kendaraanSource.Source = kendaraanList;

            var cabangSource = (CollectionViewSource)this.FindResource("CabangLookup");
            cabangSource.Source = cabangList;
            this.CabangView = (BindingListCollectionView)cabangSource.View;
            this.CabangView.MoveCurrentToFirst();

            this.CabangViewSource = (CollectionViewSource)this.FindResource("CabangLookup");

            var karySource = (CollectionViewSource)this.FindResource("KaryawanLookup");
            karySource.Source = karyawanList;

            TrnDetail = bpdmhContext.GetTD();

            this.MasterViewSource = (CollectionViewSource)this.FindResource("MasterView");
            this.DetailViewSource = (CollectionViewSource)this.FindResource("DetailView");

            TrnHeader = bpdmhContext.GetTable<PengirimanH>();
            this.PengH = bpdmhContext.GetTable<PengirimanH>();
            MasterViewSource.Source = TrnHeader;
//            DetailViewSource.Source = TrnDetail;

            this.MasterView = (BindingListCollectionView)this.MasterViewSource.View;
            this.MasterView.CurrentChanged += new EventHandler(MasterView_CurrentChanged);
            this.DetailView = (BindingListCollectionView)this.DetailViewSource.View;
        }

        void MasterView_CurrentChanged(object sender, EventArgs e)
        {
            this.DetailView = (BindingListCollectionView)this.DetailViewSource.View;
        }

        private void ListViewTransaksi_GotFocus(object sender, RoutedEventArgs e)
        {
            dynamic item = (ListViewItem)sender;
            this.ListViewTransaksi.SelectedItem = item.DataContext;
        }

        private void MasterView_CurrentChanged(object sender, RoutedEventArgs e)
        {
            this.DetailView = (BindingListCollectionView)this.MasterViewSource.View;
        }

        private void AddRecord()
        {
            this.MasterView.AddNew();
            this.MasterView.CommitNew();
        }

        private void DeleteRecord()
        {
            if (this.MasterView.CurrentPosition > -1)
            {
                var order = (PengirimanH)this.MasterView.CurrentItem;
                foreach (var detail in order.PengirimanDs)
                {
                    bpdmhContext.PengirimanDs.DeleteOnSubmit(detail);
                }
                this.MasterView.RemoveAt(this.MasterView.CurrentPosition);
            }
        }

        private void GoToPrevious()
        {
            MasterView.MoveCurrentToPrevious();
        }

        private void NextRecord()
        {
            MasterView.MoveCurrentToNext();
        }

        private void AddDetailRecord()
        {
            this.DetailView.AddNew();
            this.DetailView.CommitNew();
        }

        private void DeleteDetailRecord()
        {
//            if (DetailView.CurrentPosition > -1)
//                DetailView.RemoveAt(DetailView.CurrentPosition);
            if (DetailView.CurrentPosition > -1)
                foreach (var t in detailLists)
                    DetailView.RemoveAt(t);
        }

        private void SaveRecord()
        {
            bpdmhContext.SubmitChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var pelanggan = new MstPelanggan();
            pelanggan.IsPengirim = true;
            if (!String.IsNullOrEmpty(cboPenerima.Text))
                pelanggan.KdPenerima = cboPenerima.Text;
            this.Close();
            pelanggan.ShowDialog();
        }

        private void btnCariPenerima(object sender, RoutedEventArgs e)
        {
            var pelanggan = new MstPelanggan();
            pelanggan.IsPengirim = true;
            pelanggan.ShowDialog();
        }

        private void btnCrPenerima_Click(object sender, RoutedEventArgs e)
        {
            var pelanggan = new MstPelanggan();
            pelanggan.IsPenerima = true;
            if (!String.IsNullOrEmpty(cboPengirim.Text))
                pelanggan.KdPengirim = cboPengirim.Text;

            this.Close();
            pelanggan.ShowDialog();
        }

        private void Item_GotFocus(object sender, RoutedEventArgs e)
        {
            var item = ((ListViewItem)(sender));
            this.ListViewTransaksi.SelectedItem = item.DataContext;
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            GoToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            NextRecord();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddRecord();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecord();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveRecord();
        }

        private void btnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            AddDetailRecord();
        }

        private void btnDeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            DeleteDetailRecord();
        }

        private void cbDetail_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var o in ListViewTransaksi.SelectedItems)
                detailLists.Add(ListViewTransaksi.Items.IndexOf(o));
        }

        private void btnDeleteDetail_Click_1(object sender, RoutedEventArgs e)
        {
            if (DetailView.CurrentPosition > -1)
                DetailView.RemoveAt(DetailView.CurrentPosition);
            SaveRecord();
        }
    }
}
