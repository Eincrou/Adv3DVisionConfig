using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Advanced3DVConfig.Model;

namespace Advanced3DVConfig.ViewModel
{
    class SettingDefaultToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int inputValue = -1;
            if (parameter == null)
                throw new ArgumentException(
                    "A parameter representing the name of the Stereo3DKeyName must be supplied", "parameter");
            string keyName = (string) parameter;
            if (value is int)
                inputValue = (int) value;
            else if (value is string)
            {
                if (!Int32.TryParse((string) value, NumberStyles.HexNumber, null, out inputValue))
                    return true;
            }
            else if (value is bool){
                inputValue = (bool) value ? 1 : 0;
                if (keyName == "EnableWindowedMode" && inputValue == 1) inputValue = 5;
            }

            if (Stereo3DKeys.KeySettingsDefaults.ContainsKey(keyName))
                return inputValue != Stereo3DKeys.KeySettingsDefaults[keyName];
            if (Stereo3DKeys.KeySettingsHotkeyDefaults.ContainsKey(keyName))
                return inputValue != Stereo3DKeys.KeySettingsHotkeyDefaults[keyName];
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
