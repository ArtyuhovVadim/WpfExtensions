using System.Globalization;
using System.Text.RegularExpressions;
using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(byte), typeof(string))]
[MarkupExtensionReturnType(typeof(ByteToStringConverter))]
public partial class ByteToStringConverter : BaseConverter
{
    [GeneratedRegex("[^.0-9]")]
    private static partial Regex IsDigitRegex();

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString() ?? string.Empty;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string str) return value;

        var res = int.TryParse(IsDigitRegex().Replace(str, ""), out var result) ? result : 0;

        return res switch
        {
            > 255 => 255,
            < 0 => 0,
            _ => res
        };
    }
}