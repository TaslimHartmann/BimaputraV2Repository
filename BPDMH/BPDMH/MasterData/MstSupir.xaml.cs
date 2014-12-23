using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BPDMH.DataSet;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MstSupir.xaml
    /// </summary>
    public partial class MstSupir
    {
        public MstSupir()
        {
            InitializeComponent();
        }

        private void MstSupir_OnLoaded(object sender, RoutedEventArgs e)
        {
            TbId.Focus();
            InitilizeListView();
        }

        private void InitilizeListView()
        {
            var bpdmhContext = new DCBPDMHDataContext();
            var q = bpdmhContext.GetTable<Supir>()
                .OrderBy(a => a.NamaSupir)
                .Select(a => a).ToList();
            ListViewSupir.ItemsSource = q;
        }

        private void ClearTb()
        {
            TbId.Clear();
            TbNama.Clear();
            TbTelpon.Clear();
            TbAlamat.Clear();
        }

        private void BtnBaru_OnClick(object sender, RoutedEventArgs e)
        {
            RestartViews();
        }

        private void BtnSimpan_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var supir = new Supir
                {
                    SupirId = TbId.Text,
                    NamaSupir = TbNama.Text,
                    Telpon = TbTelpon.Text,
                    AlamatSupir = TbAlamat.Text
                };
                var isOk = DCBPDMHDataContext.InsertSupir(supir);
                if (!isOk) return;
                InitilizeListView();
                RestartViews();
            }
            else
            {
                MessageBox.Show("Data tidak boleh kosong", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RestartViews()
        {
            ClearTb();
            TbId.IsEnabled = true;
            ToogleBtnSimpan();
            TbId.Focus();
        }

        private void ToogleBtnSimpan()
        {
            BtnSimpan.Content = !string.IsNullOrWhiteSpace(TbId.Text) ? "Update" : "Simpan";
        }

        private void BtnHapus_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var supir = new Supir { SupirId = TbId.Text };
                var result = MessageBox.Show("Data akan dihapus?", "Peringatan", MessageBoxButton.YesNo,
                   MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        DCBPDMHDataContext.DeleteSupir(supir);
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

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListViewSupir_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var supir = (Supir)ListViewSupir.SelectedItem;
            if (supir == null) return;
            TbId.IsEnabled = false;
            TbId.Text = supir.SupirId;
            TbNama.Text = supir.NamaSupir;
            TbTelpon.Text = supir.Telpon;
            TbAlamat.Text = supir.AlamatSupir;
            ToogleBtnSimpan();
        }
    }
}
