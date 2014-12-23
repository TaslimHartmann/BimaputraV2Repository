using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BPDMH.DataSet;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MstCabang.xaml
    /// </summary>
    public partial class MstCabang
    {
        public MstCabang()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbId.Focus();
            InitilizeListView();
        }

        private void InitilizeListView()
        {
            var bpdmhContext = new DCBPDMHDataContext();
            var q = bpdmhContext.GetTable<Cabang>()
                .OrderBy(a => a.NmCabang)
                .Select(a => a).ToList();
            ListViewCabang.ItemsSource = q;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var cbng = new Cabang
                {
                    CabangId = TbId.Text,
                    NmCabang = TbNama.Text,
                    Telp = TbTelpon.Text,
                    Fax = TbFax.Text,
                    KtPerson = TbKtPerson.Text,
                    Alamat = TbAlamat.Text
                };
                var isOk = DCBPDMHDataContext.InsertCabang(cbng);
                if (!isOk) return;
                InitilizeListView();
                RestartViews();
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
            TbFax.Clear();
            TbKtPerson.Clear();
            TbAlamat.Clear();
        }

        private void BtnHapus_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var cbng = new Cabang {CabangId = TbId.Text};
                 var result = MessageBox.Show("Data akan dihapus?", "Peringatan", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        DCBPDMHDataContext.DeleteCabang(cbng);
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

        private void ListViewCabang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbng = (Cabang)ListViewCabang.SelectedItem;
            if (cbng == null) return;
            TbId.IsEnabled = false;
            TbId.Text = cbng.CabangId;
            TbNama.Text = cbng.NmCabang;
            TbTelpon.Text = cbng.Telp;
            TbFax.Text = cbng.Fax;
            TbKtPerson.Text = cbng.KtPerson;
            TbAlamat.Text = cbng.Alamat;
            ToogleBtnSimpan();
        }

        private void ToogleBtnSimpan()
        {
            BtnSimpan.Content = !string.IsNullOrWhiteSpace(TbId.Text) ? "Update" : "Simpan";
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
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
    }
}
