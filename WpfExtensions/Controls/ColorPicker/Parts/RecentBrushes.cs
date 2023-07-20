using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class RecentBrushes : Control
{
    static RecentBrushes()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RecentBrushes), new FrameworkPropertyMetadata(typeof(RecentBrushes)));
    }

    #region Brushes

    public IEnumerable<Brush> Brushes
    {
        get => (IEnumerable<Brush>)GetValue(BrushesProperty);
        set => SetValue(BrushesProperty, value);
    }

    public static readonly DependencyProperty BrushesProperty =
        DependencyProperty.Register(nameof(Brushes), typeof(IEnumerable<Brush>), typeof(RecentBrushes), new PropertyMetadata(default(IEnumerable<Brush>)));

    #endregion

    #region RecentBrushesMaxCount

    public int RecentBrushesMaxCount
    {
        get => (int)GetValue(RecentBrushesMaxCountProperty);
        set => SetValue(RecentBrushesMaxCountProperty, value);
    }

    public static readonly DependencyProperty RecentBrushesMaxCountProperty =
        DependencyProperty.Register(nameof(RecentBrushesMaxCount), typeof(int), typeof(RecentBrushes), new PropertyMetadata(10));

    #endregion

    #region ColorSelectedCommand

    public ICommand ColorSelectedCommand
    {
        get => (ICommand)GetValue(ColorSelectedCommandProperty);
        set => SetValue(ColorSelectedCommandProperty, value);
    }

    public static readonly DependencyProperty ColorSelectedCommandProperty =
        DependencyProperty.Register(nameof(ColorSelectedCommand), typeof(ICommand), typeof(RecentBrushes), new PropertyMetadata(default(ICommand)));

    #endregion
}