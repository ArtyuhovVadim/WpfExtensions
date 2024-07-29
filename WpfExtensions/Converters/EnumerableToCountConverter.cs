using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(IEnumerable<>), typeof(int))]
[MarkupExtensionReturnType(typeof(EnumerableToCountConverter))]
public class EnumerableToCountConverter : BaseConverter<IEnumerable<object>, int>
{
    public override int Convert(IEnumerable<object>? value, object? parameter, CultureInfo culture) => value?.Count() ?? 0;
}