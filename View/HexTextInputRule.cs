using System;
using System.Globalization;
using System.Windows.Controls;

namespace Advanced3DVConfig.View
{
    class HexTextInputRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "Value must be input");
            if (value.ToString().Length < 4)
                return new ValidationResult(false, "Four-character hex keycode combination is required");
            try {
                Int32.Parse(value.ToString(), NumberStyles.HexNumber);
            }
            catch (Exception) {
                return new ValidationResult(false, "Invalid hex value");
            }
            return ValidationResult.ValidResult;
        }
    }
}
