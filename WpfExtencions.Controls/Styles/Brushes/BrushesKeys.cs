using System;
using System.Windows;

namespace WpfExtensions.Controls.Styles.Brushes;

public static class BrushesKeys
{
    public static class ColorPickerControl
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey Foreground = CreateInstance();

        public static readonly ComponentResourceKey BorderBrush = CreateInstance();
    }

    public static class MinimizedColorPickerControl
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey Foreground = CreateInstance();

        public static readonly ComponentResourceKey BorderBrush = CreateInstance();

        public static readonly ComponentResourceKey MinimizedBackground = CreateInstance();

        public static readonly ComponentResourceKey MinimizedBorderBrush = CreateInstance();

        public static readonly ComponentResourceKey MouseOverBrush = CreateInstance();

        public static readonly ComponentResourceKey FocusedBrush = CreateInstance();
    }

    private static ComponentResourceKey CreateInstance() => 
        new(typeof(BrushesKeys), Guid.NewGuid());
}