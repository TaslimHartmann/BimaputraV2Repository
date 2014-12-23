using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
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
using Technewlogic.Samples.WpfModalDialog;

namespace BPDMH.Transaksi
{
    /// <summary>
    /// Interaction logic for TrnPengiriman.xaml
    /// </summary>
    public partial class TrnPengiriman
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
        private BindingListCollectionView _detailView;
        //        private bool isReloaded;
        private int _errors;
        private CollectionViewSource _penerimaSource;

        public CollectionViewSource PenerimaSource
        {
            get { return _penerimaSource; }
            set { _penerimaSource = value; }
        }

        public TransactionH TransactionH { get; set; }

        private List<string> _mstPngList;
        private List<string> _trnPngList;
        private List<string> _mstAlamatList;
        private List<string> _trnAlamatList;

        private List<string> _mstPnrList;
        private List<string> _trnPnrList;
        private List<string> _mstAlamatPnrList;
        private List<string> _trnAlamatPnrList;

        private bool _isReloaded;
        private List<string> _altPngCombinedList;
        private List<string> _pngCombinedList;
        private List<string> _altPnrCombinedList;
        private List<string> _pnrCombinedList;
        private List<string> _cabangList;
        private bool isNewData;
        private ModalDialog modal;

        public TrnPengiriman()
        {
            InitializeComponent();
            modal = new ModalDialog();
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

            //GetMasterData();
            _isReloaded = true;

        }

        private void GetMasterData()
        {
            _masterViewSource = (CollectionViewSource)FindResource("MasterView");
            _detailViewSource = (CollectionViewSource)FindResource("DetailView");

            _masterViewSource.Source = BpdmhContext.GetTable<TrnPengirimanH>();
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
            //            DataContext = cabangList;
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
            var pelanggan = new MstPelanggan(this) { IsPengirim = true };
            if (!String.IsNullOrEmpty(CboPenerima.Text))
            {
                pelanggan.KdPenerima = CboPenerima.Text;
            }
            Close();
            pelanggan.Show();
        }

        private void BtnCrPenerima_Click(object sender, RoutedEventArgs e)
        {
            var pelanggan = new MstPelanggan(this) { IsPenerima = true };
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
            isNewData = true;
            _masterView.AddNew();
            TbBiayaTotal.Clear();
            BtnPrevious.IsEnabled = true;
            BtnNext.IsEnabled = true;
        }

        private void DeleteRecord()
        {
            if (_masterView.CurrentPosition <= -1)
                return;
            var order = (TrnPengirimanH)_masterView.CurrentItem;
            foreach (var detail in order.TrnPengirimanDs)
            {
                BpdmhContext.TrnPengirimanDs.DeleteOnSubmit(detail);
            }
            BpdmhContext.TrnPengirimanHs.DeleteOnSubmit(order);
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
            _detailView.AddNew();
            _detailView.CommitNew();
        }

        private void SaveRecord()
        {
            var valid = BpdmhContext.GetTransaksiH()
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
            DisableController(false);
            _isReloaded = true;
            GoToPrevious();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            DisableController(false);
            NextRecord();
            _isReloaded = !_masterView.IsCurrentAfterLast;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            DisableController(true);
            AddRecord();
            //            ClearController();
            //            TglPicker.Text = DateTime.Now.ToShortDateString();
            TglPicker.SelectedDate = DateTime.Today;
            TbNoSp.Focus();
            isNewData = true;
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
            //            ClearController();
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
            MyPopup.IsOpen = false;
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

            isNewData = false;
            _isReloaded = true;
            _masterViewSource = (CollectionViewSource)FindResource("MasterView");
            _detailViewSource = (CollectionViewSource)FindResource("DetailView");

            IQueryable<TrnPengirimanH> trnPengirimanHs = null;
            if (nosp != null && tgl != null)
            {
                trnPengirimanHs = BpdmhContext.GetTable<TrnPengirimanH>()
                    .Where(t => t.NoSeri == nosp && t.TglInput == tgl);
            }
            else if (nosp != null && tgl == null)
            {
                trnPengirimanHs = BpdmhContext.GetTable<TrnPengirimanH>()
                    .Where(t => t.NoSeri == nosp);
            }
            else if (nosp == null && tgl != null)
            {
                trnPengirimanHs = BpdmhContext.GetTable<TrnPengirimanH>()
                    .Where(t => t.TglInput == tgl);
            }
            else if (nosp == null && tgl == null)
            {
                return;
            }

            if (trnPengirimanHs != null && trnPengirimanHs.Any())
            {
                //_masterView.EditItem(trnPengirimanHs);

                _masterViewSource.Source = trnPengirimanHs;
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

        private void PlgLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
            //if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            if (e.Key == Key.Enter)
            {
                GetValueFromLookup();
            }
            //if (e.Key == Key.Tab)
            //{
            PlgLookup.Visibility = Visibility.Collapsed;
            //}
            TbAlamatPengirim.Focus();
        }

        private void GetValueFromLookup()
        {
            if (PlgLookup.ItemsSource != null)
            {
                PlgLookup.Visibility = Visibility.Collapsed;
                TbNamaPengirim.Text = PlgLookup.SelectedIndex != -1 ? PlgLookup.SelectedItem.ToString() : _pngCombinedList.FirstOrDefault();
            }
            PlgLookup.Visibility = Visibility.Collapsed;
        }

        private void TbNamaPengirim_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isReloaded) return;
            var typedString = TbNamaPengirim.Text.Trim();
            _mstPngList = BpdmhContext.GetTable<Pelanggan>()
                .Where(b => b.IsPengirim == true)
                .Select(a => a.NamaPlg).ToList();

