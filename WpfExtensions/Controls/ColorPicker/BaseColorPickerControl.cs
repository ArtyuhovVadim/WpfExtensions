using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfExtensions.Mvvm.Commands;

namespace WpfExtensions.Controls.ColorPicker;

public abstract class BaseColorPickerControl : Control
{
    protected BaseColorPickerControl()
    {
        ColorSelectedCommand = new LambdaCommand<Color>(OnColorSelected);
    }

    #region Color

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(Color), typeof(BaseColorPickerControl), new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    #endregion

    #region IsTransparencySupported

    public bool IsTransparencySupported
    {
        get => (bool)GetValue(IsTransparencySupportedProperty);
        set => SetValue(IsTransparencySupportedProperty, value);
    }

    public static readonly DependencyProperty IsTransparencySupportedProperty =
        DependencyProperty.Register(nameof(IsTransparencySupported), typeof(bool), typeof(BaseColorPickerControl), new PropertyMetadata(false));

    #endregion

    #region IsColorPaletteVisible

    public bool IsColorPaletteVisible
    {
        get => (bool)GetValue(IsColorPaletteVisibleProperty);
        set => SetValue(IsColorPaletteVisibleProperty, value);
    }

    public static readonly DependencyProperty IsColorPaletteVisibleProperty =
        DependencyProperty.Register(nameof(IsColorPaletteVisible), typeof(bool), typeof(BaseColorPickerControl), new PropertyMetadata(true));

    #endregion

    #region IsRecentColorsVisible

    public bool IsRecentColorsVisible
    {
        get => (bool)GetValue(IsRecentColorsVisibleProperty);
        set => SetValue(IsRecentColorsVisibleProperty, value);
    }

    public static readonly DependencyProperty IsRecentColorsVisibleProperty =
        DependencyProperty.Register(nameof(IsRecentColorsVisible), typeof(bool), typeof(BaseColorPickerControl), new PropertyMetadata(true));

    #endregion

    #region IsRecentColorsEmpty

    private static readonly DependencyPropertyKey IsRecentColorsEmptyPropertyKey
        = DependencyProperty.RegisterReadOnly(nameof(IsRecentColorsEmpty), typeof(bool), typeof(BaseColorPickerControl), new PropertyMetadata(true));

    public static readonly DependencyProperty IsRecentColorsEmptyProperty = IsRecentColorsEmptyPropertyKey.DependencyProperty;

    public bool IsRecentColorsEmpty
    {
        get => (bool)GetValue(IsRecentColorsEmptyProperty);
        protected set => SetValue(IsRecentColorsEmptyPropertyKey, value);
    }

    #endregion

    public int RecentBrushesMaxCount { get; set; } = 10;

    public abstract IEnumerable<SolidColorBrush> RecentBrushes { get; }

    public ICommand ColorSelectedCommand { get; protected set; }

    protected abstract void OnColorSelected(Color color);
}