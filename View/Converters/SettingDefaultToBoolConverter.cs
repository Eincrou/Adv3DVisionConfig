using System;
using System.Globalization;
using System.Windows.Data;
using Advanced3DVConfig.Model;

namespace Advanced3DVConfig.View.Converters
{
    class SettingDefaultToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int inputValue = -5;
            if (parameter == null)
                throw new ArgumentException(
                    "A parameter representing the name of the Stereo3DKeyName must be supplied", nameof(parameter));
            string keyName = (string) parameter;
            if (value is int)
                inputValue = (int) value;
            else if (value is string){
                if (!Int32.TryParse((string) value, NumberStyles.HexNumber, null, out inputValue))
                    return true;
            }
            else if (value is bool){
                inputValue = (bool) value ? 1 : 0;
                if (keyName == "EnableWindowedMode" && inputValue == 1) inputValue = 5;
            }
            int defaultvalue = Stereo3DRegistryKeyDefaults.GetDefaultKeyValue(keyName);
            if (defaultvalue >= 0)
                return inputValue != defaultvalue;
            throw new ArgumentException("No Stereo3DKeySetting matches this parameter", nameof(parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
