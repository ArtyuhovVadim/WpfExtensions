using System.Windows;
using System.Windows.Controls;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class TextColorPicker : Control
{
    static TextColorPicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextColorPicker), new FrameworkPropertyMetadata(typeof(TextColorPicker)));
    }

    #region ComplexColor

    public ComplexColor ComplexColor
    {
        get => (ComplexColor)GetValue(ComplexColorProperty);
        set => SetValue(ComplexColorProperty, value);
    }

    public static readonly DependencyProperty ComplexColorProperty =
        DependencyProperty.Register(nameof(ComplexColor), typeof(ComplexColor), typeof(TextColorPicker), new PropertyMetadata(ComplexColor.Black));

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