using System;
using System.Windows;
using System.Windows.Forms;
using BPDMH.DataSet;
using BPDMH.Report;
using BPDMH.Transaksi;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MasterPage.xaml
    /// </summary>
    public partial class MasterPage
    {
        public MenuItem MenuMaster1 { get; set; }

        public MasterPage()
        {
            InitializeComponent();
            LblJam.Content = DateTime.Now;
        }

        private void MenuLogout_OnClick(object sender, RoutedEventArgs e)
        {
            MenuLogin.IsEnabled = true;
            //CloseAllWindows();
            Close();
        }

        private void MenuPelanggan_OnClick(object sender, RoutedEventArgs e)
        {
            new MstPelanggan().Show();
        }

        private void MenuKendaraan_OnClick(object sender, RoutedEventArgs e)
        {
            new MstKendaraan().Show();
        }

        private void MenuKaryawan_OnClick(object sender, RoutedEventArgs e)
        {
            new MstKaryawan().Show();
        }

        private void MenuCabang_OnClick(object sender, RoutedEventArgs e)
        {
            new MstCabang().Show();
        }

        private void MenuPebungkus_OnClick(object sender, RoutedEventArgs e)
        {
            new MstPembungkus().Show();
        }

        private void MenuPengiriman_OnClick(object sender, RoutedEventArgs e)
        {
            new TrnPengiriman().Show();
        }

        private void MenuDMH_OnClick(object sender, RoutedEventArgs e)
        {
            new DmhReportToExcel().Show();
        }

        private void MenuDMHDetail_OnClick(object sender, RoutedEventArgs e)
        {
            new DaftarMuatHarian().Show();
        }

        private void MenuPembayaran_OnClick(object sender, RoutedEventArgs e)
        {
            new MstPembayaran().Show();
        }

        private void MenuLogin_OnClick(object sender, RoutedEventArgs e)
        {
//            var lgn = new LoginScreen();
//            lgn.Owner = this;
//            lgn.ShowDialog();
            new LoginScreen().Show();
//            Close();
        }

        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        private void MenuReturPengiriman_OnClick(object sender, RoutedEventArgs e)
        {
            new RtrKirim().Show();
        }

        private void MenuPenerimaan_OnClick(object sender, RoutedEventArgs e)
        {
            new TrnPenerimaan().Show();
        }

        private void MenuReturPenerimaan_OnClick(object sender, RoutedEventArgs e)
        {
            new RtrTerima().Show();
        }

        private void MenuKbhPengiriman_OnClick(object sender, RoutedEventArgs e)
        {
            new KbhPengiriman().Show();
        }

        private void MenuKbhPenerimaan_OnClick(object sender, RoutedEventArgs e)
        {
            new KbhPenerimaan().Show();
        }

        private void MenuPengirimanReport_OnClick(object sender, RoutedEventArgs e)
        {
            new LapPengiriman().Show();
        }

        private void MenuPenerimaanReport_OnClick(object sender, RoutedEventArgs e)
        {
            new LapPenerimaan().Show();
        }

        private void MenuSupir_OnClick(object sender, RoutedEventArgs e)
        {
            new MstSupir().Show();
        }

        private void MenuMaintenance_OnClick(object sender, RoutedEventArgs e)
        {
            new Maintenance().Show();
        }
    }
}
