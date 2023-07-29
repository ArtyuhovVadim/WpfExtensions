using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(double), typeof(double))]
[MarkupExtensionReturnType(typeof(LinearConverter))]
public class LinearConverter : BaseConverter
{
    public LinearConverter()
    {
    }

    public LinearConverter(double k)
    {
        K = k;
    }

    public LinearConverter(double k, double b) : this(k)
    {
        B = b;
    }

    [ConstructorArgument("K")] public double K { get; set; } = 1;

    [ConstructorArgument("B")] public double B { get; set; }

    public override object? Convert(object? v, Type t, object? p, CultureInfo c)
    {
        if (v is null) return null;

        var x = System.Convert.ToDouble(v, c);
        return K * x + B;
    }

    public override object? ConvertBack(object? v, Type t, object? p, CultureInfo c)
    {
        if (v is null) return null;

        var y = System.Convert.ToDouble(v, c);
        return (y - B) / K;
    }
}