using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(object), typeof(Visibility))]
[MarkupExtensionReturnType(typeof(NullToVisibilityConverter))]
public class NullToVisibilityConverter : BaseConverter
{
    public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is null ? Visibility.Collapsed : Visibility.Visible;
}