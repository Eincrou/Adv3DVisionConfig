using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Advanced3DVConfig.ViewModel
{
    class HexToKeyboardConverter : IValueConverter
    {
        private readonly Dictionary<String, String> modifierKeys = new Dictionary<string, string>()
        {
            {"00", "NONE"},
            {"01", "SHIFT"},
            {"02", "CTRL"},
            {"03", "CTRL+SHIFT"},
            {"04", "ALT"},
            {"05", "ALT+SHIFT"},
            {"06", "ALT+CTRL"},
            {"07", "ALT+CTRL+SHIFT"},
            {"08", "WIN"},
            {"09", "SHIFT+WIN"},
            {"0A", "CTRL+WIN"},
            {"0B", "CTRL+SHIFT+WIN"},
            {"0C", "ALT+WIN"},
            {"0D", "ALT+SHIFT+WIN"},
            {"0E", "ALT+CTRL+WIN"},
            {"0F", "ALT+CTRL+SHIFT+WIN"},
        };

        private readonly string[] mouseButtons = new string[]
        {
            "MOUSE LEFT", "MOUSE RIGHT", "INVALID", "MOUSE MIDDLE", "MOUSE BACK", "MOUSE FORWARD"
        };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            if (s != null && s.Length == 4)
            {

                string first = s.Substring(0, 2);
                string second = s.Substring(2, 2);
                if (Int32.Parse(first, NumberStyles.HexNumber) > 15)
                    return "modifier error";
                string keyFirst = modifierKeys[first.ToUpperInvariant()];

                int keySecondInt = Int32.Parse((string) second, NumberStyles.HexNumber);
                string keySecond;
                if (keySecondInt >7)
                    keySecond = KeyInterop.KeyFromVirtualKey(keySecondInt).ToString();
                else if (keySecondInt < 7 && keySecondInt > 0)
                    keySecond = mouseButtons[keySecondInt - 1];
                else
                    keySecond = "invalid key";
                
                return String.Format("{0} + {1}",keyFirst, keySecond);
            }
            return "error";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
