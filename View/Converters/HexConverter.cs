using System;
using System.Globalization;
using System.Windows.Data;

namespace Advanced3DVConfig.View.Converters
{
    internal class HexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is uint)
                return $"{value:X4}";
            throw new ArgumentException("Value to Convert must be an unsigned integer", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            if ((s != null))
                return UInt32.Parse(s, NumberStyles.HexNumber);
            throw new ArgumentException("Value to ConvertBack must be a string", nameof(value));
        }
    }
}
