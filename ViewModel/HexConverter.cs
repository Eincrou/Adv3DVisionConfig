using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Advanced3DVConfig.ViewModel
{
    internal class HexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value != null) && (value is int))
                return String.Format("{0:X4}", (int) value);
            else
                return "Invalid int to hex conversion.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value != null) && (value is String))
                return Int32.Parse((string) value, NumberStyles.HexNumber);
            else
                throw new ArgumentException("Value to ConvertBack must be a string", "value");
        }
    }
}
