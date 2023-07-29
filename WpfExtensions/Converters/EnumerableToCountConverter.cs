using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(IEnumerable<>), typeof(int))]
[MarkupExtensionReturnType(typeof(EnumerableToCountConverter))]
public class EnumerableToCountConverter : BaseConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not IEnumerable<object> enumerable)
            return Binding.DoNothing;

        return enumerable.Count();
    }
}