using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BPDMH.DataSet;
using BPDMH.Tools;
using MessageBox = System.Windows.MessageBox;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MstPelanggan.xaml
    /// </summary>
    public partial class MstPelanggan
    {
        private Category _category;
        public static bool BooleanTrue = true;
        public static bool BooleanFalse = false;
        public bool IsPengirim { get; set; }
        public string KdPengirim { get; set; }
        public string KdPenerima { get; set; }
        public bool IsPenerima { get; set; }

        private readonly BPDMH.Transaksi.TrnPengiriman _trnPengiriman;
        private readonly bool _isShouldBack;
        private readonly DCBPDMHDataContext _bpdmhContext = new DCBPDMHDataContext();
        private List<string> _nameList;
        private List<string> daftarList;

        public MstPelanggan()
        {
            InitializeComponent();
            _category = new Category { BooleanProperty = true, EnumProperty = CategoryEnum.Penerima };
            DataContext = _category;
            _isShouldBack = false;

            var bpdmhContext = new DCBPDMHDataContext();
            _nameList = bpdmhContext.GetTH()
                .Select(a => a.NamaPenerima).ToList();

            daftarList = bpdmhContext.GetTable<Pelanggan>()
                .OrderBy(a => a.PelangganId)
                .Select(a => a.NamaPlg).ToList();
        }


        public MstPelanggan(BPDMH.Transaksi.TrnPengiriman trnPengiriman)
        {
            InitializeComponent();
            _category = new Category { BooleanProperty = true, EnumProperty = CategoryEnum.Penerima };
            DataContext = _category;
            _trnPengiriman = trnPengiriman;
            _isShouldBack = true;
        }

        private void ToogleBtnSimpan()
        {
            BtnSimpan.Content = !string.IsNullOrWhiteSpace(TbId.Text) ? "Update" : "Simpan";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbId.Focus();
            InitilizeListView();
        }

        private void InitilizeListView()
        {
            var bpdmhContext = new DCBPDMHDataContext();
            GetValue(bpdmhContext);
        }

        private void GetValue(DCBPDMHDataContext bpdmhContext)
        {
            var q = bpdmhContext.GetTable<Pelanggan>()
                .OrderBy(a => a.PelangganId)
                    .Select(
                        a =>
                            new
                            {
                                a.PelangganId,
                                a.NamaPlg,
                                a.Telp,
                                a.KtPerson,
                                Kategori = a.IsPengirim == true ? "Pengirim" : "Penerima",
                                a.IsPengirim,
                                a.Alamat
                            }).ToList();

            if (IsPengirim)
            {
                q = bpdmhContext.GetTable<Pelanggan>()
                    .OrderBy(a => a.PelangganId)
                    .Where(a => a.IsPengirim == true)
                    .Select(
                        a =>
                            new
                            {
                                a.PelangganId,
                                a.NamaPlg,
                                a.Telp,
                                a.KtPerson,
                                Kategori = a.IsPengirim == true ? "Pengirim" : "Penerima",
                                a.IsPengirim,
                                a.Alamat
                            }).ToList();
                ListViewPelanggan.ItemsSource = q;
            }
            else if (IsPenerima)
            {
                q = bpdmhContext.GetTable<Pelanggan>()
                    .Where(a => a.IsPengirim == false)
                    .Select(
                        a =>
                            new
                            {
                                a.PelangganId,
                                a.NamaPlg,
                                a.Telp,
                                a.KtPerson,
                                Kategori = a.IsPengirim == true ? "Pengirim" : "Penerima",
                                a.IsPengirim,
                                a.Alamat
                            }).ToList();
            }
            ListViewPelanggan.ItemsSource = q;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var plg = new Pelanggan
                {
                    PelangganId = TbId.Text.Trim(),
                    NamaPlg = TbNama.Text.TrimEnd(),
                    Telp = TbTelpon.Text.Trim(),
                    KtPerson = TbKtPerson.Text.Trim(),
                    Alamat = TbAlamat.Text.Trim(),
                    IsPengirim = RbConverter()
                };
                var isOk = DCBPDMHDataContext.InsertPelanggan(plg);
                if (!isOk) return;
                InitilizeListView();
                RestartViews();
            }
            else
            {
                MessageBox.Show("Data tidak boleh kosong", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnHapus_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var plg = new Pelanggan { PelangganId = TbId.Text };
                var result = MessageBox.Show("Data akan dihapus?", "Peringatan", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        DCBPDMHDataContext.DeletePelanggan(plg);
                        RestartViews();
                        break;
                }
                InitilizeListView();
            }
            else
            {
                MessageBox.Show("Data tidak boleh kosong", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearTb()
        {
            TbId.Clear();
            TbNama.Text = "";
            TbTelpon.Clear();
            TbKtPerson.Clear();
            TbAlamat.Clear();
            RbPengirim.IsChecked = true;
        }

        private void ListViewPelanggan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic plg = ListViewPelanggan.SelectedItem;
            if (plg == null) return;
            TbId.IsEnabled = false;
            TbId.Text = plg.PelangganId;
            TbNama.Text = plg.NamaPlg;
            TbTelpon.Text = plg.Telp;
            TbKtPerson.Text = plg.KtPerson;
            TbAlamat.Text = plg.Alamat;
            //            RbPengirim.IsChecked = false;//RBConverter(plg.Kategori);
            //            RbPenerima.IsChecked = true;//RBConverter(plg.Kategori);
            _category = new Category { BooleanProperty = plg.IsPengirim, EnumProperty = CategoryEnum.Pengirim };
            DataContext = _category;
            ToogleBtnSimpan();
        }

        private bool? RbConverter()
        {
            return RbPengirim.IsChecked;
        }

        public BPDMH.Transaksi.TrnPengiriman TrnPengiriman { get; set; }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListViewPelanggan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ReturnGetValue();
        }

        private void ReturnGetValue()
        {
            if (!_isShouldBack) return;
            var plgId = TbId.Text;
            if (IsPengirim)
            {
                var pengirimList = _bpdmhContext.GetTable<Pelanggan>()
                    .Where(a => a.PelangganId == plgId)
                    .Select(a => new { PengirimId = a.PelangganId, a.NamaPlg, a.Alamat }).ToList();
                _trnPengiriman.CustList.Source = pengirimList;
            }
            else
            {
                var penerimaList = _bpdmhContext.GetTable<Pelanggan>()
                    .Where(a => a.PelangganId == plgId)
                    .Select(a => new { PenerimaId = a.PelangganId, a.NamaPlg, a.Alamat }).ToList();
                _trnPengiriman.PenerimaSource.Source = penerimaList;
            }
            _trnPengiriman.Show();
            Close();
        }

        private void ListViewPelanggan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ReturnGetValue();
            }
        }

        private void BtnBaru_OnClick(object sender, RoutedEventArgs e)
        {
            RestartViews();
        }

        private void RestartViews()
        {
            ClearTb();
            TbId.IsEnabled = true;
            ToogleBtnSimpan();
            TbId.Focus();
        }

        //        private void TbNama_TextChanged(object sender, TextChangedEventArgs e)
        //        {
        //            var typedString = TbNama.Text.Trim();
        //            var autoList = new List<string>();
        //            autoList.Clear();
        //
        //            autoList.AddRange(from item in daftarList
        //                              where !string.IsNullOrWhiteSpace(TbNama.Text)
        //                              let str = item.ToLower()
        //                              where str.Contains(typedString.ToLower())
        //                              select item);
        //
        //            var secondList = new List<string>();
        //
        //            secondList.AddRange(from item in _nameList
        //                                where !string.IsNullOrWhiteSpace(TbNama.Text)
        //                                let str = item.ToLower()
        //                                where str.Contains(typedString.ToLower())
        //                                select item);
        //
        //            var combinedList = autoList.Union(secondList).ToList();
        //
        //            //            foreach (string item in _nameList)
        //            //            {
        //            //                if (!string.IsNullOrEmpty(TbNama.Text))
        //            //                {
        //            //                    string str = item.ToLower();
        //            //                    if (str.Contains(typedString.ToLower()))
        //            //                    {
        //            //                        autoList.Add(item);
        //            //                    }
        //            //                }
        //            //            }
        //
        //            if (combinedList.Count > 0)
        //            {
        //                PlgLookup.ItemsSource = combinedList;
        //                PlgLookup.Visibility = Visibility.Visible;
        //            }
        //            else if (TbNama.Text.Equals(""))
        //            {
        //                PlgLookup.Visibility = Visibility.Collapsed;
        //                PlgLookup.ItemsSource = null;
        //            }
        //            else
        //            {
        //                PlgLookup.Visibility = Visibility.Collapsed;
        //                PlgLookup.ItemsSource = null;
        //            }
        //        }

        private void ListBox1_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void PlgLookup_OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter))
            {
                if (PlgLookup.ItemsSource != null)
                {
                    PlgLookup.Visibility = Visibility.Collapsed;
                    if (PlgLookup.SelectedIndex != -1)
                    {
                        TbNama.Text = PlgLookup.SelectedItem.ToString();
                    }
                }
                PlgLookup.Visibility = Visibility.Collapsed;
            }
            TbTelpon.Focus();
        }
    }
}
