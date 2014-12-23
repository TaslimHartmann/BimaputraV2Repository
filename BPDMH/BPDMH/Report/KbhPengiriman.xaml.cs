using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BPDMH.DataSet;
using BPDMH.Tools;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Button = System.Windows.Controls.Button;
using Cabang = BPDMH.DataSet.Cabang;
using MessageBox = System.Windows.MessageBox;

namespace BPDMH.Report
{
    /// <summary>
    /// Interaction logic for KbhPengiriman.xaml
    /// </summary>
    public partial class KbhPengiriman
    {
        private readonly DCBPDMHDataContext _bpdmhContext;
        public string PlgId { get; set; }
        public string PnrId { get; set; }
        private CollectionViewSource _custLis;
        public bool IsPengirimUpdated { get; set; }
        public bool IsPenerimaUpdated { get; set; }
        private CollectionViewSource _masterViewSource;
        private CollectionViewSource _detailViewSource;
        private CollectionViewSource _rtrViewSource;
        const string Format = "ddddddd d MMM yyyy";

        public KbhPengiriman()
        {
            InitializeComponent();
            _bpdmhContext = new DCBPDMHDataContext();
        }

        private void LaporanPengiriman_OnLoaded(object sender, RoutedEventArgs e)
        {
            GetCabangLookup();
            TglPicker.SelectedDate = DateTime.Today;
            TglPicker2.SelectedDate = DateTime.Today;
            TglPicker.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Today);
            TglPicker2.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Today);
            CboCabang.Focus();
        }

        private void GetCabangLookup()
        {
            var cabangList = _bpdmhContext.GetTable<Cabang>()
                .Select(c => new { c.CabangId, c.NmCabang }).OrderBy(c => c.CabangId);
            CboCabang.ItemsSource = cabangList;
        }

        private void GetMasterData()
        {
            _masterViewSource = (CollectionViewSource)FindResource("MasterView");
            _detailViewSource = (CollectionViewSource)FindResource("DetailView");
            _rtrViewSource = (CollectionViewSource)FindResource("ReturView");
            GetDatas();
        }

        private void GetDatas()
        {
            IEnumerable<GetTransaksiHResult> thResult;
            if (CboCabang.SelectedItem != null && TglPicker.SelectedDate != null && TglPicker2.SelectedDate != null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString()
                        && c.TglInput >= TglPicker.SelectedDate
                        && c.TglInput <= TglPicker2.SelectedDate)
                        .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate != null && TglPicker2.SelectedDate == null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString()
                    && c.TglInput == TglPicker.SelectedDate)
                    .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate == null && TglPicker2.SelectedDate != null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString()
                    && c.TglInput == TglPicker2.SelectedDate)
                    .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate != null && TglPicker2.SelectedDate != null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput >= TglPicker.SelectedDate
                    && c.TglInput <= TglPicker2.SelectedDate)
                    .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate == null && TglPicker2.SelectedDate == null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString())
                        .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate != null && TglPicker2.SelectedDate == null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput == TglPicker.SelectedDate)
                    .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate == null && TglPicker2.SelectedDate != null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput == TglPicker2.SelectedDate)
                    .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());
            else
            {
                MessageBox.Show("Silahkan masukkan kategori pencarian", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var getThResults = thResult as GetTransaksiHResult[] ?? thResult.ToArray();
            if (!getThResults.Any())
            {
                DisplayNoData();
                BtnCetak.IsEnabled = false;
                CboCabang.Focus();
                _masterViewSource.Source = new BindingList<Model.KbhPengiriman>(new List<Model.KbhPengiriman>());
            }
            else
            {
                BtnCetak.IsEnabled = true;
                var daftarMuatList = new List<Model.KbhPengiriman>();

                foreach (var getThResult in getThResults.ToList())
                {
                    var daftarMuatHarian = new Model.KbhPengiriman()
                    {
                        PengirimanId = getThResult.TrnPengirimanId,
                        NoSeri = getThResult.NoSeri,
                        TglInput = (DateTime)getThResult.TglInput,
                        //                        PengirimId = getThResult.PengirimId,
                        NamaPengirim = getThResult.NamaPengirim,
                        AlamatPengirim = getThResult.AlamatPengirim,
                        //                        PenerimaId = getThResult.PenerimaId,
                        NamaPenerima = getThResult.NamaPenerima,
                        AlamatPenerima = getThResult.AlamatPenerima,
                        CabangId = getThResult.CabangId,
                        NamaCabang = getThResult.NmCabang,
                        AlamatCabang = getThResult.Alamat,
                        PembayaranId = getThResult.PembayaranId,
                        KetBayar = getThResult.Keterangan,
                        KendaraanId = getThResult.KendaraanId,
                        NoPolisi = getThResult.NoPolisi,
                        Jenis = getThResult.Jenis,
                        KaryawanId = getThResult.KaryawanId,
                        Checker = getThResult.Nama,
                        BiayaPenerus = getThResult.BiayaPenerus,
                        Biaya = getThResult.Biaya,
                        TdByPengIdResults = GetDetailData(getThResult),
                        RtrKirim = GetReturKirim(getThResult)
                    };
                    daftarMuatList.Add(daftarMuatHarian);
                }
                var listCollection = new ListCollectionView(daftarMuatList);
                if (listCollection.GroupDescriptions != null)
                    listCollection.GroupDescriptions.Add(new PropertyGroupDescription("NoSeri"));
                _masterViewSource.Source = new BindingList<Model.KbhPengiriman>(daftarMuatList);
                BtnCetak.Focus();
            }
        }

        private List<GetRtrKirimByPengIdResult> GetReturKirim(GetTransaksiHResult getThResult)
        {
            var newRtr = new List<GetRtrKirimByPengIdResult>();
            var tdResult = _bpdmhContext.GetRtrKirimByPengId(getThResult.TrnPengirimanId);
            foreach (var getThResultForDetail in tdResult)
            {
                var getTdPengIdResult = new GetRtrKirimByPengIdResult
                {
                    ReturTrnKirimId = getThResultForDetail.ReturTrnKirimId,
                    PengirimanId = getThResultForDetail.PengirimanId,
                    TglInput = getThResultForDetail.TglInput,
                    TglTerima = getThResultForDetail.TglTerima,
                    Penerima = getThResultForDetail.Penerima,
                    KaryawanId = getThResultForDetail.KaryawanId,
                    Nama = getThResultForDetail.Nama
                };
                newRtr.Add(getTdPengIdResult);
            }
            return newRtr;
        }

        private static void DisplayNoData()
        {
            MessageBox.Show("Data tidak ditemukan", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<GetTransaksiDByPengIdResult> GetDetailData(GetTransaksiHResult getThResult)
        {
            var tdResult = _bpdmhContext.GetTransaksiDByPengId(getThResult.TrnPengirimanId);
            return tdResult.Select(getThResultForDetail => new GetTransaksiDByPengIdResult
            {
                TrnPengirimanDId = getThResultForDetail.TrnPengirimanDId,
                TrnPengirimanId = getThResultForDetail.TrnPengirimanId,
                JmlColie = getThResultForDetail.JmlColie,
                PembungkusId = getThResultForDetail.PembungkusId,
                KetBungkus = getThResultForDetail.KetBungkus,
                NamaBarang = getThResultForDetail.NamaBarang,
                Berat = getThResultForDetail.Berat
            }).ToList();
        }

        private void BtnGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            GetMasterData();
        }

        private void BtnCetak_OnClick(object sender, RoutedEventArgs e)
        {
            const string format = "ddddddd d MMM yyyy";
            var dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.FileName = "KBHPengiriman_" + DateTime.Now.ToString(format, CultureInfo.CreateSpecificCulture("id-ID"));
            dlg.DefaultExt = ".text";
            dlg.Filter = "Excel 2007 (.xls)|*.xls";

            var result = dlg.ShowDialog();
            if (result != true) return;
            var fileName = dlg.FileName;
            PrintToExcel(fileName);
        }

        private void PrintToExcel(string fileName)
        {
            var xlApp = new Application();
            object misValue = Missing.Value;

            var xlWorkBook = xlApp.Workbooks.Add(misValue);
            var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.Item[1];
            var headerCell = xlWorkSheet.Range["a1", "n1"];
            headerCell.Font.Size = 15;
            headerCell.EntireRow.Font.Bold = true;
            headerCell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            headerCell.Merge(Type.Missing);

            var formatRange = xlWorkSheet.Range["a3", "n1"];
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Size = 12;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            formatRange.Columns.AutoFit();
            //formatRange.Borders.Color = Color.Black;

            var tglHariIni = "";
            if (TglPicker.SelectedDate != null)
                tglHariIni = TglPicker.SelectedDate.Value.ToString(Format, CultureInfo.CreateSpecificCulture("id-ID"));

            xlWorkSheet.Cells[1, 1] = "KBH KIRIM";
            xlWorkSheet.Cells[3, 1] = "Tanggal";
            xlWorkSheet.Cells[3, 2] = "No. Seri";
            xlWorkSheet.Cells[3, 3] = "Via";
            xlWorkSheet.Cells[3, 4] = "Tujuan";
            xlWorkSheet.Cells[3, 5] = "Bea";
            xlWorkSheet.Cells[3, 6] = "Pengirim";
            xlWorkSheet.Cells[3, 7] = "Penerima";

            //            xlWo3kSheet.Cells[6, 6] = "PengirimanDId";
            //            xlWo3kSheet.Cells[6, 7] = "PenerimaanId";
            xlWorkSheet.Cells[3, 8] = "Jumlah Collie";
            xlWorkSheet.Cells[3, 9] = "Pembungkus";
            xlWorkSheet.Cells[3, 10] = "Barang";
            xlWorkSheet.Cells[3, 11] = "Berat";
            xlWorkSheet.Cells[3, 12] = "Penerus";
            xlWorkSheet.Cells[3, 13] = "Biaya";
            xlWorkSheet.Cells[3, 14] = "Penerima";

            //            xlWorkSheet.Cells[6, 10] = "CAD";

            var daftarMuatHarians = GetLaporanPengirimanList();
            if (daftarMuatHarians == null)
                return;
            var lastrow = 2;
            for (var x = 0; x < daftarMuatHarians.Count; x++)
            {
                var a = 1;
                //                xlWorkSheet.Cells[lastrow + x + 2, a] = daftarMuatHarians[x].PenerimaanId;
                //                a++;

                var rowIndex = lastrow + x + 2;

                xlWorkSheet.Cells[rowIndex, a] = string.Format("{0:d/M/yyyy}", daftarMuatHarians[x].TglInput);
                var columnIndex = rowIndex + (daftarMuatHarians[x].TdByPengIdResults.Count - 1);
                var tglMerge = xlWorkSheet.Range["a" + rowIndex, "a" + columnIndex];
                tglMerge.Merge(Type.Missing);
                tglMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = "'" + daftarMuatHarians[x].NoSeri;
                var noSeriMerge = xlWorkSheet.Range["b" + rowIndex, "b" + columnIndex];
                noSeriMerge.Merge(Type.Missing);
                noSeriMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NoPolisi;
                var noPolisiMerge = xlWorkSheet.Range["c" + rowIndex, "c" + columnIndex];
                noPolisiMerge.Merge(Type.Missing);
                noPolisiMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NamaCabang;
                var cabangMerge = xlWorkSheet.Range["d" + rowIndex, "d" + columnIndex];
                cabangMerge.Merge(Type.Missing);
                cabangMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].KetBayar;
                var bayarMerge = xlWorkSheet.Range["e" + rowIndex, "e" + columnIndex];
                bayarMerge.Merge(Type.Missing);
                bayarMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NamaPengirim;
                var pengirimMerge = xlWorkSheet.Range["f" + rowIndex, "f" + columnIndex];
                pengirimMerge.Merge(Type.Missing);
                pengirimMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NamaPenerima;
                var penerimaMerge = xlWorkSheet.Range["g" + rowIndex, "g" + columnIndex];
                penerimaMerge.Merge(Type.Missing);
                penerimaMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                for (var g = 0; g < daftarMuatHarians[x].TdByPengIdResults.Count; g++)
                {
                    var zz = a++;
                    //                    xlWorkSheet.Cells[lastrow + x + 2 + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].PengirimanDId;
                    //                    a++;
                    //                    xlWorkSheet.Cells[lastrow + x + 2 + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].PenerimaanId;
                    //                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].JmlColie;
                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].KetBungkus;
                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].NamaBarang;
                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].Berat;
                    a = zz;
                }

                xlWorkSheet.Cells[rowIndex, a + 5] = daftarMuatHarians[x].BiayaPenerus;
                var penerusMerge = xlWorkSheet.Range["l" + rowIndex, "l" + columnIndex];
                penerusMerge.Merge(Type.Missing);
                penerusMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a + 5] = daftarMuatHarians[x].Biaya;
                var biayaMerge = xlWorkSheet.Range["m" + rowIndex, "m" + columnIndex];
                biayaMerge.Merge(Type.Missing);
                biayaMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                var getRtrKirimByPengIdResults = daftarMuatHarians[x].RtrKirim;
                if (getRtrKirimByPengIdResults.Count > 0)
                    xlWorkSheet.Cells[rowIndex, a + 5] = getRtrKirimByPengIdResults[0].Penerima;
                var pnrRtrMerge = xlWorkSheet.Range["n" + rowIndex, "n" + columnIndex];
                pnrRtrMerge.Merge(Type.Missing);
                pnrRtrMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                if (daftarMuatHarians[x].TdByPengIdResults.Count > 1)
                {
                    lastrow += daftarMuatHarians[x].TdByPengIdResults.Count - 1;
                }
                ReleaseObject(noSeriMerge);
                ReleaseObject(cabangMerge);
                ReleaseObject(bayarMerge);
                ReleaseObject(pnrRtrMerge);
                ReleaseObject(penerusMerge);
            }

            var endRow = lastrow + daftarMuatHarians.Count;
            var columnRange = xlWorkSheet.Range["a3", "n" + (endRow + 1)];
            columnRange.Columns.AutoFit();
            columnRange.Borders.Color = Color.Black;
            columnRange.BorderAround(Missing.Value, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, ColorTranslator.ToOle(Color.FromArgb(255, 192, 0)));

            xlWorkBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                 XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(headerCell);
            ReleaseObject(formatRange);
            ReleaseObject(columnRange);

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

            MessageBox.Show("File selesai dibuat.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<Model.KbhPengiriman> GetLaporanPengirimanList()
        {
            IEnumerable<GetTransaksiHResult> thResult = null;
            if (CboCabang.SelectedItem != null && TglPicker.SelectedDate != null && TglPicker2.SelectedDate != null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString()
                        && c.TglInput >= TglPicker.SelectedDate
                        && c.TglInput <= TglPicker2.SelectedDate)
                        .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate != null && TglPicker2.SelectedDate == null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString()
                    && c.TglInput == TglPicker.SelectedDate)
                    .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate == null && TglPicker2.SelectedDate != null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString()
                    && c.TglInput == TglPicker2.SelectedDate)
                    .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate != null && TglPicker2.SelectedDate != null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput >= TglPicker.SelectedDate
                    && c.TglInput <= TglPicker2.SelectedDate)
                    .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate == null && TglPicker2.SelectedDate == null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString())
                        .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate != null && TglPicker2.SelectedDate == null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput == TglPicker.SelectedDate)
                        .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate == null && TglPicker2.SelectedDate != null)
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput == TglPicker2.SelectedDate)
                    .OrderBy(c => c.TglInput).ThenBy(c => c.NoSeri, new SemiNumericComparer());

            var getThResults = thResult as GetTransaksiHResult[] ?? thResult.ToArray();
            if (!getThResults.Any())
            {
                DisplayNoData();
                BtnCetak.IsEnabled = false;
                CboCabang.Focus();
            }
            else
            {
                BtnCetak.IsEnabled = true;
                var daftarMuatList = new List<Model.KbhPengiriman>();

                foreach (var getThResult in getThResults.ToList())
                {
                    var daftarMuatHarian = new Model.KbhPengiriman()
                    {
                        PengirimanId = getThResult.TrnPengirimanId,
                        NoSeri = getThResult.NoSeri,
                        TglInput = (DateTime)getThResult.TglInput,
                        //                        PengirimId = getThResult.PengirimId,
                        NamaPengirim = getThResult.NamaPengirim,
                        AlamatPengirim = getThResult.AlamatPengirim,
                        //                        PenerimaId = getThResult.PenerimaId,
                        NamaPenerima = getThResult.NamaPenerima,
                        AlamatPenerima = getThResult.AlamatPenerima,
                        CabangId = getThResult.CabangId,
                        NamaCabang = getThResult.NmCabang,
                        AlamatCabang = getThResult.Alamat,
                        PembayaranId = getThResult.PembayaranId,
                        KetBayar = getThResult.Keterangan,
                        KendaraanId = getThResult.KendaraanId,
                        NoPolisi = getThResult.NoPolisi,
                        Jenis = getThResult.Jenis,
                        KaryawanId = getThResult.KaryawanId,
                        Checker = getThResult.Nama,
                        BiayaPenerus = getThResult.BiayaPenerus,
                        Biaya = getThResult.Biaya,
                        TdByPengIdResults = GetDetailData(getThResult),
                        RtrKirim = GetReturKirim(getThResult)
                    };
                    daftarMuatList.Add(daftarMuatHarian);
                }

                return daftarMuatList;
            }
            return null;
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex);
            }
            finally
            {
                obj = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HandleExpandCollapseForRow(object sender, RoutedEventArgs e)
        {
            var expandCollapseButton = (Button)sender;
            var selectedRow = DataGridRow.GetRowContainingElement(expandCollapseButton);

            if (null != expandCollapseButton && "+" == expandCollapseButton.Content.ToString())
            {
                if (selectedRow != null) selectedRow.DetailsVisibility = Visibility.Visible;
                expandCollapseButton.Content = "-";
            }
            else
            {
                if (selectedRow != null) selectedRow.DetailsVisibility = Visibility.Collapsed;
                if (expandCollapseButton != null) expandCollapseButton.Content = "+";
            }
        }
    }
}
