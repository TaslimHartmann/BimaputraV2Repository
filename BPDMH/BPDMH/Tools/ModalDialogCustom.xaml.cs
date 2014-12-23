using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace BPDMH.Tools
{
    /// <summary>
    /// Interaction logic for ModalDialogCustom.xaml
    /// http://www.codeproject.com/Articles/36516/WPF-Modal-Dialog
    /// </summary>
    public partial class ModalDialogCustom
    {
        public ModalDialogCustom()
		{
			InitializeComponent();
			Visibility = Visibility.Hidden;
            TglPickerLookup.SelectedDate = DateTime.Today;
		}

		private bool _hideRequest = false;
		private bool _result = false;
		private UIElement _parent;

		public void SetParent(UIElement parent)
		{
			_parent = parent;
		}

		#region Message

		public string Message
		{
			get { return (string)GetValue(MessageProperty); }
			set { SetValue(MessageProperty, value); }
		}

        public string NoSp
        {
            get { return (string)GetValue(NoSpProperty); }
            set { SetValue(NoSpProperty, value); }
        }

        public static readonly DependencyProperty NoSpProperty =
            DependencyProperty.Register(
                "NoSP", typeof(string), typeof(ModalDialogCustom), new UIPropertyMetadata(string.Empty));

        public DateTime? TglKirim
        {
            get { return (DateTime?)GetValue(TglKirimProperty); }
            set { SetValue(TglKirimProperty, value); }
        }

        public static readonly DependencyProperty TglKirimProperty =
            DependencyProperty.Register(
                "TglKirim", typeof(DateTime?), typeof(ModalDialogCustom), new UIPropertyMetadata(DateTime.MinValue));

		// Using a DependencyProperty as the backing store for Message.
		// This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MessageProperty =
			DependencyProperty.Register(
                "Message", typeof(string), typeof(ModalDialogCustom), new UIPropertyMetadata(string.Empty));

		#endregion

		public bool ShowHandlerDialog(string message)
		{
			Message = message;
			Visibility = Visibility.Visible;

			_parent.IsEnabled = false;

			_hideRequest = false;
			while (!_hideRequest)
			{
				// HACK: Stop the thread if the application is about to close
				if (this.Dispatcher.HasShutdownStarted ||
					this.Dispatcher.HasShutdownFinished)
				{
					break;
				}

				// HACK: Simulate "DoEvents"
				this.Dispatcher.Invoke(
					DispatcherPriority.Background,
					new ThreadStart(delegate { }));
				Thread.Sleep(20);
			}

			return _result;
		}
		
		private void HideHandlerDialog()
		{
			_hideRequest = true;
			Visibility = Visibility.Hidden;
			_parent.IsEnabled = true;
		}
        
        private void BtnCari_OnClick(object sender, RoutedEventArgs e)
        {
            _result = true;
            NoSp = TbNoSpLookup.Text;
            TglKirim = TglPickerLookup.SelectedDate;
            HideHandlerDialog();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            _result = false;
            HideHandlerDialog();
        }
    }
}
