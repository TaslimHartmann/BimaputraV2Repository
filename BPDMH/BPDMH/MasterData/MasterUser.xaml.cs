using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BPDMH.DataSet;
using BPDMH.Model;
using Microsoft.Build.Framework.XamlTypes;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MasterUser.xaml
    /// </summary>
    public partial class MasterUser : Window
    {
        public MasterUser()
        {
            TbKdUser.Focus();
            InitializeComponent();
        }

        private void InitiateListview()
        {
            var bpdmhContext = new DCBPDMHDataContext();
            var q = bpdmhContext.GetTable<Karyawan>().Select(a => a);
            ListView1.ItemsSource = q;
        }

        private void BtnUserSimpan_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbKdUser.Text))
            {
                var user = new Karyawan {KaryawanId = TbKdUser.Text, Nama = TbNamaUser.Text, Password = TbPassword.Text};
                bool isOk = DCBPDMHDataContext.InsertKaryawan(user);
                if (isOk)
                {
                    ClearEt();
                    InitiateListview();
                    MessageBox.Show("Ok", "Berhasil");
                }
            }
            else
            {
                MessageBox.Show("Data tidak boleh kosong", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        
        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var user = (Karyawan)ListView1.SelectedItem;
            if (user == null) return;
            TbKdUser.IsEnabled = false;
            TbKdUser.Text = user.KaryawanId;
            TbNamaUser.Text = user.Nama;
            TbPassword.Text = user.Password;
        }

        private void BtnUserHapus_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbKdUser.Text))
            {
                var user = new Karyawan() {KaryawanId = TbKdUser.Text};
                var result = MessageBox.Show("Data akan dihapus?", "Peringatan", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        DCBPDMHDataContext.DeleteKaryawan(user);
                        ClearEt();
                        TbKdUser.IsEnabled = true;
                        TbKdUser.Focus();
                        InitiateListview();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Data tidak boleh kosong", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearEt()
        {
            TbKdUser.Clear();
            TbNamaUser.Clear();
            TbPassword.Clear();
            TbKdUser.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitiateListview();
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
