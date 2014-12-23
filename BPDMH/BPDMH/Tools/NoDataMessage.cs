using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace BPDMH.Tools
{
    public static class NoDataMessage
    {
        private static void DisplayNoData()
        {
            MessageBox.Show("Data tidak ditemukan", "Informasi");
        }
    }
}
