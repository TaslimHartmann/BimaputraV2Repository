using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using BPDMH.DataSet;
using BPDMH.MasterData;
using BPDMH.Tools;

namespace BPDMH.Transaksi
{
    /// <summary>
    /// Interaction logic for TrnPenerimaan.xaml
    /// </summary>
    public partial class TrnPenerimaan
    {
        public DCBPDMHDataContext BpdmhContext { get; set; }
        public string PlgId { get; set; }
        public string PnrId { get; set; }
        private CollectionViewSource _custLis;
        public CollectionViewSource CustList
        {
            get { return _custLis; }
            set { _custLis = value; }
        }
        public bool IsPengirimUpdated { get; set; }
        public bool IsPenerimaUpdated { get; set; }
        private CollectionViewSource _masterViewSource;
        public CollectionViewSource MasterViewSource
        {
            get { return _masterViewSource; }
            set { _masterViewSource = value; }
        }
        private CollectionViewSource _detailViewSource;
        private BindingListCollectionView _masterView;
        private BindingListCollectionView _detailView = new BindingListCollectionView(new BindingList<PenerimaanD>());
        //        private bool isReloaded;
        private int _errors;
        private CollectionViewSource _penerimaSource;
        public CollectionViewSource PenerimaSource
        {
            get { return _penerimaSource; }
            set { _penerimaSource = value; }
        }

        public TransactionH TransactionH { get; set; }

        private List<string> _trnPngList;
        private List<string> _trnAlamatList;
        private List<string> _trnPnrList;
        private List<string> _trnAlamatPnrList;

        private bool _isReloaded;
        private List<string> uniquePnr;
        private List<string> uniqueList;
        private List<string> pengirimList;
        private List<string> penerimaList;
        private List<string> alamtPengirimList;
        private List<string> alamtPnrList;

        public TrnPenerimaan()
        {
            InitializeComponent();
            ModalDialogOk.SetParent(MainGrid);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialValue();
            GetMasterData();
            TbNoSp.IsEnabled = false;
        }

        private void InitialValue()
        {
            BpdmhContext = new DCBPDMHDataContext();
            //            TglKirim1.SelectedDate = DateTime.Now.Date;
            //TglKirim1.DisplayDate = DateTime.Today;
//            TglPicker.Text = DateTime.Today.ToShortDateString();
            TglPicker.SelectedDate = DateTime.Today;

            GetKendaraanLookup();
            GetCabangLookup();
            GetKaryawanLookup();
            GetPembayaranLookup();
            GetPembungkusLookup();
            GetCustomerList();
            
            _isReloaded = true;
        }

        private void GetMasterData()
        {
            _masterViewSource = (CollectionViewSource)FindResource("MasterView");
            _detailViewSource = (CollectionViewSource)FindResource("DetailView");

            _masterViewSource.Source = BpdmhContext.GetTable<TrnPenerimaanH>();
            _masterView = (BindingListCollectionView)_masterViewSource.View;
            _masterView.MoveCurrentToLast();
            _detailView = (BindingListCollectionView)_detailViewSource.View;
            _masterView.CurrentChanged += MasterView_CurrentChanged;
        }

