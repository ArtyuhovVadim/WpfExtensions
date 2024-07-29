using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(double), typeof(double))]
[MarkupExtensionReturnType(typeof(LinearConverter))]
public class LinearConverter : BaseConverter<double, double>
{
    public LinearConverter() { }

    public LinearConverter(double k) => K = k;

    public LinearConverter(double k, double b) : this(k) => B = b;

    [ConstructorArgument("K")] public double K { get; set; } = 1;

    [ConstructorArgument("B")] public double B { get; set; }

    public override double Convert(double value, object? parameter, CultureInfo c) => K * value + B;

    public override double ConvertBack(double value, object? parameter, CultureInfo c) => (value - B) / K;
}