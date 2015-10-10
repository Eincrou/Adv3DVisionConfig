using System;
using System.Globalization;
using System.Windows.Data;

namespace Advanced3DVConfig.View.Converters
{
    internal class HexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is int))
                return String.Format("{0:X4}", (int) value);
            throw new ArgumentException("Value to Convert must be an integer", "value");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is String))
                return Int32.Parse((string) value, NumberStyles.HexNumber);
            throw new ArgumentException("Value to ConvertBack must be a string", "value");
        }
    }
}