        private void GetCustomerList()
        {
            var customerList = BpdmhContext.GetTable<Pelanggan>()
                .Where(a => a.IsPengirim == true)
                .Select(a => new { PengirimId = a.PelangganId, a.NamaPlg, a.Alamat });

            var customerListBy = BpdmhContext.GetTable<Pelanggan>()
                .Where(c => c.PelangganId == PlgId)
                .Select(a => new { PengirimId = a.PelangganId, a.NamaPlg, a.Alamat }).ToList();

            var penerimaList = BpdmhContext.GetTable<Pelanggan>()
                .Where(a => a.IsPengirim == false)
                .Select(a => new { PenerimaId = a.PelangganId, a.NamaPlg, a.Alamat });

            var penerimaListBy = BpdmhContext.GetTable<Pelanggan>()
                .Where(c => c.PelangganId == PnrId)
                .Select(a => new { PenerimaId = a.PelangganId, a.NamaPlg, a.Alamat }).ToList();

            _custLis = (CollectionViewSource)FindResource("PelangganLookup");
            _penerimaSource = (CollectionViewSource)FindResource("PenerimaLookup");
            if (IsPengirimUpdated || IsPenerimaUpdated && PnrId != null)
            {
                _custLis.Source = customerListBy;
                _penerimaSource.Source = penerimaListBy;
            }
            else if (IsPengirimUpdated && PnrId == null)
            {
                _custLis.Source = customerListBy;
                _penerimaSource.Source = penerimaList;
            }
            else if (IsPenerimaUpdated && PlgId == null)
            {
                _penerimaSource.Source = penerimaListBy;
                _custLis.Source = customerList;
            }
            else
            {
                _penerimaSource.Source = penerimaList;
                _custLis.Source = customerList;
            }
        }

        private void GetPembungkusLookup()
        {
            var pembungkusList = from p in BpdmhContext.Pembungkus
                                 select new { p.Keterangan, p.PembungkusId };
            var pembungkusSource = (CollectionViewSource)FindResource("PembungkusLookup");
            pembungkusSource.Source = pembungkusList;
        }

        private void GetPembayaranLookup()
        {
            var pembayaranList = BpdmhContext.GetTable<Pembayaran>()
                .Select(p => p);
            var pembayaranSource = (CollectionViewSource)FindResource("PembayaranLookup");
            pembayaranSource.Source = pembayaranList;
        }

        private void GetKaryawanLookup()
        {
            var karyawanList = BpdmhContext.GetTable<Karyawan>()
                .OrderBy(k => k.Nama)
                .Select(k => new { k.KaryawanId, k.Nama });
            var karySource = (CollectionViewSource)FindResource("KaryawanLookup");
            karySource.Source = karyawanList;
        }

        private void GetCabangLookup()
        {
            var cabangList = BpdmhContext.GetTable<Cabang>()
                .Select(c => new { c.CabangId, c.NmCabang }).OrderBy(c => c.CabangId);
            var cabangSource = (CollectionViewSource)FindResource("CabangLookup");
            cabangSource.Source = cabangList;
        }

        private void GetKendaraanLookup()
        {
            var kendaraanList = BpdmhContext.GetTable<Kendaraan>()
                .Select(k => new { k.KendaraanId, k.NoPolisi });
            var kendaraanSource = (CollectionViewSource)FindResource("KendaraanLookup");
            kendaraanSource.Source = kendaraanList;
        }

        private void MasterView_CurrentChanged(object sender, EventArgs e)
        {
            _detailView = (BindingListCollectionView)_detailViewSource.View;
        }

        private void BtnCariPengirim_Click(object sender, RoutedEventArgs e)
        {
            var pelanggan = new MstPelangganTerima(this) { IsPengirim = true };
            if (!String.IsNullOrEmpty(CboPenerima.Text))
            {
                pelanggan.KdPenerima = CboPenerima.Text;
            }
            Close();
            pelanggan.Show();
        }

        private void BtnCrPenerima_Click(object sender, RoutedEventArgs e)
        {
            var pelanggan = new MstPelangganTerima(this) { IsPenerima = true };
            if (!String.IsNullOrEmpty(CboPengirim.Text))
                pelanggan.KdPengirim = CboPengirim.Text;
            Close();
            pelanggan.Show();
        }

        private void Item_GotFocus(object sender, RoutedEventArgs e)
        {
            var item = ((ListViewItem)(sender));
            ListViewTransaksi.SelectedItem = item.DataContext;
        }

        private void AddRecord()
        {
            _masterView.AddNew();
            TbBiayaTotal.Clear();
            BtnPrevious.IsEnabled = true;
            BtnNext.IsEnabled = true;
        }

