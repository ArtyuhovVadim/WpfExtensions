using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System;

namespace WpfExtensions.Converters;

public class IntToStringConverter : BaseConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString() ?? string.Empty;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string str) return value;

        return int.TryParse(Regex.Replace(str, "[^.0-9]", ""), out var result) ? result : 0;
    }
}