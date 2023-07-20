using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfExtensions.Controls;

public class ColorPickerControl : Control
{
    private readonly ObservableCollection<SolidColorBrush> _recentBrushes = new();

    static ColorPickerControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerControl), new FrameworkPropertyMetadata(typeof(ColorPickerControl)));
    }

    public ColorPickerControl()
    {
        ColorSelectedCommand = new LambdaCommand(OnColorSelected);
    }

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

    #region IsRecentColorsEmpty

    private static readonly DependencyPropertyKey IsRecentColorsEmptyPropertyKey
        = DependencyProperty.RegisterReadOnly(nameof(IsRecentColorsEmpty), typeof(bool), typeof(ColorPickerControl), new PropertyMetadata(true));

    public static readonly DependencyProperty IsRecentColorsEmptyProperty = IsRecentColorsEmptyPropertyKey.DependencyProperty;

    public bool IsRecentColorsEmpty
    {
        get => (bool)GetValue(IsRecentColorsEmptyProperty);
        private set => SetValue(IsRecentColorsEmptyPropertyKey, value);
    }

    #endregion

    public ICommand ColorSelectedCommand { get; }

    public int RecentBrushesMaxCount { get; set; } = 10;

    public IEnumerable<SolidColorBrush> RecentBrushes => _recentBrushes;

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);

        UpdateRecentColors(Color);
    }

    protected override void OnPreviewMouseDown(MouseButtonEventArgs e) => Focus();

    private void OnColorSelected(object o)
    {
        if (o is not Color color) return;

        Color = color;

        UpdateRecentColors(color);
    }

    private void UpdateRecentColors(Color color)
    {
        if (_recentBrushes.Any(x => x.Color == color))
            return;

        if (_recentBrushes.Count >= RecentBrushesMaxCount)
            _recentBrushes.RemoveAt(RecentBrushesMaxCount - 1);

        var brush = new SolidColorBrush(color);

        _recentBrushes.Insert(0, brush);

        IsRecentColorsEmpty = false;
    }
}