        private void DeleteRecord()
        {
            if (_masterView.CurrentPosition <= -1)
                return;
            var order = (TrnPenerimaanH)_masterView.CurrentItem;
            foreach (var detail in order.TrnPenerimaanDs)
            {
                BpdmhContext.TrnPenerimaanDs.DeleteOnSubmit(detail);
            }
            BpdmhContext.TrnPenerimaanHs.DeleteOnSubmit(order);
            BpdmhContext.SubmitChanges();
            _masterView.RemoveAt(_masterView.CurrentPosition);
        }

        private void GoToPrevious()
        {
            if (_masterView.CurrentPosition <= 0)
                return;

            _masterView.MoveCurrentToPrevious();
        }

        private void NextRecord()
        {
            if (_masterView.CurrentPosition < _masterView.Count - 1)
            _masterView.MoveCurrentToNext();
        }

        private void AddDetailRecord()
        {
            //            if (_detailView == null)
            //            {
            //                _detailViewSource.Source = new CollectionViewSource().Source;
            //                _detailView = (BindingListCollectionView)_detailViewSource.View;
            //            }
            _detailView.AddNew();
            _detailView.CommitNew();
        }

        private void SaveRecord()
        {
            var valid = BpdmhContext.GetTrnPenerimaanH()
                    .FirstOrDefault(a => a.NoSeri == TbNoSp.Text && a.TglInput == TglPicker.SelectedDate);
            if (valid != null)
            {
                _masterView.CommitEdit();
            }
            else
            {
                _masterView.CommitNew();
            }

            try
            {
                BpdmhContext.SubmitChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Gagal menyimpan data", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            TbBiayaTotal.Clear();
            TbNoSp.Focus();
//            MessageBox.Show("Data berhasil disimpan", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnDeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            if (_detailView.CurrentPosition > -1)
                _detailView.RemoveAt(_detailView.CurrentPosition);
            SaveRecord();
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            _isReloaded = true;
            GoToPrevious();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NextRecord();
            _isReloaded = !_masterView.IsCurrentAfterLast;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            DisableController(true);
            AddRecord();
            TglPicker.SelectedDate = DateTime.Today;
            TbNoSp.Focus();
            _isReloaded = false;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbNoSp.Text))
                return;
            if (MessageBox.Show("Yakin akan menghapus data?", "Perhatian", MessageBoxButton.YesNo,
                MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                DeleteRecord();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbNoSp.Text))
                return;
//            if (TglPicker.SelectedDate == null)
//            {
//                TglPicker.SelectedDate = DateTime.Today;
//            }
            SaveRecord();
            AddRecord();
            TbNoSp.Focus();
        }

