using System.Windows;
using System.Windows.Controls;

namespace WpfExtensions.Controls;

public class TextBoxEx : TextBox
{
    static TextBoxEx()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxEx), new FrameworkPropertyMetadata(typeof(TextBoxEx)));
    }

    #region CornerRadius

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(TextBoxEx), new PropertyMetadata(default(CornerRadius)));

    #endregion

    #region Watermark

    public string Watermark
    {
        get => (string)GetValue(WatermarkProperty);
        set => SetValue(WatermarkProperty, value);
    }

    public static readonly DependencyProperty WatermarkProperty =
        DependencyProperty.Register(nameof(Watermark), typeof(string), typeof(TextBoxEx), new PropertyMetadata(string.Empty));

    #endregion

    #region WatermarkOpacity

    public double WatermarkOpacity
    {
        get => (double)GetValue(WatermarkOpacityProperty);
        set => SetValue(WatermarkOpacityProperty, value);
    }

    public static readonly DependencyProperty WatermarkOpacityProperty =
        DependencyProperty.Register(nameof(WatermarkOpacity), typeof(double), typeof(TextBoxEx), new PropertyMetadata(1d));

    #endregion

    #region Icon

    public object Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(TextBoxEx), new PropertyMetadata(default(object)));

    #endregion
    
    #region IconMargin

    public Thickness IconMargin
    {
        get => (Thickness)GetValue(IconMarginProperty);
        set => SetValue(IconMarginProperty, value);
    }

    public static readonly DependencyProperty IconMarginProperty =
        DependencyProperty.Register(nameof(IconMargin), typeof(Thickness), typeof(TextBoxEx), new PropertyMetadata(default(Thickness)));

    #endregion
}