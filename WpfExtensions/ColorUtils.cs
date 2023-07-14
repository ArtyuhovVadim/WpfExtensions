using System;
using System.Windows.Media;

namespace WpfExtensions;

public static class ColorUtils
{
    public static (double h, double s, double v) ToHsv(Color color)
    {
        var tmpColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

        int max = Math.Max(tmpColor.R, Math.Max(tmpColor.G, tmpColor.B));
        int min = Math.Min(tmpColor.R, Math.Min(tmpColor.G, tmpColor.B));

        var hue = tmpColor.GetHue();
        var saturation = max == 0 ? 0 : 1d - 1d * min / max;
        var value = max / 255d;

        return (hue, saturation, value);
    }

    public static Color FromHsv(double hue, double saturation, double value)
    {
        var hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        var f = hue / 60 - Math.Floor(hue / 60);

        value *= 255;
        var v = Convert.ToByte(value);
        var p = Convert.ToByte(value * (1 - saturation));
        var q = Convert.ToByte(value * (1 - f * saturation));
        var t = Convert.ToByte(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => Color.FromArgb(255, v, t, p),
            1 => Color.FromArgb(255, q, v, p),
            2 => Color.FromArgb(255, p, v, t),
            3 => Color.FromArgb(255, p, q, v),
            4 => Color.FromArgb(255, t, p, v),
            _ => Color.FromArgb(255, v, p, q)
        };
    }
}