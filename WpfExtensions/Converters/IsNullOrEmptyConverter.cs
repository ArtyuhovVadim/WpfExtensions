﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(string), typeof(bool))]
[MarkupExtensionReturnType(typeof(IsNullOrEmptyConverter))]
public class IsNullOrEmptyConverter : BaseConverter<string, bool>
{
    public override bool Convert(string? value, object? parameter, CultureInfo culture) => string.IsNullOrEmpty(value);
}