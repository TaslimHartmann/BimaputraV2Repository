using System;
using System.Collections.Generic;
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

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for TransaksiDMHLookup.xaml
    /// </summary>
    public partial class TransaksiDmhLookup
    {
        private BPDMH.Transaksi.TrnPengiriman _trnPengiriman;
        private DCBPDMHDataContext bpdmhContext;
        private string _noSP;

        public TransaksiDmhLookup()
        {
            InitializeComponent();

        }

        public TransaksiDmhLookup(BPDMH.Transaksi.TrnPengiriman trnPengiriman)
        {
//            bpdmhContext = new DCBPDMHDataContext();
//            var q = bpdmhContext.GetTable<PengirimanH>();
            InitializeComponent();
            GetPengirimanH();
            _trnPengiriman = trnPengiriman;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            GetPengirimanH();
        }

        private void GetPengirimanH()
        {
            bpdmhContext = new DCBPDMHDataContext();
            var q = bpdmhContext.GetTable<PengirimanH>();
            MainGrid.ItemsSource = q;
        }

        private void BtnGetValue_Click(object sender, RoutedEventArgs e)
        {
            var q = bpdmhContext.GetTrn()
                .Where(p => p.NoSeri == _noSP).ToList();
            _trnPengiriman.MasterViewSource.Source = q;
            _trnPengiriman.Show();
            Close();
        }

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pby = (PengirimanH)MainGrid.SelectedItem;
            if (pby == null) return;
            _noSP = pby.NoSeri;
        }

    }
}
