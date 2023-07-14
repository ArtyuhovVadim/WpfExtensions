using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(Color), typeof(SolidColorBrush))]
[MarkupExtensionReturnType(typeof(ColorToBrushConverter))]
public class ColorToBrushConverter : BaseConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Color color) return Binding.DoNothing;

        return new SolidColorBrush(color);
    }
}