        private void BtnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            AddDetailRecord();
        }

        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _errors == 0;
            e.Handled = true;
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_masterView.IsCurrentAfterLast)
            {
                AddRecord();
//                TglPicker.Text = DateTime.Now.ToShortDateString();
                TglPicker.SelectedDate = DateTime.Today;
                TbNoSp.Focus();
            }

            e.Handled = true;
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors++;
            else
                _errors--;
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var fieldInfo = typeof(Window).GetField("_isClosing", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
                fieldInfo.SetValue(this, false);
            e.Cancel = true;
            Hide();
        }

        private void BtnCariTransaksi_OnClick(object sender, RoutedEventArgs e)
        {
            var res = ModalDialogOk.ShowHandlerDialog(TbNoSp.Text);
            if (!res) return;
            var nosp = ModalDialogOk.NoSp;
            var tgl = ModalDialogOk.TglKirim;

            _masterViewSource = (CollectionViewSource)FindResource("MasterView");
            _detailViewSource = (CollectionViewSource)FindResource("DetailView");

            IQueryable<TrnPenerimaanH> trnPenerimaanHs = null;
            if (nosp != null && tgl != null)
            {
                trnPenerimaanHs = BpdmhContext.GetTable<TrnPenerimaanH>()
                    .Where(t => t.NoSeri == nosp && t.TglInput == tgl);
            }
            else if (nosp != null && tgl == null)
            {
                trnPenerimaanHs = BpdmhContext.GetTable<TrnPenerimaanH>()
                    .Where(t => t.NoSeri == nosp);
            }
            else if (nosp == null && tgl != null)
            {
                trnPenerimaanHs = BpdmhContext.GetTable<TrnPenerimaanH>()
                    .Where(t => t.TglInput == tgl);
            }
            else if (nosp == null && tgl == null)
            {
                return;
            }

            if (trnPenerimaanHs != null && trnPenerimaanHs.Any())
            {
                //_masterView.EditItem(trnPengirimanHs);

                _masterViewSource.Source = trnPenerimaanHs;
                _masterView = (BindingListCollectionView)_masterViewSource.View;
                _detailView = (BindingListCollectionView)_detailViewSource.View;
                _masterView.CurrentChanged += MasterView_CurrentChanged;
//                BtnPrevious.IsEnabled = true;
//                BtnNext.IsEnabled = true;
            }
            else
            {
                InitialValue();
                _masterViewSource.Source = null;
                _masterView = (BindingListCollectionView)_masterViewSource.View;
                _detailView = (BindingListCollectionView)_detailViewSource.View;
                TbBiayaTotal.Clear();
                TbTerbilang.Clear();
                BtnPrevious.IsEnabled = false;
                BtnNext.IsEnabled = false;

                MessageBox.Show("Data tidak ditemukan", "Informasi", MessageBoxButton.OK,
                   MessageBoxImage.Information);
                TbNoSp.Focus();
            }
        }

        private void TbNamaPengirim_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isReloaded) return;
            var typedString = TbNamaPengirim.Text.Trim();
            
            _trnPngList = BpdmhContext.GetTrnPenerimaan()
                .Select(a => a.NamaPengirim).ToList();

            pengirimList = new List<string>();
            pengirimList.Clear();

            pengirimList.AddRange(from item in _trnPngList
                              where !string.IsNullOrWhiteSpace(TbNamaPengirim.Text)
                              let str = item.ToLower()
                              where str.Contains(typedString.ToLower())
                              select item);

            if (pengirimList.Count > 0)
            {
                PlgLookup.ItemsSource = pengirimList.Distinct();
                PlgLookup.Visibility = Visibility.Visible;
            }
            else if (TbNamaPengirim.Text.Equals(""))
            {
                PlgLookup.Visibility = Visibility.Collapsed;
                PlgLookup.ItemsSource = null;
            }
            else
            {
                PlgLookup.Visibility = Visibility.Collapsed;
                PlgLookup.ItemsSource = null;
            }
        }

        private void TbAlamatPengirim_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isReloaded) return;
            var typedString = TbAlamatPengirim.Text.Trim();
            
            _trnAlamatList = BpdmhContext.GetTrnPenerimaan()
                .Select(a => a.AlamatPengirim).ToList();

            alamtPengirimList = new List<string>();
            alamtPengirimList.AddRange(from item in _trnAlamatList
                                  where !string.IsNullOrWhiteSpace(TbAlamatPengirim.Text)
                                  let str = item.ToLower()
                                  where str.Contains(typedString.ToLower())
                                  select item);

            
            if (alamtPengirimList.Count > 0)
            {
                AlamatPlgLookup.ItemsSource = alamtPengirimList.Distinct(); ;
                AlamatPlgLookup.Visibility = Visibility.Visible;
            }
            else if (TbAlamatPengirim.Text.Equals(""))
            {
                AlamatPlgLookup.Visibility = Visibility.Collapsed;
                AlamatPlgLookup.ItemsSource = null;
            }
            else
            {
                AlamatPlgLookup.Visibility = Visibility.Collapsed;
                AlamatPlgLookup.ItemsSource = null;
            }
        }

        private void TbNamaPenerima_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isReloaded) return;
            var typedString = TbNamaPenerima.Text.Trim();
            
            _trnPnrList = BpdmhContext.GetTrnPenerimaan()
                .Select(a => a.NamaPenerima).ToList();

            penerimaList = new List<string>();
            penerimaList.AddRange(from item in _trnPnrList
                                where !string.IsNullOrWhiteSpace(TbNamaPenerima.Text)
                                let str = item.ToLower()
                                where str.Contains(typedString.ToLower())
                                select item);
            
            if (penerimaList.Count > 0)
            {
                PnrLookup.ItemsSource = penerimaList.Distinct(); ;
                PnrLookup.Visibility = Visibility.Visible;
            }
            else if (TbAlamatPenerima.Text.Equals(""))
            {
                PnrLookup.Visibility = Visibility.Collapsed;
                PnrLookup.ItemsSource = null;
            }
            else
            {
                PnrLookup.Visibility = Visibility.Collapsed;
                PnrLookup.ItemsSource = null;
            }
        }

        private void TbAlamatPenerima_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isReloaded) return;
            var typedString = TbAlamatPenerima.Text.Trim();
            
            _trnAlamatPnrList = BpdmhContext.GetTrnPenerimaan()
                .Select(a => a.AlamatPenerima).ToList();

            alamtPnrList = new List<string>();
            alamtPnrList.AddRange(from item in _trnAlamatPnrList
                                     where !string.IsNullOrWhiteSpace(TbAlamatPenerima.Text)
                                     let str = item.ToLower()
                                     where str.Contains(typedString.ToLower())
                                     select item);

            
            if (alamtPnrList.Count > 0)
            {
                AlamatPnrLookup.ItemsSource = alamtPnrList.Distinct();
                AlamatPnrLookup.Visibility = Visibility.Visible;
            }
            else if (TbAlamatPenerima.Text.Equals(""))
            {
                AlamatPnrLookup.Visibility = Visibility.Collapsed;
                AlamatPnrLookup.ItemsSource = null;
            }
            else
            {
                AlamatPnrLookup.Visibility = Visibility.Collapsed;
                AlamatPnrLookup.ItemsSource = null;
            }
        }

        private void PlgLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
