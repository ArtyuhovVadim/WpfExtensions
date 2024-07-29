using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(Color), typeof(SolidColorBrush))]
[MarkupExtensionReturnType(typeof(ColorToSolidColorBrushConverter))]
public class ColorToSolidColorBrushConverter : BaseConverter<Color, SolidColorBrush>
{
    public override SolidColorBrush Convert(Color value, object? parameter, CultureInfo culture) => new(value);

    public override Color ConvertBack(SolidColorBrush? value, object? parameter, CultureInfo culture) => value?.Color ?? Colors.Transparent;
};