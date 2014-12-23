using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using tool = Microsoft.Windows.Controls;
using System.ComponentModel;

namespace ExportToExcel
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Employees emps = new Employees();

            Employee e1 = new Employee();
            e1.Id = 52590;
            e1.Name = "Sathish";
            e1.Designation = "Developer";            
            emps.Add(e1);

            Employee e2 = new Employee();
            e2.Id = 52592;
            e2.Name = "Karthick";
            e2.Designation = "Developer";            
            emps.Add(e2);

            Employee e3 = new Employee();
            e3.Id = 52593;
            e3.Name = "Raja";
            e3.Designation = "Manager";            
            emps.Add(e3);

            Employee e4 = new Employee();
            e4.Id = 12778;
            e4.Name = "Sumesh";
            e4.Designation = "Project Lead";            
            emps.Add(e4);

            Employee e5 = new Employee();
            e5.Id = 12590;
            e5.Name = "Srini";
            e5.Designation = "Project Lead";            
            emps.Add(e5);
            
            dgEmployee.ItemsSource = emps;

            Books books = new Books();

            Book b1 = new Book();
            b1.ISBN = 582912;
            b1.Title = "C#";
            b1.Author = "James";
            books.Add(b1);

            Book b2 = new Book();
            b2.ISBN = 174290;
            b2.Title = "WPF";
            b2.Author = "Smith";
            books.Add(b2);

            Book b3 = new Book();
            b3.ISBN = 095177;
            b3.Title = ".NET";
            b3.Author = "Robert";
            books.Add(b3);

            Book b4 = new Book();
            b4.ISBN = 112275;
            b4.Title = "Java";
            b4.Author = "Steve";
            books.Add(b4);

            Book b5 = new Book();
            b5.ISBN = 998721;
            b5.Title = "COBOL";
            b5.Author = "John";
            books.Add(b5);

            dgBook.ItemsSource = books;

        }         
        /// <summary>
        /// Event for generating excel sheet for books datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<Book, Books> s = new ExportToExcel<Book, Books>();            
            ICollectionView view = CollectionViewSource.GetDefaultView(dgBook.ItemsSource);            
            s.dataToPrint = (Books)view.SourceCollection;
            s.GenerateReport();
        }
        /// <summary>
        /// Event for generating excel sheet for employee datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<Employee, Employees> s = new ExportToExcel<Employee, Employees>();
            s.dataToPrint = (Employees)dgEmployee.ItemsSource;
            s.GenerateReport();
        }
    }
    
    /// <summary>
    /// Class for generator of Excel file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class ExportToExcel<T, U>
        where T : class
        where U : List<T>
    {
        public List<T> dataToPrint;
        // Excel object references.
        private Excel.Application _excelApp = null;
        private Excel.Workbooks _books = null;
        private Excel._Workbook _book = null;
        private Excel.Sheets _sheets = null;
        private Excel._Worksheet _sheet = null;
        private Excel.Range _range = null;
        private Excel.Font _font = null;
        // Optional argument variable
        private object _optionalValue = Missing.Value;

        /// <summary>
        /// Generate report and sub functions
        /// </summary>
        public void GenerateReport()
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        CreateExcelRef();
                        FillSheet();
                        OpenReport();
                        Mouse.SetCursor(Cursors.Arrow);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while generating Excel report");
            }
            finally
            {
                ReleaseObject(_sheet);
                ReleaseObject(_sheets);
                ReleaseObject(_book);
                ReleaseObject(_books);
                ReleaseObject(_excelApp);
            }
        }
        /// <summary>
        /// Make MS Excel application visible
        /// </summary>
        private void OpenReport()
        {
            _excelApp.Visible = true;
        }
        /// <summary>
        /// Populate the Excel sheet
        /// </summary>
        private void FillSheet()
        {
            object[] header = CreateHeader();
            WriteData(header);
        }
        /// <summary>
        /// Write data into the Excel sheet
        /// </summary>
        /// <param name="header"></param>
        private void WriteData(object[] header)
        {
            object[,] objData = new object[dataToPrint.Count, header.Length];

            for (int j = 0; j < dataToPrint.Count; j++)
            {
                var item = dataToPrint[j];
                for (int i = 0; i < header.Length; i++)
                {
                    var y = typeof(T).InvokeMember(header[i].ToString(), BindingFlags.GetProperty, null, item, null);
                    objData[j, i] = (y == null) ? "" : y.ToString();
                }
            }
            AddExcelRows("A2", dataToPrint.Count, header.Length, objData);
            AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);
        }
        /// <summary>
        /// Method to make columns auto fit according to data
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }
        /// <summary>
        /// Create header from the properties
        /// </summary>
        /// <returns></returns>
        private object[] CreateHeader()
        {
            PropertyInfo[] headerInfo = typeof(T).GetProperties();

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < headerInfo.Length; n++)
            {
                objHeaders.Add(headerInfo[n].Name);
            }

            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();

            return headerToAdd;
        }
        /// <summary>
        /// Set Header style as bold
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }
        /// <summary>
        /// Method to add an excel rows
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows(string startRange, int rowCount, int colCount, object values)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }       
        /// <summary>
        /// Create Excel applicaiton parameters instances
        /// </summary>
        private void CreateExcelRef()
        {
            _excelApp = new Excel.Application();
            _books = (Excel.Workbooks)_excelApp.Workbooks;
            _book = (Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Excel.Sheets)_book.Worksheets;
            _sheet = (Excel._Worksheet)(_sheets.get_Item(1));
        }
        /// <summary>
        /// Release unused COM objects
        /// </summary>
        /// <param name="obj"></param>
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
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }

    /// <summary>
    /// List of Employee Class 
    /// </summary>
    public class Employees : List<Employee> { }
    /// <summary>
    /// Employee Class
    /// </summary>
    public class Employee
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string designation;

        public string Designation
        {
            get { return designation; }
            set { designation = value; }
        }

    }

    /// <summary>
    /// List of Book class
    /// </summary>
    public class Books : List<Book> { }

    /// <summary>
    /// Book Class
    /// </summary>
    public class Book
    {
        private int iSBN;

        public int ISBN
        {
            get { return iSBN; }
            set { iSBN = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
    }

}
