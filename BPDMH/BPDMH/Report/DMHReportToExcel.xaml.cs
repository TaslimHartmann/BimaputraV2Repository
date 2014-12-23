using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BPDMH.DataSet;
using BPDMH.Model;
using BPDMH.Tools;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Button = System.Windows.Controls.Button;
using Cabang = BPDMH.DataSet.Cabang;
using CheckBox = System.Windows.Controls.CheckBox;
using MessageBox = System.Windows.MessageBox;

namespace BPDMH.Report
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class DmhReportToExcel
    {
        private readonly DCBPDMHDataContext _bpdmhContext;
        public string PlgId { get; set; }
        public string PnrId { get; set; }
        private CollectionViewSource _custLis;
        public bool IsPengirimUpdated { get; set; }
        public bool IsPenerimaUpdated { get; set; }
        private CollectionViewSource _masterViewSource;
        private CollectionViewSource _detailViewSource;
        private readonly List<string> _cabangList = new List<string>();
        private const string Format = "ddddddd d MMM yyyy";
        public ObservableCollection<CheckBoxCabang> CheckBoxCabang { get; set; }


        public DmhReportToExcel()
        {
            InitializeComponent();
            _bpdmhContext = new DCBPDMHDataContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetKendaraanLookup();
            InitializeCabangList();
            GetCabangLookup();
            //            GetCustomerList();

            TglPicker.DisplayDate = DateTime.Today;
            TglPicker.Text = DateTime.Today.ToShortDateString();

            GetSupirLookup();
            GetKaryawanLookup1();
            GetKaryawanLookup2();
            //GetCustomerList();
            //GetMasterData();
            CboCabang.Focus();
        }

        private void InitializeCabangList()
        {
            CheckBoxCabang = new ObservableCollection<CheckBoxCabang>();
            foreach (var cabang in _bpdmhContext.GetTable<Cabang>().ToList())
            {
                var db = new Cabang() { CabangId = cabang.CabangId };
                var newCbdb = new CheckBoxCabang { Cabang = db, IsChecked = false, CabangId = db.CabangId };
                CheckBoxCabang.Add(newCbdb);
            }
        }

        private void GetSupirLookup()
        {
            var supirList = _bpdmhContext.GetTable<Supir>()
                .OrderBy(k => k.NamaSupir)
                .Select(k => new { k.SupirId, k.NamaSupir });
            CboSupir.ItemsSource = supirList;
        }

        private void GetKaryawanLookup1()
        {
            var karyawanList = _bpdmhContext.GetTable<Karyawan>()
                .OrderBy(k => k.Nama)
                .Select(k => new { k.KaryawanId, k.Nama });
            //            var karySource = (CollectionViewSource)this.FindResource("KaryawanLookup1");
            //            karySource.Source = karyawanList;
            CboPenyusun.ItemsSource = karyawanList;
        }

        private void GetKaryawanLookup2()
        {
            var karyawanList = _bpdmhContext.GetTable<Karyawan>()
                .OrderBy(k => k.Nama)
                .Select(k => new { k.KaryawanId, k.Nama });
            //            var karySource = (CollectionViewSource)this.FindResource("KaryawanLookup2");
            //            karySource.Source = karyawanList;
            CboPeneliti.ItemsSource = karyawanList;
        }

        private void GetMasterData()
        {
            _masterViewSource = (CollectionViewSource)FindResource("MasterView");
            _detailViewSource = (CollectionViewSource)FindResource("DetailView");
            GetDatas();
        }

        private void GetDatas()
        {
            var thResult = Enumerable.Empty<GetTransaksiHResult>();
            List<IEnumerable<GetTransaksiHResult>> thResultList = null;
            //            if (CboCabang.SelectedItem != null && TglPicker.SelectedDate != null
            //                && CboKendaraan.SelectedItem != null)
            //                thResult = _bpdmhContext.GetTransaksiH()
            //                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString()
            //                                && c.TglInput == TglPicker.SelectedDate
            //                                && c.KendaraanId == CboKendaraan.SelectedValue.ToString())
            //                    .OrderBy(c => c.NoSeri);
            //
            //            else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate != null
            //                     && CboKendaraan.SelectedItem == null)
            //                thResult = _bpdmhContext.GetTransaksiH()
            //                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString()
            //                                && c.TglInput == TglPicker.SelectedDate)
            //                    .OrderBy(c => c.NoSeri);
            //
            //            else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate == null
            //                     && CboKendaraan.SelectedItem != null)
            //                thResult = _bpdmhContext.GetTransaksiH()
            //                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString()
            //                                && c.KendaraanId == CboKendaraan.SelectedValue.ToString())
            //                    .OrderBy(c => c.NoSeri);
            //
            //            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate != null
            //                     && CboKendaraan.SelectedItem != null)
            //                thResult = _bpdmhContext.GetTransaksiH()
            //                    .Where(c => c.TglInput == TglPicker.SelectedDate
            //                                && c.KendaraanId == CboKendaraan.SelectedValue.ToString())
            //                    .OrderBy(c => c.NoSeri);
            //
            //            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate == null
            //                     && CboKendaraan.SelectedItem != null)
            //                thResult = _bpdmhContext.GetTransaksiH()
            //                    .Where(c => c.KendaraanId == CboKendaraan.SelectedValue.ToString())
            //                    .OrderBy(c => c.NoSeri);
            //
            //            else if (CboCabang.SelectedItem != null && TglPicker.SelectedDate == null
            //                     && CboKendaraan.SelectedItem == null)
            //                thResult = _bpdmhContext.GetTransaksiH()
            //                    .Where(c => c.CabangId == CboCabang.SelectedValue.ToString())
            //                    .OrderBy(c => c.NoSeri);
            //
            //            else if (CboCabang.SelectedItem == null && TglPicker.SelectedDate != null
            //                     && CboKendaraan.SelectedItem == null)
            //                thResult = _bpdmhContext.GetTransaksiH()
            //                    .Where(c => c.TglInput == TglPicker.SelectedDate).OrderBy(c => c.NoSeri);
            //
            //            else
            //                thResult = _bpdmhContext.GetTransaksiH().OrderBy(c => c.NoSeri);
            
            if (_cabangList.Count > 0 && TglPicker.SelectedDate != null
                && CboKendaraan.SelectedItem != null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                         .Where(c => _cabangList.Contains(c.CabangId) && c.TglInput == TglPicker.SelectedDate
                                     && c.KendaraanId == CboKendaraan.SelectedValue.ToString())
                         .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count > 0 && TglPicker.SelectedDate != null
                     && CboKendaraan.SelectedItem == null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                        .Where(c => _cabangList.Contains(c.CabangId) && c.TglInput == TglPicker.SelectedDate)
                        .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count > 0 && TglPicker.SelectedDate == null
                     && CboKendaraan.SelectedItem != null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => _cabangList.Contains(c.CabangId) && c.KendaraanId
                                == CboKendaraan.SelectedValue.ToString())
                    .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count == 0 && TglPicker.SelectedDate != null
                     && CboKendaraan.SelectedItem != null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput == TglPicker.SelectedDate
                                && c.KendaraanId == CboKendaraan.SelectedValue.ToString())
                    .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count == 0 && TglPicker.SelectedDate == null
                     && CboKendaraan.SelectedItem != null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.KendaraanId == CboKendaraan.SelectedValue.ToString())
                    .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count > 0 && TglPicker.SelectedDate == null
                     && CboKendaraan.SelectedItem == null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                           .Where(c => _cabangList.Contains(c.CabangId))
                           .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count == 0 && TglPicker.SelectedDate != null
                     && CboKendaraan.SelectedItem == null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput == TglPicker.SelectedDate)
                    .OrderBy(c => c.NoSeri);
            }

            else
            {
                thResult = _bpdmhContext.GetTransaksiH().OrderBy(c => c.NoSeri);
            }

            var getThResults = thResult as GetTransaksiHResult[] ?? thResult.ToArray();
            if (!getThResults.Any())
            {
                DisplayNoData();
                BtnCetak.IsEnabled = false;
                CboCabang.Focus();
                _masterViewSource.Source = new BindingList<DaftarMuatHarian>(new List<DaftarMuatHarian>());
            }
            else
            {
                BtnCetak.IsEnabled = true;
                var daftarMuatList = new List<DaftarMuatHarian>();

                foreach (var getThResult in getThResults.ToList())
                {
                    var daftarMuatHarian = new DaftarMuatHarian()
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
                        TdByPengIdResults = GetDetailData(getThResult)
                    };
                    daftarMuatList.Add(daftarMuatHarian);
                }
                var listCollection = new ListCollectionView(daftarMuatList);
                if (listCollection.GroupDescriptions != null)
                    listCollection.GroupDescriptions.Add(new PropertyGroupDescription("NoSeri"));
                _masterViewSource.Source = new BindingList<DaftarMuatHarian>(daftarMuatList);
                BtnCetak.Focus();
            }
        }

        private List<GetTransaksiDByPengIdResult> GetDetailData(GetTransaksiHResult getThResult)
        {
            var detailList = new List<GetTransaksiDByPengIdResult>();

            var tdResult = _bpdmhContext.GetTransaksiDByPengId(getThResult.TrnPengirimanId);
            foreach (var getThResultForDetail in tdResult)
            {
                var getTdPengIdResult = new GetTransaksiDByPengIdResult
                {
                    TrnPengirimanDId = getThResultForDetail.TrnPengirimanDId,
                    TrnPengirimanId = getThResultForDetail.TrnPengirimanId,
                    JmlColie = getThResultForDetail.JmlColie,
                    PembungkusId = getThResultForDetail.PembungkusId,
                    KetBungkus = getThResultForDetail.KetBungkus,
                    NamaBarang = getThResultForDetail.NamaBarang,
                    Berat = getThResultForDetail.Berat
                };
                detailList.Add(getTdPengIdResult);
            }
            return detailList;
        }

        private void GetCustomerList()
        {
            var customerList = _bpdmhContext.GetTable<Pelanggan>()
                .Where(a => a.IsPengirim == true)
                .Select(a => new { PengirimId = a.PelangganId, a.NamaPlg, a.Alamat });

            var customerListBy = _bpdmhContext.GetTable<Pelanggan>()
                .Where(c => c.PelangganId == PlgId)
                .Select(a => new { PengirimId = a.PelangganId, a.NamaPlg, a.Alamat }).ToList();

            var penerimaList = _bpdmhContext.GetTable<Pelanggan>()
                .Where(a => a.IsPengirim == false)
                .Select(a => new { PenerimaId = a.PelangganId, a.NamaPlg, a.Alamat });

            var penerimaListBy = _bpdmhContext.GetTable<Pelanggan>()
                .Where(c => c.PelangganId == PnrId)
                .Select(a => new { PenerimaId = a.PelangganId, a.NamaPlg, a.Alamat }).ToList();

            _custLis = (CollectionViewSource)FindResource("PelangganLookup");
            var penerimaSource = (CollectionViewSource)FindResource("PenerimaLookup");
            if (IsPengirimUpdated || IsPenerimaUpdated && PnrId != null)
            {
                _custLis.Source = customerListBy;
                penerimaSource.Source = penerimaListBy;
            }
            else if (IsPengirimUpdated && PnrId == null)
            {
                _custLis.Source = customerListBy;
                penerimaSource.Source = penerimaList;
            }
            else if (IsPenerimaUpdated && PlgId == null)
            {
                penerimaSource.Source = penerimaListBy;
                _custLis.Source = customerList;
            }
            else
            {
                penerimaSource.Source = penerimaList;
                _custLis.Source = customerList;
            }
        }

        private void GetCabangLookup()
        {
            var daftarCabang = _bpdmhContext.GetTable<Cabang>()
                .Select(c => new { c.CabangId, c.NmCabang }).OrderBy(c => c.CabangId);
            var cabangSource = (CollectionViewSource)this.FindResource("CabangLookup");
            //            cabangSource.Source = daftarCabang;
            CboCabang.ItemsSource = daftarCabang;
            ListBoxCabang.ItemsSource = CheckBoxCabang;
        }

        private void GetKendaraanLookup()
        {
            var kendaraanList = _bpdmhContext.GetTable<Kendaraan>()
                .Select(k => new { k.KendaraanId, k.NoPolisi });
            //            var kendaraanSource = (CollectionViewSource)this.FindResource("KendaraanLookup");
            //            kendaraanSource.Source = kendaraanList;
            CboKendaraan.ItemsSource = kendaraanList;
        }

        private void PrintToExcel(string fileName)
        {
            var xlApp = new Application();
            object misValue = Missing.Value;

            var xlWorkBook = xlApp.Workbooks.Add(misValue);
            var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.Item[1];
            var headerCell = xlWorkSheet.Range["a1", "i1"];
            headerCell.Font.Size = 15;
            headerCell.EntireRow.Font.Bold = true;
            headerCell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            headerCell.Merge(Type.Missing);

            var formatRange = xlWorkSheet.Range["a6", "i6"];
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Size = 12;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //formatRange.Interior.Color = Color.RoyalBlue;
            formatRange.Columns.AutoFit();
            formatRange.Borders.Color = Color.Black;

            xlWorkSheet.Cells[1, 1] = "DAFTAR MUAT HARIAN";
            xlWorkSheet.Cells[3, 3] = "Tujuan";
            xlWorkSheet.Cells[3, 4] = ": " + NamaCabang(_cabangList);
            xlWorkSheet.Cells[4, 3] = "Tanggal";
            if (TglPicker.SelectedDate != null)
                xlWorkSheet.Cells[4, 4] = ": " +
                                          TglPicker.SelectedDate.Value.ToString(Format,
                                              CultureInfo.CreateSpecificCulture("id-ID"));
            xlWorkSheet.Cells[3, 6] = "Armada/KA";
            xlWorkSheet.Cells[3, 7] = ": " + TbKendaraan.Text;
            xlWorkSheet.Cells[4, 6] = "Sopir/Pengawal";
            xlWorkSheet.Cells[4, 7] = ": " + TbChecker.Text;

            //            xlWorkSheet.Cells[6, 1] = "PenerimaanId";
            xlWorkSheet.Cells[6, 1] = "No Seri";
            xlWorkSheet.Cells[6, 2] = "Pengirim";
            xlWorkSheet.Cells[6, 3] = "Penerima";
            xlWorkSheet.Cells[6, 4] = "Tujuan";
            //            xlWorkSheet.Cells[6, 6] = "PengirimanDId";
            //            xlWorkSheet.Cells[6, 7] = "PenerimaanId";
            xlWorkSheet.Cells[6, 5] = "Nama Barang";
            xlWorkSheet.Cells[6, 6] = "Jumlah Collie";
            xlWorkSheet.Cells[6, 7] = "Pembungkus";
            xlWorkSheet.Cells[6, 8] = "Berat";
            xlWorkSheet.Cells[6, 9] = "Pembayaran";
            //            xlWorkSheet.Cells[6, 10] = "CAD";

            var daftarMuatHarians = GetDaftarMuatHarianList();
            if (daftarMuatHarians == null)
                return;
            var lastrow = 5;
            for (var x = 0; x < daftarMuatHarians.Count; x++)
            {
                var a = 1;
                //                xlWorkSheet.Cells[lastrow + x + 2, a] = daftarMuatHarians[x].PenerimaanId;
                //                a++;

                var rowIndex = lastrow + x + 2;
                xlWorkSheet.Cells[rowIndex, a] = "'" + daftarMuatHarians[x].NoSeri;
                var columnIndex = rowIndex + (daftarMuatHarians[x].TdByPengIdResults.Count - 1);
                var firstColumnMerge = xlWorkSheet.Range["a" + rowIndex, "a" + columnIndex];
                firstColumnMerge.Merge(Type.Missing);
                firstColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NamaPengirim;
                var secondColumnMerge = xlWorkSheet.Range["b" + rowIndex, "b" + columnIndex];
                secondColumnMerge.Merge(Type.Missing);
                secondColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NamaPenerima;
                var thirdColumnMerge = xlWorkSheet.Range["c" + rowIndex, "c" + columnIndex];
                thirdColumnMerge.Merge(Type.Missing);
                thirdColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NamaCabang;
                var fourthColumnMerge = xlWorkSheet.Range["d" + rowIndex, "d" + columnIndex];
                fourthColumnMerge.Merge(Type.Missing);
                fourthColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                for (var g = 0; g < daftarMuatHarians[x].TdByPengIdResults.Count; g++)
                {
                    var zz = a++;
                    //                    xlWorkSheet.Cells[lastrow + x + 2 + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].PengirimanDId;
                    //                    a++;
                    //                    xlWorkSheet.Cells[lastrow + x + 2 + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].PenerimaanId;
                    //                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].NamaBarang;
                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].JmlColie;
                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].KetBungkus;
                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].Berat;
                    a = zz;
                }

                xlWorkSheet.Cells[rowIndex, a + 5] = daftarMuatHarians[x].KetBayar;
                var fifthColumnMerge = xlWorkSheet.Range["i" + rowIndex, "i" + columnIndex];
                fifthColumnMerge.Merge(Type.Missing);
                fifthColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                if (daftarMuatHarians[x].TdByPengIdResults.Count > 1)
                {
                    lastrow += daftarMuatHarians[x].TdByPengIdResults.Count - 1;
                }
                ReleaseObject(firstColumnMerge);
                ReleaseObject(secondColumnMerge);
                ReleaseObject(thirdColumnMerge);
                ReleaseObject(fourthColumnMerge);
                ReleaseObject(fifthColumnMerge);
            }

            var endRow = lastrow + daftarMuatHarians.Count;
            var columnRange = xlWorkSheet.Range["a6", "i" + (endRow + 1)];
            columnRange.Columns.AutoFit();
            columnRange.Borders.Color = Color.Black;

            columnRange.BorderAround(Missing.Value, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                ColorTranslator.ToOle(Color.FromArgb(255, 192, 0)));

            var footerRow = endRow + 4;

            xlWorkSheet.Cells[footerRow, 2] = "PENYUSUN";
            var penyusunCell = xlWorkSheet.Range["b" + footerRow, "d" + footerRow];

            xlWorkSheet.Cells[footerRow, 5] = "PENELITI";
            var penelitiCell = xlWorkSheet.Range["e" + footerRow, "g" + footerRow];

            xlWorkSheet.Cells[footerRow, 8] = "KETERANGAN";
            var ketCell = xlWorkSheet.Range["j" + footerRow, "l" + footerRow];

            footerRow = footerRow + 1;
            //            xlWorkSheet.Cells[footerRow, 2] = "Jakarta, _____________________";


            xlWorkSheet.Cells[footerRow, 2] = "Jakarta, " +
                                              DateTime.Now.ToString(Format, CultureInfo.CreateSpecificCulture("id-ID"));
            var jakartaCell = xlWorkSheet.Range["b" + footerRow, "d" + footerRow];

            footerRow = footerRow + 2;
            xlWorkSheet.Cells[footerRow, 2] = "Tanda Tangan";
            var ttCell = xlWorkSheet.Range["b" + footerRow, "d" + footerRow];

            xlWorkSheet.Cells[footerRow, 5] = "Tanda Tangan";
            var ttPenelitiCell = xlWorkSheet.Range["e" + footerRow, "g" + footerRow];


            footerRow = footerRow + 5;
            //            xlWorkSheet.Cells[footerRow, 2] = "[____________________________]";
            xlWorkSheet.Cells[footerRow, 2] = "[ " + TbPenyusun.Text + " ]";
            var ulCell = xlWorkSheet.Range["b" + footerRow, "d" + footerRow];

            //            xlWorkSheet.Cells[footerRow, 5] = "[____________________________]";
            xlWorkSheet.Cells[footerRow, 5] = "[ " + TbPeneliti.Text + " ]";
            var ulPenelitiCell = xlWorkSheet.Range["e" + footerRow, "g" + footerRow];

            penyusunCell.Merge(Type.Missing);
            jakartaCell.Merge(Type.Missing);
            ttCell.Merge(Type.Missing);
            ulCell.Merge(Type.Missing);

            penelitiCell.Merge(Type.Missing);
            ketCell.Merge(Type.Missing);
            ttPenelitiCell.Merge(Type.Missing);
            ulPenelitiCell.Merge(Type.Missing);

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

        private static void ReleaseObject(object obj)
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

        private static void DisplayNoData()
        {
            MessageBox.Show("Data tidak ditemukan", "Informasi");
        }

        private List<DaftarMuatHarian> GetDaftarMuatHarianList()
        {
            var thResult = Enumerable.Empty<GetTransaksiHResult>();
            if (_cabangList.Count > 0 && TglPicker.SelectedDate != null
                && CboKendaraan.SelectedItem != null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                         .Where(c => _cabangList.Contains(c.CabangId) && c.TglInput == TglPicker.SelectedDate
                                     && c.KendaraanId == CboKendaraan.SelectedValue.ToString())
                         .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count > 0 && TglPicker.SelectedDate != null
                     && CboKendaraan.SelectedItem == null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                        .Where(c => _cabangList.Contains(c.CabangId) && c.TglInput == TglPicker.SelectedDate)
                        .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count > 0 && TglPicker.SelectedDate == null
                     && CboKendaraan.SelectedItem != null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => _cabangList.Contains(c.CabangId) && c.KendaraanId
                                == CboKendaraan.SelectedValue.ToString())
                    .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count == 0 && TglPicker.SelectedDate != null
                     && CboKendaraan.SelectedItem != null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput == TglPicker.SelectedDate
                                && c.KendaraanId == CboKendaraan.SelectedValue.ToString())
                    .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count == 0 && TglPicker.SelectedDate == null
                     && CboKendaraan.SelectedItem != null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.KendaraanId == CboKendaraan.SelectedValue.ToString())
                    .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count > 0 && TglPicker.SelectedDate == null
                     && CboKendaraan.SelectedItem == null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                           .Where(c => _cabangList.Contains(c.CabangId))
                           .OrderBy(c => c.NoSeri);
            }

            else if (_cabangList.Count == 0 && TglPicker.SelectedDate != null
                     && CboKendaraan.SelectedItem == null)
            {
                thResult = _bpdmhContext.GetTransaksiH()
                    .Where(c => c.TglInput == TglPicker.SelectedDate)
                    .OrderBy(c => c.NoSeri);
            }

            else
            {
                thResult = _bpdmhContext.GetTransaksiH().OrderBy(c => c.NoSeri);
            }
            
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
                var daftarMuatList = new List<DaftarMuatHarian>();
                foreach (var getThResult in getThResults.ToList())
                {
                    var daftarMuatHarian = new DaftarMuatHarian()
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
                        Biaya = getThResult.Biaya,
                        TdByPengIdResults = GetDetailData(getThResult)
                    };
                    daftarMuatList.Add(daftarMuatHarian);
                }
                return daftarMuatList;
            }
            return null;
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

        private void BtnCetak_Click(object sender, RoutedEventArgs e)
        {
            const string format = "ddddddd d MMM yyyy";
            var dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.FileName = "DaftarMuatHarian_" +
                           DateTime.Now.ToString(format, CultureInfo.CreateSpecificCulture("id-ID"));
            dlg.DefaultExt = ".text";
            dlg.Filter = "Excel 2007 (.xls)|*.xls";

            var result = dlg.ShowDialog();
            if (result != true) return;
            var filename = dlg.FileName;
            PrintToExcel(filename);
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            //_cabangList.Clear();
            var selected = ListBoxCabang.SelectedItems;
            foreach (var selectedItem in selected)
            {
                var row = ((CheckBoxCabang)selectedItem).CabangId;
                _cabangList.Add(row);
            }
            GetMasterData();
        }

        private void TestExcel(string fileName)
        {
            //        http://www.codeproject.com/Tips/170865/Releasing-Excel-after-using-Interop

            Application excelApplication = null;
            _Workbook workBook = null;
            Workbooks workBooks = null;
            _Worksheet xlWorkSheet = null;
            Range headeRange = null;
            Range oRange = null;
            Range formatRange = null;
            excelApplication = new Application { Visible = false, DisplayAlerts = false };
            workBooks = excelApplication.Workbooks;
            workBook = workBooks.Add(1);
            xlWorkSheet = workBook.ActiveSheet;

            headeRange = xlWorkSheet.Cells;
            oRange = xlWorkSheet.Cells;
            formatRange = xlWorkSheet.Cells;
            //oRange.set_Item(1, 1, "Test");
            //oRange.Item[1, 3] = "Test"; //baris, kolom


            oRange.Item[1, 1] = "DAFTAR MUAT HARIAN";
            oRange = xlWorkSheet.Range["a1", "i1"];
            oRange.Merge(Type.Missing);
            oRange.Style.Font.Size = 12;
            //            xlWorkSheet.Range["a1"].Font.Size = 20;
            oRange.EntireRow.Font.Bold = true;

            oRange.Cells[1, 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;

            oRange.Item[3, 3] = "Tujuan";
            oRange.Style.Font.Size = 15;
            oRange.EntireRow.Font.Bold = true;
            //oRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            oRange.Columns.AutoFit();
            oRange.Borders.Color = Color.Black;

            oRange.Item[3, 4] = ": " + NamaCabang(_cabangList);
            oRange.Item[4, 3] = "Tanggal";
            if (TglPicker.SelectedDate != null)
                oRange.Item[4, 4] = ": " +
                                    TglPicker.SelectedDate.Value.ToString(Format,
                                        CultureInfo.CreateSpecificCulture("id-ID"));
            oRange.Item[3, 6] = "Armada/KA";
            oRange.Item[3, 7] = ": " + TbKendaraan.Text;
            oRange.Item[4, 6] = "Sopir/Pengawal";
            oRange.Item[4, 7] = ": " + TbChecker.Text;

            //            xlWorkSheet.Cells[6, 1] = "PenerimaanId";
            oRange.Item[6, 1] = "No Seri";
            oRange.Item[6, 2] = "Pengirim";
            oRange.Item[6, 3] = "Penerima";
            oRange.Item[6, 4] = "Tujuan";
            //            xlWorkSheet.Cells[6, 6] = "PengirimanDId";
            //            xlWorkSheet.Cells[6, 7] = "PenerimaanId";
            oRange.Item[6, 5] = "Nama Barang";
            oRange.Item[6, 6] = "Jumlah Collie";
            oRange.Item[6, 7] = "Pembungkus";
            oRange.Item[6, 8] = "Berat";
            oRange.Item[6, 9] = "Pembayaran";
            //            xlWorkSheet.Cells[6, 10] = "CAD";

            var daftarMuatHarians = GetDaftarMuatHarianList();
            if (daftarMuatHarians == null)
                return;
            var lastrow = 5;
            for (var x = 0; x < daftarMuatHarians.Count; x++)
            {
                var a = 1;
                //                xlWorkSheet.Cells[lastrow + x + 2, a] = daftarMuatHarians[x].PenerimaanId;
                //                a++;

                var rowIndex = lastrow + x + 2;
                xlWorkSheet.Cells[rowIndex, a] = "'" + daftarMuatHarians[x].NoSeri;
                var columnIndex = rowIndex + (daftarMuatHarians[x].TdByPengIdResults.Count - 1);
                var firstColumnMerge = xlWorkSheet.Range["a" + rowIndex, "a" + columnIndex];
                firstColumnMerge.Merge(Type.Missing);
                firstColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NamaPengirim;
                var secondColumnMerge = xlWorkSheet.Range["b" + rowIndex, "b" + columnIndex];
                secondColumnMerge.Merge(Type.Missing);
                secondColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NamaPenerima;
                var thirdColumnMerge = xlWorkSheet.Range["c" + rowIndex, "c" + columnIndex];
                thirdColumnMerge.Merge(Type.Missing);
                thirdColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                a++;
                xlWorkSheet.Cells[rowIndex, a] = daftarMuatHarians[x].NamaCabang;
                var fourthColumnMerge = xlWorkSheet.Range["d" + rowIndex, "d" + columnIndex];
                fourthColumnMerge.Merge(Type.Missing);
                fourthColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                for (var g = 0; g < daftarMuatHarians[x].TdByPengIdResults.Count; g++)
                {
                    var zz = a++;
                    //                    xlWorkSheet.Cells[lastrow + x + 2 + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].PengirimanDId;
                    //                    a++;
                    //                    xlWorkSheet.Cells[lastrow + x + 2 + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].PenerimaanId;
                    //                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].NamaBarang;
                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].JmlColie;
                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].KetBungkus;
                    a++;
                    xlWorkSheet.Cells[rowIndex + g, a] = daftarMuatHarians[x].TdByPengIdResults[g].Berat;
                    a = zz;
                }

                xlWorkSheet.Cells[rowIndex, a + 5] = daftarMuatHarians[x].KetBayar;
                var fifthColumnMerge = xlWorkSheet.Range["i" + rowIndex, "i" + columnIndex];
                fifthColumnMerge.Merge(Type.Missing);
                fifthColumnMerge.VerticalAlignment = XlVAlign.xlVAlignTop;

                if (daftarMuatHarians[x].TdByPengIdResults.Count > 1)
                {
                    lastrow += daftarMuatHarians[x].TdByPengIdResults.Count - 1;
                }
                ReleaseObject(firstColumnMerge);
                ReleaseObject(secondColumnMerge);
                ReleaseObject(thirdColumnMerge);
                ReleaseObject(fourthColumnMerge);
                ReleaseObject(fifthColumnMerge);
            }

            var endRow = lastrow + daftarMuatHarians.Count;
            var columnRange = xlWorkSheet.Range["a6", "i" + (endRow + 1)];
            columnRange.Columns.AutoFit();
            columnRange.Borders.Color = Color.Black;

            columnRange.BorderAround(Missing.Value, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic,
                ColorTranslator.ToOle(Color.FromArgb(255, 192, 0)));

            var footerRow = endRow + 4;

            xlWorkSheet.Cells[footerRow, 2] = "PENYUSUN";
            var penyusunCell = xlWorkSheet.Range["b" + footerRow, "d" + footerRow];

            xlWorkSheet.Cells[footerRow, 5] = "PENELITI";
            var penelitiCell = xlWorkSheet.Range["e" + footerRow, "g" + footerRow];

            xlWorkSheet.Cells[footerRow, 8] = "KETERANGAN";
            var ketCell = xlWorkSheet.Range["j" + footerRow, "l" + footerRow];

            footerRow = footerRow + 1;
            //            xlWorkSheet.Cells[footerRow, 2] = "Jakarta, _____________________";


            xlWorkSheet.Cells[footerRow, 2] = "Jakarta, " +
                                              DateTime.Now.ToString(Format, CultureInfo.CreateSpecificCulture("id-ID"));
            var jakartaCell = xlWorkSheet.Range["b" + footerRow, "d" + footerRow];

            footerRow = footerRow + 2;
            xlWorkSheet.Cells[footerRow, 2] = "Tanda Tangan";
            var ttCell = xlWorkSheet.Range["b" + footerRow, "d" + footerRow];

            xlWorkSheet.Cells[footerRow, 5] = "Tanda Tangan";
            var ttPenelitiCell = xlWorkSheet.Range["e" + footerRow, "g" + footerRow];


            footerRow = footerRow + 5;
            //            xlWorkSheet.Cells[footerRow, 2] = "[____________________________]";
            xlWorkSheet.Cells[footerRow, 2] = "[ " + TbPenyusun.Text + " ]";
            var ulCell = xlWorkSheet.Range["b" + footerRow, "d" + footerRow];

            //            xlWorkSheet.Cells[footerRow, 5] = "[____________________________]";
            xlWorkSheet.Cells[footerRow, 5] = "[ " + TbPeneliti.Text + " ]";
            var ulPenelitiCell = xlWorkSheet.Range["e" + footerRow, "g" + footerRow];

            penyusunCell.Merge(Type.Missing);
            jakartaCell.Merge(Type.Missing);
            ttCell.Merge(Type.Missing);
            ulCell.Merge(Type.Missing);

            penelitiCell.Merge(Type.Missing);
            ketCell.Merge(Type.Missing);
            ttPenelitiCell.Merge(Type.Missing);
            ulPenelitiCell.Merge(Type.Missing);

            workBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal,
                null, null, false, false, XlSaveAsAccessMode.xlShared,
                false, false, null, null, null);

            Console.WriteLine("Before cleanup.");
            //Console.ReadKey(false);

            workBook.Close();
            excelApplication.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(formatRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRange);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBooks);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApplication);

            Console.WriteLine("After cleanup.");
            //Console.ReadKey(false);
        }

        private string NamaCabang(IEnumerable<string> cabangList)
        {
            var nmCabang = new List<string>();
            foreach (var cabang in cabangList.Where(cabang => nmCabang != null))
            {
                nmCabang.AddRange(_bpdmhContext.GetTable<Cabang>()
                    .Where(c => c.CabangId == cabang)
                    .Select(c => c.NmCabang));
            }
            return string.Join(", ", nmCabang);
        }
    }
}
