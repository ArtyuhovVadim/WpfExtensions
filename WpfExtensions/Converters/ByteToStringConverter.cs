using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using WpfExtensions.Converters.Base;

namespace WpfExtensions.Converters;

[ValueConversion(typeof(byte), typeof(string))]
[MarkupExtensionReturnType(typeof(ByteToStringConverter))]
public partial class ByteToStringConverter : BaseConverter<byte, string>
{
    [GeneratedRegex("[^.0-9]")]
    private static partial Regex IsDigitRegex();

    public override string Convert(byte value, object? parameter, CultureInfo culture) => value.ToString();

    public override byte ConvertBack(string? value, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return 0;

        var res = int.TryParse(IsDigitRegex().Replace(value, ""), out var result) ? result : 0;

        return res switch
        {
            > 255 => 255,
            < 0 => 0,
            _ => (byte)res
        };
    }
}