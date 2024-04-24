using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(Color), typeof(string))]
[MarkupExtensionReturnType(typeof(ColorToStringConverter))]
public partial class ColorToStringConverter : BaseConverter
{
    public bool IsTransparencySupported { get; set; }

    public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Color color)
            return Binding.DoNothing;

        var colorStr = color.ToString();

        if (!IsTransparencySupported)
            colorStr = colorStr.Remove(1, 2);

        return colorStr;
    }

    public override object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string str)
            return Binding.DoNothing;

        var matchResult = IsTransparencySupported
            ? RegexWithTransparency().Match(str)
            : RegexWithoutTransparency().Match(str);

        if (!matchResult.Success)
            return DependencyProperty.UnsetValue;

        return (Color)ColorConverter.ConvertFromString(str)!;
    }

    [GeneratedRegex("^#[0-9A-Fa-f]{8}$")]
    private static partial Regex RegexWithTransparency();

    [GeneratedRegex("^#[0-9A-Fa-f]{6}$")]
    private static partial Regex RegexWithoutTransparency();
}
