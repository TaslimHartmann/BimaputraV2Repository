using System;
using System.Windows;

namespace BPDMH
{
    class Program : Application
    {
        [STAThread]
        public static void Main()
        {
// ReSharper disable once ObjectCreationAsStatement
            new Program();
        }

        public Program()
        {
            StartupUri = new Uri("/MasterData/LoginScreen.xaml", UriKind.Relative);
//            StartupUri = new Uri("/Report/DMHReportToExcel.xaml", UriKind.Relative);
            Run(); 
        }
    }
}
