using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using BPDMH.DataSet;
using BPDMH.Splash;

namespace BPDMH.MasterData
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen
    {
        private DCBPDMHDataContext _bpdmhContext;

        public LoginScreen()
        {
            InitializeComponent();
            GetStatusLookup();
            TxtName.Focus();
        }

        private void GetStatusLookup()
        {
            var statusList = new List<string> { "Operator", "Administratior" };
            CboStatus.ItemsSource = statusList;
            CboStatus.SelectedIndex = 0;
        }

        private void SetAuthentication()
        {
            _bpdmhContext = new DCBPDMHDataContext();
            var user = _bpdmhContext.GetTable<Karyawan>()
                .Where(k => k.Nama == TxtName.Text
                && k.Password == TxtPassword.Password
                && k.Status == CboStatus.SelectedIndex).ToList();
            if (user.Count == 0)
                MessageBox.Show("Periksa user name atau password ", "Information");
            else
            {
                //Close();
                Hide();
                CallSplahScreen(CboStatus.SelectedIndex);
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            SetAuthentication();
        }

        private void CallSplahScreen(int status)
        {
            Splasher.Splash = new Splash.SplashScreen();
            Splasher.ShowSplash();

            for (var i = 0; i < 400; i++)
            {
                MessageListener.Instance.ReceiveMessage(string.Format("Load module {0}", i));
                Thread.Sleep(1);
            }

            Splasher.CloseSplash();
            var newMasterPage = new MasterPage();
            newMasterPage.MenuMaster.IsEnabled = status != 0;
            newMasterPage.MenuTransaksi.IsEnabled = true;
            newMasterPage.MenuLaporan.IsEnabled = true;
            newMasterPage.LblLogo.Visibility = Visibility.Visible;
            newMasterPage.MenuLogin.IsEnabled = false;
            newMasterPage.Show();
            Close();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CboStatus_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetAuthentication();
            }
        }
    }
}
