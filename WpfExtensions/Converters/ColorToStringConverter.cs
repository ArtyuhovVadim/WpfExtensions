using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(Color), typeof(string))]
[MarkupExtensionReturnType(typeof(ColorToStringConverter))]
public class ColorToStringConverter : BaseConverter
{
    private readonly Regex _regexWithTransparency = new("#[0-9A-Fa-f]{8}$");
    private readonly Regex _regexWithoutTransparency = new("#[0-9A-Fa-f]{6}$");

    public bool IsTransparencySupported { get; set; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Color color)
            return Binding.DoNothing;

        var colorStr = color.ToString();

        if (!IsTransparencySupported)
            colorStr = colorStr.Remove(1, 2);

        return colorStr;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string str)
            return Binding.DoNothing;

        var matchResult = IsTransparencySupported ? _regexWithTransparency.Match(str) : _regexWithoutTransparency.Match(str);

        if (!matchResult.Success)
            return Binding.DoNothing;

        return (Color)ColorConverter.ConvertFromString(str)!;
    }
}