using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(object), typeof(Visibility))]
[MarkupExtensionReturnType(typeof(NullToVisibilityConverter))]
public class NullToVisibilityConverter : BaseConverter<object, Visibility>
{
    public override Visibility Convert(object? value, object? parameter, CultureInfo culture) => value is null ? Visibility.Collapsed : Visibility.Visible;
}