using System.Globalization;
using System.Linq;
using System.Windows;
using BPDMH.DataSet;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for Maintenance.xaml
    /// </summary>
    public partial class Maintenance
    {
        private DCBPDMHDataContext _bpdmhContext;

        public Maintenance()
        {
            InitializeComponent();
        }

        private void Maintenance_OnLoaded(object sender, RoutedEventArgs e)
        {
            _bpdmhContext = new DCBPDMHDataContext();
        }

        private void BtnCekPengiriman_OnClick(object sender, RoutedEventArgs e)
        {
            var q = _bpdmhContext.GetTable<TrnPengirimanH>()
                .Where(c => c.NoSeri == null || c.TglInput == null)
                .ToList();
            TbId.Text = q.Count.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnHapusPengiriman_OnClick(object sender, RoutedEventArgs e)
        {
            var q = _bpdmhContext.GetTable<TrnPengirimanH>()
                .Where(c => c.NoSeri == null || c.TglInput == null)
                .ToList();
            _bpdmhContext.GetTable<TrnPengirimanH>().DeleteAllOnSubmit(q);
            _bpdmhContext.SubmitChanges();

            var a = _bpdmhContext.GetTable<TrnPengirimanH>()
                .Where(c => c.NoSeri == null || c.TglInput == null)
                .ToList();
            TbId.Text = a.Count.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnCekPenerimaan_OnClick(object sender, RoutedEventArgs e)
        {
            var q = _bpdmhContext.GetTable<TrnPenerimaanH>()
                .Where(c => c.NoSeri == null || c.TglInput == null)
                .ToList();
            TbNama.Text = q.Count.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnHapusPenerimaan_OnClick(object sender, RoutedEventArgs e)
        {
            var q = _bpdmhContext.GetTable<TrnPenerimaanH>().Where(c => c.NoSeri != null && c.TglInput == null).ToList();
            _bpdmhContext.GetTable<TrnPenerimaanH>().DeleteAllOnSubmit(q);
            _bpdmhContext.SubmitChanges();

            var a = _bpdmhContext.GetTable<TrnPenerimaanH>().Where(c => c.NoSeri != null && c.TglInput == null).ToList();
            TbNama.Text = a.Count.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnTutup_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
