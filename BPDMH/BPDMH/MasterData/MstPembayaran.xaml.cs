using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BPDMH.DataSet;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MstPembayaran.xaml
    /// </summary>
    public partial class MstPembayaran
    {
        public MstPembayaran()
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
            var q = bpdmhContext.GetTable<Pembayaran>()
                .OrderBy(a => a.Keterangan)
                .Select(a => a);
            ListViewPembayaran.ItemsSource = q;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var pby = new Pembayaran { PembayaranId = TbId.Text.Trim(), Keterangan = TbKet.Text.Trim() };
                var isOk = DCBPDMHDataContext.InsertPembayaran(pby);
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
            TbKet.Clear();
        }

        private void BtnHapus_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var pby = new Pembayaran{ PembayaranId = TbId.Text };
                var result = MessageBox.Show("Data akan dihapus?", "Peringatan", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        DCBPDMHDataContext.DeletePembayaran(pby);
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

        private void ListViewPembayaran_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pby = (Pembayaran)ListViewPembayaran.SelectedItem;
            if (pby == null) return;
            TbId.IsEnabled = false;
            TbId.Text = pby.PembayaranId;
            TbKet.Text = pby.Keterangan;
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
