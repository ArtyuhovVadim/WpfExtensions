using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(bool), typeof(bool))]
[MarkupExtensionReturnType(typeof(NotConverter))]
public class NotConverter : BaseConverter<bool, bool>
{
    public override bool Convert(bool value, object? parameter, CultureInfo culture) => !value;

    public override bool ConvertBack(bool value, object? parameter, CultureInfo culture) => !value;
}