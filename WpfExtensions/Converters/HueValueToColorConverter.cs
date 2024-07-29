using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using WpfExtensions.Converters.Base;
using WpfExtensions.Utils;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(double), typeof(Color))]
[MarkupExtensionReturnType(typeof(HueValueToColorConverter))]
public class HueValueToColorConverter : BaseConverter<double, Color>
{
    public override Color Convert(double value, object? parameter, CultureInfo culture) => ColorUtils.HsvToRgb(value, 1, 1);
}