using System.Windows.Media;

namespace WpfExtensions.Utils;

public static class ColorUtils
{
    public static Color HsvToRgb(double hue, double saturation, double value)
    {
        double num;
        double num2;
        double num3;
        if (saturation == 0.0)
        {
            num = value;
            num2 = value;
            num3 = value;
        }
        else
        {
            hue = hue != 360.0 ? hue / 60.0 : 0.0;
            var num4 = (int)Math.Truncate(hue);
            var num5 = hue - num4;
            var num6 = value * (1.0 - saturation);
            var num7 = value * (1.0 - saturation * num5);
            var num8 = value * (1.0 - saturation * (1.0 - num5));
            switch (num4)
            {
                case 0:
                    num = value;
                    num2 = num8;
                    num3 = num6;
                    break;
                case 1:
                    num = num7;
                    num2 = value;
                    num3 = num6;
                    break;
                case 2:
                    num = num6;
                    num2 = value;
                    num3 = num8;
                    break;
                case 3:
                    num = num6;
                    num2 = num7;
                    num3 = value;
                    break;
                case 4:
                    num = num8;
                    num2 = num6;
                    num3 = value;
                    break;
                default:
                    num = value;
                    num2 = num6;
                    num3 = num7;
                    break;
            }
        }

        return Color.FromRgb((byte)(num * 255d), (byte)(num2 * 255d), (byte)(num3 * 255d));
    }

    public static HsvColor RgbToHsv(Color color, double hue = 0)
    {
        var num = 0.0;
        double num2 = Math.Min(Math.Min(color.R, color.G), color.B);
        double num3 = Math.Max(Math.Max(color.R, color.G), color.B);
        var num4 = num3 - num2;
        var num5 = num3 != 0.0 ? num4 / num3 : 0.0;

        if (num5 == 0.0)
        {
            num = hue;
        }
        else
        {
            if (color.R == num3)
            {
                num = (color.G - color.B) / num4;
            }
            else if (color.G == num3)
            {
                num = 2.0 + (color.B - color.R) / num4;
            }
            else if (color.B == num3)
            {
                num = 4.0 + (color.R - color.G) / num4;
            }
            num *= 60.0;
            if (num < 0.0)
            {
                num += 360.0;
            }
        }


        return new HsvColor(num, num5, num3 / 255d);
    }

    public static HsvColor ToHsv(this Color color, double hue = 0) => RgbToHsv(color, hue);

    public static Color ToRgb(this HsvColor color) => HsvToRgb(color.Hue, color.Saturation, color.Value);
}