//            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            if (e.Key == Key.Enter)
            {
                GetValueFromLookup();
            }
            PlgLookup.Visibility = Visibility.Collapsed;
            TbAlamatPengirim.Focus();
        }

        private void GetValueFromLookup()
        {
            if (PlgLookup.ItemsSource != null)
            {
                PlgLookup.Visibility = Visibility.Collapsed;
                TbNamaPengirim.Text = PlgLookup.SelectedIndex != -1 ? PlgLookup.SelectedItem.ToString() : pengirimList.FirstOrDefault();
            }
            PlgLookup.Visibility = Visibility.Collapsed;
        }

        private void PlgLookup_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetValueFromLookup();
            TbAlamatPengirim.Focus();
        }

        private void AlamatPlgLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
//            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            if(e.Key == Key.Enter)
            {
                GetValueFromAlamatLookup();
            }
            AlamatPlgLookup.Visibility = Visibility.Collapsed;
            CboCabang.Focus();
        }

        private void GetValueFromAlamatLookup()
        {
            if (AlamatPlgLookup.ItemsSource != null)
            {
                AlamatPlgLookup.Visibility = Visibility.Collapsed;
                TbAlamatPengirim.Text = AlamatPlgLookup.SelectedIndex != -1 ? AlamatPlgLookup.SelectedItem.ToString() : alamtPengirimList.FirstOrDefault();
            }
            AlamatPlgLookup.Visibility = Visibility.Collapsed;
        }

        private void AlamatPlgLookup_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetValueFromAlamatLookup();
            CboCabang.Focus();
        }

        private void PnrLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
//            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            if(e.Key == Key.Enter)
            {
                GetValueFromPnrLookup();
            }
            PnrLookup.Visibility = Visibility.Collapsed;
            TbAlamatPenerima.Focus();
        }

        private void GetValueFromPnrLookup()
        {
            if (PnrLookup.ItemsSource != null)
            {
                PnrLookup.Visibility = Visibility.Collapsed;
                TbNamaPenerima.Text = PnrLookup.SelectedIndex != -1 ? PnrLookup.SelectedItem.ToString() : penerimaList.FirstOrDefault();
            }
            PnrLookup.Visibility = Visibility.Collapsed;
        }

        private void PnrLookup_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetValueFromPnrLookup();
            TbAlamatPenerima.Focus();
        }

        private void AlamatPnrLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
