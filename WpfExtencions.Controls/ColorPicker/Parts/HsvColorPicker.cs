using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfExtensions.Utils;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class HsvColorPicker : Control
{
    static HsvColorPicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(HsvColorPicker), new FrameworkPropertyMetadata(typeof(HsvColorPicker)));
    }

    #region ColorChangedEvent

    public event ColorChangedEventHandler ColorChanged
    {
        add => AddHandler(ColorChangedEvent, value);
        remove => RemoveHandler(ColorChangedEvent, value);
    }

    public static readonly RoutedEvent ColorChangedEvent =
        EventManager.RegisterRoutedEvent(nameof(ColorChanged), RoutingStrategy.Bubble, typeof(ColorChangedEventHandler), typeof(HsvColorPicker));

    #endregion

    #region Hue

    public double Hue
    {
        get => (double)GetValue(HueProperty);
        set => SetValue(HueProperty, value);
    }

    public static readonly DependencyProperty HueProperty =
        DependencyProperty.Register(nameof(Hue), typeof(double), typeof(HsvColorPicker), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHueChanged, OnCoerceHueValue));

    private static object OnCoerceHueValue(DependencyObject d, object basevalue) =>
        d is not HsvColorPicker ? basevalue : Math.Clamp((double)basevalue, 0, 360);

    private static void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not HsvColorPicker picker)
            return;

        var hue = (double)e.NewValue;

        var newColor = ColorUtils.HsvToRgb(hue, picker.Saturation, picker.Value) with { A = picker.Alpha };

        if (picker.Color == newColor)
            return;

        if (picker._canRaisingEvents)
            picker.RaiseEvent(new ColorChangedEventArgs(ColorChangedEvent, picker, newColor, picker.Color));
        picker.Color = newColor;
    }

    #endregion

    #region Saturation

    public double Saturation
    {
        get => (double)GetValue(SaturationProperty);
        set => SetValue(SaturationProperty, value);
    }

    public static readonly DependencyProperty SaturationProperty =
        DependencyProperty.Register(nameof(Saturation), typeof(double), typeof(HsvColorPicker), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSaturationChanged, OnCoerceZeroToOneValue));

    private static void OnSaturationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not HsvColorPicker picker)
            return;

        var saturation = (double)e.NewValue;

        var newColor = ColorUtils.HsvToRgb(picker.Hue, saturation, picker.Value) with { A = picker.Alpha };

        if (picker.Color == newColor)
            return;

        if (picker._canRaisingEvents)
            picker.RaiseEvent(new ColorChangedEventArgs(ColorChangedEvent, picker, newColor, picker.Color));
        picker.Color = newColor;
    }

    #endregion

    #region Value

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double), typeof(HsvColorPicker), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged, OnCoerceZeroToOneValue));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not HsvColorPicker picker)
            return;

        var value = (double)e.NewValue;

        var newColor = ColorUtils.HsvToRgb(picker.Hue, picker.Saturation, value) with { A = picker.Alpha };

        if (picker.Color == newColor)
            return;

        if (picker._canRaisingEvents)
            picker.RaiseEvent(new ColorChangedEventArgs(ColorChangedEvent, picker, newColor, picker.Color));
        picker.Color = newColor;
    }

    #endregion

    #region Alpha

    public byte Alpha
    {
        get => (byte)GetValue(AlphaProperty);
        set => SetValue(AlphaProperty, value);
    }

    public static readonly DependencyProperty AlphaProperty =
        DependencyProperty.Register(nameof(Alpha), typeof(byte), typeof(HsvColorPicker), new FrameworkPropertyMetadata(byte.MaxValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnAlphaChanged));

    private static void OnAlphaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not HsvColorPicker picker)
            return;

        var alpha = (byte)e.NewValue;
        var newColor = picker.Color with { A = alpha };

        if (picker.Color == newColor)
            return;

        if (picker._canRaisingEvents)
            picker.RaiseEvent(new ColorChangedEventArgs(ColorChangedEvent, picker, newColor, picker.Color));

        picker.Color = newColor;
    }

    #endregion

    #region Color

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(Color), typeof(HsvColorPicker), new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnColorChanged));

    private bool _canRaisingEvents;

    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not HsvColorPicker picker)
            return;

        var newColor = (Color)e.NewValue;
        var oldColor = (Color)e.OldValue;

        if (newColor != ColorUtils.HsvToRgb(picker.Hue, picker.Saturation, picker.Value) with { A = picker.Alpha })
        {
            var newHsv = ColorUtils.RgbToHsv(newColor, picker.Hue);

            picker._canRaisingEvents = false;

            picker.Hue = newHsv.Hue;
            picker.Saturation = newHsv.Saturation;
            picker.Value = newHsv.Value;
            picker.Alpha = newColor.A;

            picker._canRaisingEvents = true;

            picker.RaiseEvent(new ColorChangedEventArgs(ColorChangedEvent, picker, newColor, oldColor));
        }
    }

    #endregion

    #region IsTransparencySupported

    public bool IsTransparencySupported
    {
        get => (bool)GetValue(IsTransparencySupportedProperty);
        set => SetValue(IsTransparencySupportedProperty, value);
    }

    public static readonly DependencyProperty IsTransparencySupportedProperty =
        DependencyProperty.Register(nameof(IsTransparencySupported), typeof(bool), typeof(HsvColorPicker), new PropertyMetadata(false));

    #endregion

    private static object OnCoerceZeroToOneValue(DependencyObject d, object basevalue) =>
        d is not HsvColorPicker ? basevalue : Math.Clamp((double)basevalue, 0, 1);
}