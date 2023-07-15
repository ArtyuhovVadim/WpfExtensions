using System.Windows;
using System.Windows.Controls;
using WpfExtensions.Controls.ColorPicker;

namespace WpfExtensions.Controls;

public class ColorPickerControl : Control
{
    static ColorPickerControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerControl), new FrameworkPropertyMetadata(typeof(ColorPickerControl)));
    }

    #region ComplexColor

    public ComplexColor ComplexColor
    {
        get => (ComplexColor)GetValue(ComplexColorProperty);
        set => SetValue(ComplexColorProperty, value);
    }

    public static readonly DependencyProperty ComplexColorProperty =
        DependencyProperty.Register(nameof(ComplexColor), typeof(ComplexColor), typeof(ColorPickerControl), new PropertyMetadata(default(ComplexColor)));

    #endregion

    #region IsTransparencySupported

    public bool IsTransparencySupported
    {
        get => (bool)GetValue(IsTransparencySupportedProperty);
        set => SetValue(IsTransparencySupportedProperty, value);
    }

    public static readonly DependencyProperty IsTransparencySupportedProperty =
        DependencyProperty.Register(nameof(IsTransparencySupported), typeof(bool), typeof(ColorPickerControl), new PropertyMetadata(false));

    #endregion
}