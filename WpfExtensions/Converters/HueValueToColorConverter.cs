﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using WpfExtensions.Converters.Base;
using WpfExtensions.Utils;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(double), typeof(Color))]
[MarkupExtensionReturnType(typeof(HueValueToColorConverter))]
public class HueValueToColorConverter : BaseConverter
{
    public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double d)
        {
            return ColorUtils.HsvToRgb(d, 1, 1);
        }

        return Binding.DoNothing;
    }
}