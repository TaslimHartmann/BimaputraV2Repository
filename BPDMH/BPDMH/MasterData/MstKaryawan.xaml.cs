using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BPDMH.DataSet;
using BPDMH.Model;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MstKaryawan.xaml
    /// </summary>
    public partial class MstKaryawan
    {
        public MstKaryawan()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbId.Focus();
            InitilizeListView();
            GetStatusLookup();
        }

        private void GetStatusLookup()
        {
            var statusList = new List<string> { "Operator", "Administratior"};
            CboStatus.ItemsSource = statusList;
            CboStatus.SelectedIndex = 0;
        }

        private void InitilizeListView()
        {
            var bpdmhContext = new DCBPDMHDataContext();
            var q = bpdmhContext.GetTable<Karyawan>()
                .OrderBy(a => a.Nama)
                .Select(a => new {a.KaryawanId, a.Nama, 
                a.Jabatan, a.Telpon, a.Alamat, a.Password,
                Status = a.Status == 0 ? "Operator" : "Administrator"
            }).ToList();
            
            ListViewKaryawan.ItemsSource = q;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var kry = new Karyawan
                {
                    KaryawanId = TbId.Text.Trim(),
                    Nama = TbNama.Text.Trim(),
                    Jabatan = TbJabatan.Text.Trim(),
                    Telpon = TbTelpon.Text.Trim(),
                    Alamat = TbAlamat.Text.Trim(),
                    Password = TbPassword.Text.Trim(),
                    Status = CboStatus.SelectedIndex
                };
                var isOk = DCBPDMHDataContext.InsertKaryawan(kry);
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
            TbJabatan.Clear();
            TbTelpon.Clear();
            TbAlamat.Clear();
            TbPassword.Clear();
            CboStatus.SelectedIndex = 0;
        }

        private void BtnHapus_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var kry = new Karyawan {KaryawanId = TbId.Text};
                var result = MessageBox.Show("Data akan dihapus?", "Peringatan", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        DCBPDMHDataContext.DeleteKaryawan(kry);
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

        private void ListViewKaryawan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic kry = ListViewKaryawan.SelectedItem;
            if (kry == null) return;

           
//            var kry = (Karyawan)ListViewKaryawan.SelectedItem;
//
//            if (kry == null) return;
            TbId.IsEnabled = false;
            TbId.Text = kry.KaryawanId;
            TbNama.Text = kry.Nama;
            TbJabatan.Text = kry.Jabatan;
            TbTelpon.Text = kry.Telpon;
            TbAlamat.Text = kry.Alamat;
            TbPassword.Text = kry.Password;
            CboStatus.SelectedIndex = kry.Status == "Operator" ? 0 : 1;
            
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
