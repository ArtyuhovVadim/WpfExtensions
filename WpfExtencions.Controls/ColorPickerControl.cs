using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfExtensions.Controls.ColorPicker;

namespace WpfExtensions.Controls;

public class ColorPickerControl : BaseColorPickerControl
{
    static ColorPickerControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerControl), new FrameworkPropertyMetadata(typeof(ColorPickerControl)));
    }

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
        var recentBrushes = GetRecentBrushes(RecentBrushesGroupName!);

        if (recentBrushes.Any(x => x.Color == color))
            return;

        if (recentBrushes.Count >= RecentBrushesMaxCount)
            recentBrushes.RemoveAt(RecentBrushesMaxCount - 1);

        var brush = new SolidColorBrush(color);

        recentBrushes.Insert(0, brush);

        IsRecentColorsEmpty = false;
    }
}