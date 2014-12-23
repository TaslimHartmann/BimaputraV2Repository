using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BPDMH.DataSet;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for MstPembungkus.xaml
    /// </summary>
    public partial class MstPembungkus
    {
        public MstPembungkus()
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
            var q = bpdmhContext.GetTable<Pembungkus>()
                .OrderBy(a => a.Keterangan)
                .Select(a => a);
            ListViewKendaraan.ItemsSource = q;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbId.Text))
            {
                var pbn = new Pembungkus { PembungkusId = TbId.Text.Trim(), Keterangan = TbKet.Text.Trim() };
                var isOk = DCBPDMHDataContext.InsertPembungkus(pbn);
                if (!isOk) return; 
                RestartViews();
                InitilizeListView();
            }
            else
            {
                MessageBox.Show("Data tidak boleh kosong", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //            var dc = new DCBPDMHDataContext();
            //            var vc = from pengh in dc.PengirimanHs
            //                     join cbn in dc.Cabangs
            //                        on pengh.CabangId equals cbn.CabangId
            //                     join plg in dc.Pelanggans
            //                        on pengh.PengirimId equals plg.PelangganId
            //                     join pnr in dc.Pelanggans
            //                        on pengh.PenerimaId equals pnr.PelangganId
            //                     join knd in dc.Kendaraans
            //                        on pengh.KendaraanId equals knd.KendaraanId
            //                     join kry in dc.Karyawans
            //                        on pengh.KaryawanId equals kry.KaryawanId
            //                     join pengD in dc.PengirimanDs
            //                         on pengh.PenerimaanId equals pengD.PenerimaanId
            //                     join pbn in dc.Pembungkus
            //                        on pengD.PembungkusId equals pbn.PembungkusId
            //                     select new { pengh.PenerimaanId, pengh.NoSeri, NamaPengirim = plg.NamaPlg, NamaPenerima = pnr.NamaPlg, knd.NoPolisi, kry.Nama, pengD.NamaBarang };

            //            var vc =
            //                dc.PengirimanHs.Join(dc.Cabangs, pengh => pengh.CabangId, cbn => cbn.CabangId,
            //                                     (pengh, cbn) => new { pengh, cbn })
            //                  .Join(dc.Pelanggans, @t => @t.pengh.PengirimId, plg => plg.PelangganId, (@t, plg) => new { @t, plg })
            //                  .Join(dc.Pelanggans, @t => @t.@t.pengh.PenerimaId, pnr => pnr.PelangganId, (@t, pnr) => new { @t, pnr })
            //                  .Join(dc.Kendaraans, @t => @t.@t.@t.pengh.KendaraanId, knd => knd.KendaraanId,
            //                        (@t, knd) => new { @t, knd })
            //                  .Join(dc.Karyawans, @t => @t.@t.@t.@t.pengh.KaryawanId, kry => kry.KaryawanId,
            //                        (@t, kry) => new { @t, kry })
            //                  .Join(dc.PengirimanDs, @t => @t.@t.@t.@t.@t.pengh.PenerimaanId, pengD => pengD.PenerimaanId,
            //                        (@t, pengD) => new { @t, pengD })
            //                  .Join(dc.Pembungkus, @t => @t.pengD.PembungkusId, pbn => pbn.PembungkusId,
            //                        (@t, pbn) =>
            //                        new
            //                            {
            //                                @t.@t.@t.@t.@t.@t.pengh.PenerimaanId,
            //                                @t.@t.@t.@t.@t.@t.pengh.NoSeri,
            //                                NamaPengirim = @t.@t.@t.@t.@t.plg.NamaPlg,
            //                                NamaPenerima = @t.@t.@t.@t.pnr.NamaPlg,
            //                                @t.@t.@t.knd.NoPolisi,
            //                                @t.@t.kry.Nama,
            //                                @t.pengD.NamaBarang
            //                            });
            //
            //            ListViewKendaraan.ItemsSource = vc;
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
                var pbn = new Pembungkus { PembungkusId = TbId.Text };
                var result = MessageBox.Show("Data akan dihapus?", "Peringatan", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        DCBPDMHDataContext.DeletePembungkus(pbn);
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
            var knd = (Pembungkus)ListViewKendaraan.SelectedItem;
            if (knd == null) return;
            TbId.IsEnabled = false;
            TbId.Text = knd.PembungkusId;
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