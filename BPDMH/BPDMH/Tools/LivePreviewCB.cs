using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BPDMH.Tools
{
    public class LivePreviewCb : ComboBox
    {
        public static readonly DependencyProperty LivePreviewItemProperty =
            DependencyProperty.Register("LivePreviewItem", typeof(object), typeof(LivePreviewCb),
            new FrameworkPropertyMetadata(null));

        public event EventHandler IsDropDownOpenChanged;

        public LivePreviewCb()
        {
            var dpd = DependencyPropertyDescriptor.FromProperty(IsDropDownOpenProperty, typeof(LivePreviewCb));
            dpd.AddValueChanged(this, OnDropDownOpenChanged);
        }

        public object LivePreviewItem
        {
            get { return GetValue(LivePreviewItemProperty); }
            set { SetValue(LivePreviewItemProperty, value); }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var container = base.GetContainerForItemOverride();
            var comboBoxItem = container as ComboBoxItem;
            if (comboBoxItem != null)
            {
                DependencyPropertyDescriptor.FromProperty(ComboBoxItem.IsHighlightedProperty, typeof(ComboBoxItem))
                    .AddValueChanged(comboBoxItem, OnItemHighlighted);
            }
            return container;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            LivePreviewItem = SelectedItem;
            base.OnSelectionChanged(e);
        }

        private void OnItemHighlighted(object sender, EventArgs e)
        {
            var comboBoxItem = sender as ComboBoxItem;
            if (comboBoxItem != null && comboBoxItem.IsHighlighted)
            {
                LivePreviewItem = comboBoxItem.DataContext;
            }
        }

        private void OnDropDownOpenChanged(object sender, EventArgs e)
        {
            if (!IsDropDownOpen)
            {
                LivePreviewItem = SelectedItem;
            }

            if (IsDropDownOpenChanged != null)
            {
                IsDropDownOpenChanged(this, EventArgs.Empty);
            }
        }
    }
}
