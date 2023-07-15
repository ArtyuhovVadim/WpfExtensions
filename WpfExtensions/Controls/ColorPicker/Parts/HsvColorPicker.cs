using System.Windows;
using System.Windows.Controls;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class HsvColorPicker : Control
{
    static HsvColorPicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(HsvColorPicker), new FrameworkPropertyMetadata(typeof(HsvColorPicker)));
    }

    #region ComplexColor

    public ComplexColor ComplexColor
    {
        get => (ComplexColor)GetValue(ComplexColorProperty);
        set => SetValue(ComplexColorProperty, value);
    }

    public static readonly DependencyProperty ComplexColorProperty =
        DependencyProperty.Register(nameof(ComplexColor), typeof(ComplexColor), typeof(HsvColorPicker), new PropertyMetadata(ComplexColor.Black));

    #endregion
}