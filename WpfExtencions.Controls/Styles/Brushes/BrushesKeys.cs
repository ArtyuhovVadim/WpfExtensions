﻿using System.Windows;

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

    public static class ColorPalette
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey BorderBrush = CreateInstance();

        public static readonly ComponentResourceKey HighlightColor = CreateInstance();
    }

    public static class ColorSlider
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey BorderBrush = CreateInstance();

        public static readonly ComponentResourceKey ThumbBrush = CreateInstance();
    }

    public static class HsvColorPicker
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey BorderBrush = CreateInstance();
    }

    public static class RecentBrushes
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey BorderBrush = CreateInstance();

        public static readonly ComponentResourceKey HighlightColor = CreateInstance();
    }

    public static class TextColorPicker
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey BorderBrush = CreateInstance();
    }

    public static class Button
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey Foreground = CreateInstance();

        public static readonly ComponentResourceKey BorderBrush = CreateInstance();

        public static readonly ComponentResourceKey FocusedBackground = CreateInstance();

        public static readonly ComponentResourceKey FocusedBorderBrush = CreateInstance();

        public static readonly ComponentResourceKey MouseOverBackground = CreateInstance();

        public static readonly ComponentResourceKey MouseOverBorderBrush = CreateInstance();

        public static readonly ComponentResourceKey PressedBackground = CreateInstance();

        public static readonly ComponentResourceKey PressedBorderBrush = CreateInstance();

        public static readonly ComponentResourceKey DefaultedBackground = CreateInstance();

        public static readonly ComponentResourceKey DefaultedBorderBrush = CreateInstance();

        public static readonly ComponentResourceKey DisabledBackground = CreateInstance();

        public static readonly ComponentResourceKey DisabledForeground = CreateInstance();

        public static readonly ComponentResourceKey DisabledBorderBrush = CreateInstance();
    }

    public static class TextBox
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey Foreground = CreateInstance();

        public static readonly ComponentResourceKey BorderBrush = CreateInstance();

        public static readonly ComponentResourceKey SelectionBrush = CreateInstance();

        public static readonly ComponentResourceKey CaretBrush = CreateInstance();

        public static readonly ComponentResourceKey FocusedBorderBrush = CreateInstance();

        public static readonly ComponentResourceKey DisabledBackground = CreateInstance();

        public static readonly ComponentResourceKey DisabledForeground = CreateInstance();

        public static readonly ComponentResourceKey DisabledBorderBrush = CreateInstance();
    }

    private static ComponentResourceKey CreateInstance() =>
        new(typeof(BrushesKeys), Guid.NewGuid());
}