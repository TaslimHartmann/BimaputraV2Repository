using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BPDMH.DataSet;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MstKendaraan.xaml
    /// </summary>
    public partial class MstKendaraan
    {
        public MstKendaraan()
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
            var q = bpdmhContext.GetTable<Kendaraan>()
                .OrderBy(a => a.KendaraanId)
                .Select(a => a);
            ListViewKendaraan.ItemsSource = q;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var knd = new Kendaraan
                {
                    KendaraanId = TbId.Text.Trim(),
                    NoPolisi = TbNoPolisi.Text.Trim(),
                    Jenis = TbJenis.Text.Trim(),
                    Keterangan = TbKet.Text.Trim()
                };
                var isOk = DCBPDMHDataContext.InsertKendaraan(knd);
                if (!isOk) return;
                RestartViews();
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
            TbNoPolisi.Clear();
            TbJenis.Clear();
            TbKet.Clear();
        }

        private void BtnHapus_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var knd = new Kendaraan {KendaraanId = TbId.Text};
                var result = MessageBox.Show("Data akan dihapus?", "Peringatan", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        DCBPDMHDataContext.DeleteKendaraan(knd);
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

        private void ListViewKendaraan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var knd = (Kendaraan)ListViewKendaraan.SelectedItem;
            if (knd == null) return;
            TbId.IsEnabled = false;
            TbId.Text = knd.KendaraanId;
            TbNoPolisi.Text = knd.NoPolisi;
            TbJenis.Text = knd.Jenis;
            TbKet.Text = knd.Keterangan;
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
