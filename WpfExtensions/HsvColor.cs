using System;

namespace WpfExtensions;

public struct HsvColor
{
    public HsvColor(double h, double s, double v)
    {
        Hue = h;
        Saturation = s;
        Value = v;
    }

    public double Hue { get; set; }

    public double Saturation { get; set; }

    public double Value { get; set; }

    public bool Equals(HsvColor other) => Hue.Equals(other.Hue) && Saturation.Equals(other.Saturation) && Value.Equals(other.Value);

    public override bool Equals(object? obj) => obj is HsvColor other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Hue, Saturation, Value);

    public static bool operator ==(HsvColor colorA, HsvColor colorB) => !(colorA != colorB);

    public static bool operator !=(HsvColor colorA, HsvColor colorB) => 
        colorA.Hue.CompareTo(colorB.Hue) != 0 || colorA.Saturation.CompareTo(colorB.Saturation) != 0 || colorA.Value.CompareTo(colorB.Value) != 0;

    public override string ToString() => $"H:{Hue:0.00}, S:{Saturation:0.00}, V:{Value:0.00}";
}