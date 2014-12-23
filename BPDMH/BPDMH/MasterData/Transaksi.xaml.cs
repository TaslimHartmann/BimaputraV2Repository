using System;
using System.Collections;
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
    /// Interaction logic for Transaksi.xaml
    /// </summary>
    public partial class Transaksi : Window
    {
        public Transaksi()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var ab = new DCBPDMHDataContext();
//            var dc = new TransactionList("001");
//            IEnumerable abc = dc;
//            ListViewTransaksi.ItemsSource = abc;
//            var d = new TransactionList();
//            this.DataContext = d;
//            string dd = TbNoSp.Text;
//            var c = ab.GetTDByPengId("001");
           // ListViewTransaksi.ItemsSource = c;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBoxNoSp.Visibility = Visibility.Visible;
        }
    }
}
