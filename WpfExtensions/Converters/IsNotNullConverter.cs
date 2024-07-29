using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(object), typeof(bool))]
[MarkupExtensionReturnType(typeof(IsNotNullConverter))]
public class IsNotNullConverter : BaseConverter<object, bool>
{
    public override bool Convert(object? value, object? parameter, CultureInfo culture) => value is not null;
}