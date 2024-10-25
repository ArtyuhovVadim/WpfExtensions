using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(object), typeof(IEnumerable<object>))]
[MarkupExtensionReturnType(typeof(ObjectToEnumerableConverter))]
public class ObjectToEnumerableConverter : BaseConverter<object?, IEnumerable<object?>>
{
    public override IEnumerable<object?> Convert(object? value, object? parameter, CultureInfo culture) => [value];

    public override object? ConvertBack(IEnumerable<object?>? value, object? parameter, CultureInfo culture)
    {
        var array = value?.ToArray();
        return array is not { Length: 1 } ? null : array[0];
    }
}