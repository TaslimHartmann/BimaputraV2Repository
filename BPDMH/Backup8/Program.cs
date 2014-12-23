using System;
using System.Collections.Generic;
using System.Text;

namespace MyClass.WriteToExcel
{
    public class ExcelWrite : ExcelFileWriter<int>
    {
        public object[,] myExcelData;
        private int myRowCnt;
        public override object[] Headers
        {
            get 
            {
                object[] headerName = { "Header1", "Header2", "Header3", "Header4" };
                return headerName;
            }
        }

        public override void FillRowData(List<int> list)
        {
            
            myRowCnt = list.Count;
            myExcelData = new object[RowCount + 1, 4];
            for (int row = 1; row <= myRowCnt; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    myExcelData[row, col] = list[row - 1];
                }
            }
        }

        public override object[,] ExcelData
        {
            get 
            {
                return myExcelData;
            }
        }

        public override int ColumnCount
        {
            get 
            {
                return 4;
            }
        }

        public override int RowCount
        {
            get 
            {
                return myRowCnt;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<int> myList = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                myList.Add(i);
            }
            ExcelFileWriter<int> myExcel = new ExcelWrite();
            myExcel.WriteDateToExcel(@"C:\TEMP\myExcel.xls",myList,"A1","D1");
        }
    }
        
}
