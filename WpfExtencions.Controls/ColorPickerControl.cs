using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfExtensions.Controls.ColorPicker;

namespace WpfExtensions.Controls;

public class ColorPickerControl : BaseColorPickerControl
{
    private readonly ObservableCollection<SolidColorBrush> _recentBrushes = new();

    static ColorPickerControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerControl), new FrameworkPropertyMetadata(typeof(ColorPickerControl)));
    }

    public override IEnumerable<SolidColorBrush> RecentBrushes => _recentBrushes;

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);

        UpdateRecentColors(Color);
    }

    protected override void OnPreviewMouseDown(MouseButtonEventArgs e) => Focus();

    protected override void OnColorSelected(Color color)
    {
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