using System;
using System.Globalization;
using System.Windows.Data;

namespace Advanced3DVConfig.View.Converters
{
    internal class OnOffConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolToReturn;
            if ((value is int) && ((int) value > 0))
                boolToReturn = true;
            else
                boolToReturn = false;
            return (parameter.ToString() == "invert") ? !boolToReturn : boolToReturn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intToReturn;
            if ((value is bool) && ((bool) value) == true)
                intToReturn = 1;
            else
                intToReturn = 0;
            if (parameter.ToString() == "invert")
                intToReturn = (intToReturn == 0) ? 1 : 0;
            return intToReturn;
        }
    }
}
