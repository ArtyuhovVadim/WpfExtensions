using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class ComplexColor : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ComplexColor() : this(Colors.Black) { }

    public ComplexColor(Color color) => Color = color;

    private byte _a;

    private byte _r;
    private byte _g;
    private byte _b;

    private double _h;
    private double _s;
    private double _v;

    public Color Color
    {
        get => Color.FromArgb(_a, _r, _g, _b);
        set
        {
            _r = value.R;
            _g = value.G;
            _b = value.B;
            _a = value.A;

            RecalculateHsvFromRgb();

            OnPropertyChanged(nameof(Red));
            OnPropertyChanged(nameof(Green));
            OnPropertyChanged(nameof(Blue));
            OnPropertyChanged(nameof(Alpha));
        }
    }

    public byte Red
    {
        get => _r;
        set
        {
            _r = value;
            RecalculateHsvFromRgb();
            OnPropertyChanged();
        }
    }

    public byte Green
    {
        get => _g;
        set
        {
            _g = value;
            RecalculateHsvFromRgb();
            OnPropertyChanged();
        }
    }

    public byte Blue
    {
        get => _b;
        set
        {
            _b = value;
            RecalculateHsvFromRgb();
            OnPropertyChanged();
        }
    }

    public byte Alpha
    {
        get => _a;
        set
        {
            _a = value;
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
        (_h, _s, _v) = ToHsv(_r, _g, _b);

        OnPropertyChanged(nameof(Color));

        OnPropertyChanged(nameof(Hue));
        OnPropertyChanged(nameof(Saturation));
        OnPropertyChanged(nameof(Value));
    }

    private void RecalculateRgbFromHsv()
    {
        (_r, _g, _b) = FromHsv(_h, _s, _v);

        OnPropertyChanged(nameof(Color));

        OnPropertyChanged(nameof(Red));
        OnPropertyChanged(nameof(Green));
        OnPropertyChanged(nameof(Blue));
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private static (double h, double s, double v) ToHsv(byte r, byte g, byte b)
    {
        var tmpColor = System.Drawing.Color.FromArgb(255, r, g, b);

        int max = Math.Max(tmpColor.R, Math.Max(tmpColor.G, tmpColor.B));
        int min = Math.Min(tmpColor.R, Math.Min(tmpColor.G, tmpColor.B));

        var hue = tmpColor.GetHue();
        var saturation = max == 0 ? 0 : 1d - 1d * min / max;
        var value = max / 255d;

        return (hue, saturation, value);
    }

    private static (byte r, byte g, byte b) FromHsv(double hue, double saturation, double value)
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
            0 => (v, t, p),
            1 => (q, v, p),
            2 => (p, v, t),
            3 => (p, q, v),
            4 => (t, p, v),
            _ => (v, p, q)
        };
    }
}