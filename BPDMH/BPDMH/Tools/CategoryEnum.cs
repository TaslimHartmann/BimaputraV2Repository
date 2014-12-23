using System;
using System.Windows.Data;

namespace BPDMH.Tools
{
    public enum CategoryEnum
    {
        Pengirim,
        Penerima
    }

    public class Category
    {
        public CategoryEnum EnumProperty { get; set; }
        public bool BooleanProperty { get; set; }
    }

    public class RadioButtonCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}