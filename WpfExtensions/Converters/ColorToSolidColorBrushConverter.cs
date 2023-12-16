using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(Color), typeof(SolidColorBrush))]
[MarkupExtensionReturnType(typeof(ColorToSolidColorBrushConverter))]
public class ColorToSolidColorBrushConverter : BaseConverter
{
    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => new SolidColorBrush((Color)value!);

    public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => ((SolidColorBrush)value!).Color;
};