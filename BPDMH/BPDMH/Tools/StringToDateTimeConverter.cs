using System;
using System.Globalization;

namespace BPDMH.Tools
{
    public class StringToDateTimeConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : ((DateTime)value).ToString(parameter as string, CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value as string))
            {
                return null;
            }
            try
            {
                var dt = DateTime.ParseExact(value as string, parameter as string, CultureInfo.InvariantCulture);
                return dt as DateTime?;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
