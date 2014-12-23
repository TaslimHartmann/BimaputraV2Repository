using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BPDMH.Tools
{
    /// <summary>
    /// Interaction logic for AutoCompleteCB.xaml
    /// </summary>
    public partial class AutoCompleteCb : Grid
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(AutoCompleteCb));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(String), typeof(AutoCompleteCb));

        public static readonly DependencyProperty CaseSensitiveProperty =
            DependencyProperty.Register("CaseSensitive", typeof(bool), typeof(AutoCompleteCb),
            new PropertyMetadata(false));

        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(int), typeof(AutoCompleteCb),
            new PropertyMetadata(150));


        private bool _updating = false;

        public AutoCompleteCb()
        {
            InitializeComponent();
            cbx.IsDropDownOpenChanged += cbx_IsDropDownOpenChanged;
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public String SelectedItem
        {
            get { return (String)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public bool CaseSensitive
        {
            get { return (bool)GetValue(CaseSensitiveProperty); }
            set { SetValue(CaseSensitiveProperty, value); }
        }

        public int MaxDropDownHeight
        {
            get { return (int)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        private void cbx_IsDropDownOpenChanged(object sender, EventArgs e)
        {
            if (cbx.IsDropDownOpen)
                return;

            _updating = true;
            try
            {
                // reset the combobox selection once the drop down closes
                cbx.SelectedItem = null;
                tbx.SelectionStart = tbx.Text.Length;
                tbx.Focus();
            }
            finally
            {
                _updating = false;
            }
        }

        private void Tbx_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_updating)
                return;

            UpdateFilter();
        }

        private void Cbx_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (cbx.IsDropDownOpen &&
                    cbx.Items.Count > 0)
                {
                    var firstItem = cbx.Items[0];
                    if (cbx.LivePreviewItem == firstItem)
                    {
                        _updating = true;
                        try
                        {
                            cbx.SelectedIndex = -1;
                            tbx.Focus();

                            cbx.IsDropDownOpen = (cbx.Items.Filter != null);
                        }
                        finally
                        {
                            _updating = false;
                        }
                    }
                }
            }
            else if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                if (cbx.LivePreviewItem != null)
                {
                    cbx.SelectedItem = cbx.LivePreviewItem;
                    tbx.SelectionStart = tbx.Text.Length;
                    cbx.IsDropDownOpen = false;
                    tbx.Focus();
                    e.Handled = true;
                }
            }
        }

        private void UpdateFilter()
        {
            if (tbx.Text != String.Empty && HasMatchingItems())
            {
                _updating = true;
                try
                {
                    cbx.IsDropDownOpen = true;
                    SetComboboxFilter();
                    tbx.Focus();
                }
                finally
                {
                    _updating = false;
                }
            }
            else
            {
                cbx.IsDropDownOpen = false;
                cbx.Items.Filter = null;
            }
        }

        private void SetComboboxFilter()
        {
            if (CaseSensitive)
            {
                cbx.Items.Filter = value => value.ToString().StartsWith(tbx.Text);
            }
            else
            {
                var lowerText = tbx.Text.ToLower();
                cbx.Items.Filter = value => value.ToString().ToLower().StartsWith(lowerText);
            }
        }

        private bool HasMatchingItems()
        {
            if (CaseSensitive)
            {
                return cbx.Items.SourceCollection.Cast<object>()
                    .Where(_1 => _1.ToString().StartsWith(tbx.Text)).Count() > 0;
            }
            else
            {
                var lowerText = tbx.Text.ToLower();
                return cbx.Items.SourceCollection.Cast<object>().Where
                    (
                        _1 => _1.ToString().ToLower().StartsWith(lowerText)
                    ).Count() > 0;
            }
        }

        private void Cbx_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_updating)
                return;

            _updating = true;
            try
            {
                tbx.Text = cbx.SelectedItem != null ? cbx.SelectedItem.ToString() : String.Empty;
            }
            finally
            {
                _updating = false;
            }
        }

        private void Tbx_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (cbx.IsDropDownOpen &&
                    cbx.Items.Count > 0)
                {
                    var firstItem = cbx.Items[0];
                    if (cbx.LivePreviewItem == firstItem)
                    {
                        _updating = true;
                        try
                        {
                            cbx.SelectedIndex = -1;
                            tbx.Focus();

                            cbx.IsDropDownOpen = (cbx.Items.Filter != null);
                        }
                        finally
                        {
                            _updating = false;
                        }
                    }
                }
            }
            else if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                if (cbx.LivePreviewItem != null)
                {
                    cbx.SelectedItem = cbx.LivePreviewItem;
                    tbx.SelectionStart = tbx.Text.Length;
                    cbx.IsDropDownOpen = false;
                    tbx.Focus();
                    e.Handled = true;
                }
            }   
        }

    }
}