            _trnPngList = BpdmhContext.GetTransaksiH()
                .Select(a => a.NamaPengirim).ToList();

            var autoList = new List<string>();
            autoList.Clear();

            autoList.AddRange(from item in _mstPngList
                              where !string.IsNullOrWhiteSpace(TbNamaPengirim.Text)
                              let str = item.ToLower()
                              where str.Contains(typedString.ToLower())
                              select item);

            var secondList = new List<string>();

            secondList.AddRange(from item in _trnPngList
                                where !string.IsNullOrWhiteSpace(TbNamaPengirim.Text)
                                let str = item.ToLower()
                                where str.Contains(typedString.ToLower())
                                select item);

            _pngCombinedList = autoList.Union(secondList).ToList();

            //            foreach (string item in _trnPngList)
            //            {
            //                if (!string.IsNullOrEmpty(TbNama.Text))
            //                {
            //                    string str = item.ToLower();
            //                    if (str.Contains(typedString.ToLower()))
            //                    {
            //                        autoList.Add(item);
            //                    }
            //                }
            //            }
            var uniquePengirim = _pngCombinedList.Distinct();
            if (_pngCombinedList.Count > 0)
            {
                PlgLookup.ItemsSource = uniquePengirim;
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

        private void PlgLookup_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetValueFromLookup();
            TbAlamatPengirim.Focus();
        }