//            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            if(e.Key == Key.Enter)
            {
                GetValueFromAlamatPnrLookup();
            }
            AlamatPnrLookup.Visibility = Visibility.Collapsed;
            CboKaryawan.Focus();
        }

        private void GetValueFromAlamatPnrLookup()
        {
            if (AlamatPnrLookup.ItemsSource != null)
            {
                AlamatPnrLookup.Visibility = Visibility.Collapsed;
                TbAlamatPenerima.Text = AlamatPnrLookup.SelectedIndex != -1 ? AlamatPnrLookup.SelectedItem.ToString() : alamtPnrList.FirstOrDefault();
            }
            AlamatPnrLookup.Visibility = Visibility.Collapsed;
        }

        private void AlamatPnrLookup_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetValueFromAlamatPnrLookup();
            CboKaryawan.Focus();
        }

        private void TbBiaya_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbBiayaPnr.Text) && !string.IsNullOrWhiteSpace(TbBiaya.Text))
                TbBiayaTotal.Text = (Convert.ToInt64(TbBiayaPnr.Text) + Convert.ToInt64(TbBiaya.Text)).ToString(CultureInfo.InvariantCulture);
        }

        private void TbBiayaPnr_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbBiaya.Text) && !string.IsNullOrWhiteSpace(TbBiayaPnr.Text))
                TbBiayaTotal.Text = (Convert.ToInt64(TbBiayaPnr.Text) + Convert.ToInt64(TbBiaya.Text)).ToString(CultureInfo.InvariantCulture);
        }

        private void TbBiayaTotal_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TbTerbilang.Text = new Terbilang(TbBiayaTotal.Text).Hasil();
        }

        private void TbNamaPengirim_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            TbAlamatPengirim.Focus();
            PlgLookup.Visibility = Visibility.Collapsed;
        }

        private void TbAlamatPengirim_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            CboCabang.Focus();
            AlamatPlgLookup.Visibility = Visibility.Collapsed;
        }

        private void TbNamaPenerima_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            TbAlamatPenerima.Focus();
            PnrLookup.Visibility = Visibility.Collapsed;
        }

        private void TbAlamatPenerima_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            CboKaryawan.Focus();
            AlamatPnrLookup.Visibility = Visibility.Collapsed;
        }

        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            _isReloaded = true;
            _masterViewSource = (CollectionViewSource)FindResource("MasterView");
            _detailViewSource = (CollectionViewSource)FindResource("DetailView");

            IQueryable<TrnPengirimanH> trnPengirimanHs = null;

            trnPengirimanHs = BpdmhContext.GetTable<TrnPengirimanH>()
                .Where(t => t.NoSeri == TbNoSp.Text.Trim() && t.TglInput == TglPicker.SelectedDate);

            _masterView.EditItem(trnPengirimanHs);
            DisableController(true);
            TbNoSp.IsEnabled = false;
        }

        private void DisableController(bool setEnable)
        {
            TbNoSp.IsEnabled = setEnable;
            TbNamaPengirim.IsEnabled = setEnable;
            TbAlamatPengirim.IsEnabled = setEnable;
            CboCabang.IsEnabled = setEnable;
            CboKendaraan.IsEnabled = setEnable;
            TglPicker.IsEnabled = setEnable;
            TbNamaPenerima.IsEnabled = setEnable;
            TbAlamatPenerima.IsEnabled = setEnable;
            CboKaryawan.IsEnabled = setEnable;
            BtnAddDetail.IsEnabled = setEnable;
            ListViewTransaksi.IsEnabled = setEnable;
            CboPembayaran.IsEnabled = setEnable;
            TbBiayaPnr.IsEnabled = setEnable;
            TbBiaya.IsEnabled = setEnable;
        }
    }
}
