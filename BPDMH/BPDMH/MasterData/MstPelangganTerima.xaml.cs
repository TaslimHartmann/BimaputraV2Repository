using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BPDMH.DataSet;
using BPDMH.Tools;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MstPelangganTerima.xaml
    /// </summary>
    public partial class MstPelangganTerima : Window
    {
        private Category _category;
        public static bool BooleanTrue = true;
        public static bool BooleanFalse = false;
        public bool IsPengirim { get; set; }
        public string KdPengirim { get; set; }
        public string KdPenerima { get; set; }
        public bool IsPenerima { get; set; }

        private BPDMH.Transaksi.TrnPenerimaan _trnPenerimaan;
        private bool _isShouldBack;

        public MstPelangganTerima()
        {
            InitializeComponent();
            _category = new Category { BooleanProperty = true, EnumProperty = CategoryEnum.Penerima };
            DataContext = _category;
            _isShouldBack = false;
        }


        public MstPelangganTerima(BPDMH.Transaksi.TrnPenerimaan trnPenerimaan)
        {
            InitializeComponent();
            _category = new Category { BooleanProperty = true, EnumProperty = CategoryEnum.Penerima };
            DataContext = _category;
            _trnPenerimaan = trnPenerimaan;
            _isShouldBack = true;
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
                    NamaPlg = TbNama.Text.Trim(),
                    Telp = TbTelpon.Text.Trim(),
                    KtPerson = TbKtPerson.Text.Trim(),
                    Alamat = TbAlamat.Text.Trim(),
                    IsPengirim = RbConverter()
                };
                var isOk = DCBPDMHDataContext.InsertPelanggan(plg);
                if (!isOk) return;
                ClearTb();
                InitilizeListView();
                TbId.Focus();
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
                        ClearTb();
                        TbId.IsEnabled = true;
                        TbId.Focus();
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
            TbNama.Clear();
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

        private void ToogleBtnSimpan()
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
                BtnSimpan.Content = "Update";
        }

        private bool? RbConverter()
        {
            return RbPengirim.IsChecked;
        }

        private DCBPDMHDataContext _bpdmhContext = new DCBPDMHDataContext();
        

        public BPDMH.Transaksi.TrnPengiriman TrnPengiriman { get; set; }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListViewPelanggan_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                    .Select(a => new {PengirimId = a.PelangganId, a.NamaPlg, a.Alamat}).ToList();
                _trnPenerimaan.CustList.Source = pengirimList;
            }
            else
            {
                var penerimaList = _bpdmhContext.GetTable<Pelanggan>()
                    .Where(a => a.PelangganId == plgId)
                    .Select(a => new {PenerimaId = a.PelangganId, a.NamaPlg, a.Alamat}).ToList();
                _trnPenerimaan.PenerimaSource.Source = penerimaList;
            }
            _trnPenerimaan.Show();
            Close();
        }

        private void ListViewPelanggan_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ReturnGetValue();
            }
        }

        private void BtnBaru_OnClick(object sender, RoutedEventArgs e)
        {
            ClearTb();
            TbId.Focus();
        }
    }
}
