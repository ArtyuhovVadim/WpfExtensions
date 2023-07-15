using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker;

public class ComplexColor : INotifyPropertyChanged
{
    public static ComplexColor Black => new(Colors.Black);

    public static ComplexColor White => new(Colors.White);

    public event PropertyChangedEventHandler PropertyChanged;

    public ComplexColor(Color color) => Color = color;

    private double _a;

    private double _r;
    private double _g;
    private double _b;

    private double _h;
    private double _s;
    private double _v;

    public Color Color
    {
        get => Color.FromArgb((byte)(_a * 255d), (byte)(_r * 255d), (byte)(_g * 255d), (byte)(_b * 255d));
        set
        {
            _r = value.R / 255d;
            _g = value.G / 255d;
            _b = value.B / 255d;
            _a = value.A / 255d;

            RecalculateHsvFromRgb();

            OnPropertyChanged(nameof(Red));
            OnPropertyChanged(nameof(Green));
            OnPropertyChanged(nameof(Blue));
            OnPropertyChanged(nameof(Alpha));
        }
    }

    public byte Red
    {
        get => (byte)(_r * 255d);
        set
        {
            _r = value / 255d;
            RecalculateHsvFromRgb();
            OnPropertyChanged();
        }
    }

    public byte Green
    {
        get => (byte)(_g * 255d);
        set
        {
            _g = value / 255d;
            RecalculateHsvFromRgb();
            OnPropertyChanged();
        }
    }

    public byte Blue
    {
        get => (byte)(_b * 255d);
        set
        {
            _b = value / 255d;
            RecalculateHsvFromRgb();
            OnPropertyChanged();
        }
    }

    public byte Alpha
    {
        get => (byte)(_a * 255d);
        set
        {
            _a = value / 255d;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Color));
        }
    }

    public double Hue
    {
        get => _h;
        set
        {
            _h = value;
            OnPropertyChanged();
            RecalculateRgbFromHsv();
        }
    }

    public double Saturation
    {
        get => _s;
        set
        {
            _s = value;
            OnPropertyChanged();
            RecalculateRgbFromHsv();
        }
    }

    public double Value
    {
        get => _v;
        set
        {
            _v = value;
            OnPropertyChanged();
            RecalculateRgbFromHsv();
        }
    }

    private void RecalculateHsvFromRgb()
    {
        (_h, _s, _v) = ConvertRgbToHsv(_r, _g, _b);

        OnPropertyChanged(nameof(Saturation));
        OnPropertyChanged(nameof(Value));
        OnPropertyChanged(nameof(Hue));

        OnPropertyChanged(nameof(Color));
    }

    private void RecalculateRgbFromHsv()
    {
        (_r, _g, _b) = ConvertHsvToRgb(_h, _s, _v);

        OnPropertyChanged(nameof(Red));
        OnPropertyChanged(nameof(Green));
        OnPropertyChanged(nameof(Blue));

        OnPropertyChanged(nameof(Color));
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private (double h, double s, double v) ConvertRgbToHsv(double r1, double g1, double b1)
    {
        double r = r1 * 255d;
        double g = g1 * 255d;
        double b = b1 * 255d;

        double num = 0.0;
        double num2 = Math.Min(Math.Min(r, g), b);
        double num3 = Math.Max(Math.Max(r, g), b);
        double num4 = num3 - num2;
        double num5 = ((num3 != 0.0) ? (num4 / num3) : 0.0);
        if (num5 == 0.0)
        {
            num = _h;
        }
        else
        {
            if (r == num3)
            {
                num = (g - b) / num4;
            }
            else if (g == num3)
            {
                num = 2.0 + (b - r) / num4;
            }
            else if (b == num3)
            {
                num = 4.0 + (r - g) / num4;
            }
            num *= 60.0;
            if (num < 0.0)
            {
                num += 360.0;
            }
        }


        return (num, num5, num3 / 255.0);
    }

    private (double r, double g, double b) ConvertHsvToRgb(double h, double s, double v)
    {
        double num = 0.0;
        double num2 = 0.0;
        double num3 = 0.0;
        if (s == 0.0)
        {
            num = v;
            num2 = v;
            num3 = v;
        }
        else
        {
            h = ((h != 360.0) ? (h / 60.0) : 0.0);
            int num4 = (int)Math.Truncate(h);
            double num5 = h - num4;
            double num6 = v * (1.0 - s);
            double num7 = v * (1.0 - s * num5);
            double num8 = v * (1.0 - s * (1.0 - num5));
            switch (num4)
            {
                case 0:
                    num = v;
                    num2 = num8;
                    num3 = num6;
                    break;
                case 1:
                    num = num7;
                    num2 = v;
                    num3 = num6;
                    break;
                case 2:
                    num = num6;
                    num2 = v;
                    num3 = num8;
                    break;
                case 3:
                    num = num6;
                    num2 = num7;
                    num3 = v;
                    break;
                case 4:
                    num = num8;
                    num2 = num6;
                    num3 = v;
                    break;
                default:
                    num = v;
                    num2 = num6;
                    num3 = num7;
                    break;
            }
        }

        return (num, num2, num3);
    }
}