using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfExtensions.Controls;

public class ColorPickerControl : Control
{
    static ColorPickerControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerControl), new FrameworkPropertyMetadata(typeof(ColorPickerControl)));
    }

    public ColorPickerControl()
    {
        ColorSelectedCommand = new LambdaCommand(OnColorSelected);
    }

    #region ColorSelectedCommand

    public ICommand ColorSelectedCommand
    {
        get => (ICommand)GetValue(ColorSelectedCommandProperty);
        set => SetValue(ColorSelectedCommandProperty, value);
    }

    public static readonly DependencyProperty ColorSelectedCommandProperty =
        DependencyProperty.Register(nameof(ColorSelectedCommand), typeof(ICommand), typeof(ColorPickerControl), new PropertyMetadata(default(ICommand)));

    #endregion

    #region Color

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(Color), typeof(ColorPickerControl), new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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

    #region IsColorPaletteVisible

    public bool IsColorPaletteVisible
    {
        get => (bool)GetValue(IsColorPaletteVisibleProperty);
        set => SetValue(IsColorPaletteVisibleProperty, value);
    }

    public static readonly DependencyProperty IsColorPaletteVisibleProperty =
        DependencyProperty.Register(nameof(IsColorPaletteVisible), typeof(bool), typeof(ColorPickerControl), new PropertyMetadata(true));

    #endregion

    #region IsRecentColorsVisible

    public bool IsRecentColorsVisible
    {
        get => (bool)GetValue(IsRecentColorsVisibleProperty);
        set => SetValue(IsRecentColorsVisibleProperty, value);
    }

    public static readonly DependencyProperty IsRecentColorsVisibleProperty =
        DependencyProperty.Register(nameof(IsRecentColorsVisible), typeof(bool), typeof(ColorPickerControl), new PropertyMetadata(false));

    #endregion

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
    }

    protected override void OnPreviewMouseDown(MouseButtonEventArgs e) => Focus();

    private void OnColorSelected(object o)
    {
        if (o is not Color color) return;

        Color = color;
    }
}