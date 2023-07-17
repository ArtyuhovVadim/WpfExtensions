using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class TextColorPicker : Control
{
    static TextColorPicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextColorPicker), new FrameworkPropertyMetadata(typeof(TextColorPicker)));
    }

    #region Red

    public byte Red
    {
        get => (byte)GetValue(RedProperty);
        set => SetValue(RedProperty, value);
    }

    public static readonly DependencyProperty RedProperty =
        DependencyProperty.Register(nameof(Red), typeof(byte), typeof(TextColorPicker), new FrameworkPropertyMetadata(default(byte), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnRedChanged));

    private static void OnRedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextColorPicker picker)
            return;

        var red = (byte)e.NewValue;

        if (picker.Color.R == red)
            return;

        picker.Color = picker.Color with { R = red };
    }

    #endregion

    #region Green

    public byte Green
    {
        get => (byte)GetValue(GreenProperty);
        set => SetValue(GreenProperty, value);
    }

    public static readonly DependencyProperty GreenProperty =
        DependencyProperty.Register(nameof(Green), typeof(byte), typeof(TextColorPicker), new FrameworkPropertyMetadata(default(byte), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGreenChanged));

    private static void OnGreenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextColorPicker picker)
            return;

        var green = (byte)e.NewValue;

        if (picker.Color.G == green)
            return;

        picker.Color = picker.Color with { G = green };
    }

    #endregion

    #region Blue

    public byte Blue
    {
        get => (byte)GetValue(BlueProperty);
        set => SetValue(BlueProperty, value);
    }

    public static readonly DependencyProperty BlueProperty =
        DependencyProperty.Register(nameof(Blue), typeof(byte), typeof(TextColorPicker), new FrameworkPropertyMetadata(default(byte), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBlueChanged));

    private static void OnBlueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextColorPicker picker)
            return;

        var blue = (byte)e.NewValue;

        if (picker.Color.B == blue)
            return;

        picker.Color = picker.Color with { B = blue };
    }

    #endregion

    #region Alpha

    public byte Alpha
    {
        get => (byte)GetValue(AlphaProperty);
        set => SetValue(AlphaProperty, value);
    }

    public static readonly DependencyProperty AlphaProperty =
        DependencyProperty.Register(nameof(Alpha), typeof(byte), typeof(TextColorPicker), new FrameworkPropertyMetadata(default(byte), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnAlphaChanged));

    private static void OnAlphaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextColorPicker picker)
            return;

        var alpha = (byte)e.NewValue;

        if (picker.Color.A == alpha)
            return;

        picker.Color = picker.Color with { A = alpha };
    }

    #endregion

    #region Color

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(Color), typeof(TextColorPicker), new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnColorChanged));

    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextColorPicker picker)
            return;

        var newColor = (Color)e.NewValue;
        var oldColor = (Color)e.OldValue;

        if (newColor == oldColor)
            return;

        picker.Red = newColor.R;
        picker.Green = newColor.G;
        picker.Blue = newColor.B;
        picker.Alpha = newColor.A;
    }

    #endregion

    #region IsTransparencySupported

    public bool IsTransparencySupported
    {
        get => (bool)GetValue(IsTransparencySupportedProperty);
        set => SetValue(IsTransparencySupportedProperty, value);
    }

    public static readonly DependencyProperty IsTransparencySupportedProperty =
        DependencyProperty.Register(nameof(IsTransparencySupported), typeof(bool), typeof(TextColorPicker), new PropertyMetadata(false));

    #endregion
}