        private void AlamatPlgLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
            //            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            if (e.Key == Key.Enter)
            {
                GetValueFromAlamatLookup();
            }
            // if (e.Key == Key.Tab)
            //{
            AlamatPlgLookup.Visibility = Visibility.Collapsed;
            //}
            CboCabang.Focus();
        }

        private void AlamatPlgLookup_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetValueFromAlamatLookup();
            CboCabang.Focus();
        }

        private void GetValueFromAlamatLookup()
        {
            if (AlamatPlgLookup.ItemsSource != null)
            {
                AlamatPlgLookup.Visibility = Visibility.Collapsed;
                TbAlamatPengirim.Text = AlamatPlgLookup.SelectedIndex != -1 ? AlamatPlgLookup.SelectedItem.ToString() : _altPngCombinedList.FirstOrDefault();
            }
            AlamatPlgLookup.Visibility = Visibility.Collapsed;
        }

        private void TbAlamatPengirim_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isReloaded) return;
            var typedString = TbAlamatPengirim.Text.Trim();
            _mstAlamatList = BpdmhContext.GetTable<Pelanggan>()
                .Select(a => a.Alamat).ToList();

            _trnAlamatList = BpdmhContext.GetTransaksiH()
                .Select(a => a.AlamatPengirim).ToList();

            var mstAlamatList = new List<string>();
            mstAlamatList.Clear();

            mstAlamatList.AddRange(from item in _mstAlamatList
                                   where !string.IsNullOrWhiteSpace(TbAlamatPengirim.Text)
                                   let str = item.ToLower()
                                   where str.Contains(typedString.ToLower())
                                   select item);

            var trnAlamtList = new List<string>();
            trnAlamtList.AddRange(from item in _trnAlamatList
                                  where !string.IsNullOrWhiteSpace(TbAlamatPengirim.Text)
                                  let str = item.ToLower()
                                  where str.Contains(typedString.ToLower())
                                  select item);

            _altPngCombinedList = mstAlamatList.Union(trnAlamtList).ToList();

            //            if (combinedList.Count == 1)
            //            {
            //                TbAlamatPengirim.Text = combinedList[0];
            //                AlamatPlgLookup.Visibility = Visibility.Visible;
            //            }
            var uniqueAlamatPenerima = _altPngCombinedList.Distinct();
            if (_altPngCombinedList.Count > 0)
            {
                AlamatPlgLookup.ItemsSource = uniqueAlamatPenerima;
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

        private void PnrLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
            //            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            if (e.Key == Key.Enter)
            {
                GetValueFromPnrLookup();
            }
            PnrLookup.Visibility = Visibility.Collapsed;
            TbAlamatPenerima.Focus();
        }

        private void PnrLookup_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetValueFromPnrLookup();
            TbAlamatPenerima.Focus();
        }

        private void GetValueFromPnrLookup()
        {
            if (PnrLookup.ItemsSource != null)
            {
                PnrLookup.Visibility = Visibility.Collapsed;
                TbNamaPenerima.Text = PnrLookup.SelectedIndex != -1 ? PnrLookup.SelectedItem.ToString() : _pnrCombinedList.FirstOrDefault();
            }
            PnrLookup.Visibility = Visibility.Collapsed;
        }

        private void TbNamaPenerima_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isReloaded) return;
            var typedString = TbNamaPenerima.Text.Trim();
            _mstPnrList = BpdmhContext.GetTable<Pelanggan>()
                .Where(b => b.IsPengirim == false)
                .Select(a => a.NamaPlg).ToList();

            _trnPnrList = BpdmhContext.GetTransaksiH()
                .Select(a => a.NamaPenerima).ToList();

            var mstPnrList = new List<string>();
            mstPnrList.Clear();

            mstPnrList.AddRange(from item in _mstPnrList
                                where !string.IsNullOrWhiteSpace(TbNamaPenerima.Text)
                                let str = item.ToLower()
                                where str.Contains(typedString.ToLower())
                                select item);

            var trnPnrList = new List<string>();
            trnPnrList.AddRange(from item in _trnPnrList
                                where !string.IsNullOrWhiteSpace(TbNamaPenerima.Text)
                                let str = item.ToLower()
                                where str.Contains(typedString.ToLower())
                                select item);

            _pnrCombinedList = mstPnrList.Union(trnPnrList).ToList();
            var uniquePnrList = _pnrCombinedList.Distinct();

            if (_pnrCombinedList.Count > 0)
            {
                PnrLookup.ItemsSource = uniquePnrList;
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

        private void AlamatPnrLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
            //            if ((e.Key == Key.Enter) || (e.Key == Key.Tab))
            if (e.Key == Key.Enter)
            {
                GetValueFromAlamatPnrLookup();
            }
            AlamatPnrLookup.Visibility = Visibility.Collapsed;
            CboKaryawan.Focus();
        }

        private void AlamatPnrLookup_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetValueFromAlamatPnrLookup();
            CboKaryawan.Focus();
        }

        private void GetValueFromAlamatPnrLookup()
        {
            if (AlamatPnrLookup.ItemsSource != null)
            {
                AlamatPnrLookup.Visibility = Visibility.Collapsed;
                TbAlamatPenerima.Text = AlamatPnrLookup.SelectedIndex != -1 ? AlamatPnrLookup.SelectedItem.ToString() : _altPnrCombinedList.FirstOrDefault();
            }
            AlamatPnrLookup.Visibility = Visibility.Collapsed;
        }

        private void TbAlamatPenerima_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isReloaded) return;
            var typedString = TbAlamatPenerima.Text.Trim();
            _mstAlamatPnrList = BpdmhContext.GetTable<Pelanggan>()
                .Where(b => b.IsPengirim == false)
                .Select(a => a.Alamat).ToList();

            _trnAlamatPnrList = BpdmhContext.GetTransaksiH()
                .Select(a => a.AlamatPenerima).ToList();

            var mstAlamatPnrList = new List<string>();
            mstAlamatPnrList.Clear();

            mstAlamatPnrList.AddRange(from item in _mstAlamatPnrList
                                      where !string.IsNullOrWhiteSpace(TbAlamatPenerima.Text)
                                      let str = item.ToLower()
                                      where str.Contains(typedString.ToLower())
                                      select item);

            var trnAlamtPnrList = new List<string>();
            trnAlamtPnrList.AddRange(from item in _trnAlamatPnrList
                                     where !string.IsNullOrWhiteSpace(TbAlamatPenerima.Text)
                                     let str = item.ToLower()
                                     where str.Contains(typedString.ToLower())
                                     select item);

            _altPnrCombinedList = mstAlamatPnrList.Union(trnAlamtPnrList).ToList();

            if (_altPnrCombinedList.Count > 0)
            {
                AlamatPnrLookup.ItemsSource = _altPnrCombinedList;
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

        private void ClearController()
        {
            TbNoSp.Clear();
            TbNamaPengirim.Clear();
            TbAlamatPengirim.Clear();
            GetCabangLookup();
            GetKaryawanLookup();
            GetKendaraanLookup();
            TglPicker.DisplayDate = DateTime.Today;
            TbNamaPenerima.Clear();
            TbAlamatPenerima.Clear();
            TbNoSp.Focus();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            if (isNewData)
                _masterView.CancelNew();
            else
            {
                var abc = _masterView.IsEditingItem;
                _masterView.CancelEdit();
            }

            //_masterView.MoveCurrentTo(_masterView.CurrentEditItem);

        }

        private void BtnCari_OnClick(object sender, RoutedEventArgs e)
        {
            MyPopup.IsOpen = false;
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
    }
}