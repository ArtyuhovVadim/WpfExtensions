using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using WpfExtensions.Controls.ColorPicker;

namespace WpfExtensions.Controls;

public class MinimizedColorPickerControl : BaseColorPickerControl
{
    private const string ApplyButtonName = "PART_ApplyButton";
    private const string CancelButtonName = "PART_CancelButton";
    private const string RootPopupName = "PART_RootPopup";

    private Button? _applyButton;
    private Button? _cancelButton;
    private Popup? _rootPopup;

    private readonly ObservableCollection<SolidColorBrush> _recentBrushes = new();

    static MinimizedColorPickerControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MinimizedColorPickerControl), new FrameworkPropertyMetadata(typeof(MinimizedColorPickerControl)));
    }

    #region ColorPickerWidth

    public double ColorPickerWidth
    {
        get => (double)GetValue(ColorPickerWidthProperty);
        set => SetValue(ColorPickerWidthProperty, value);
    }

    public static readonly DependencyProperty ColorPickerWidthProperty =
        DependencyProperty.Register(nameof(ColorPickerWidth), typeof(double), typeof(MinimizedColorPickerControl), new PropertyMetadata(default(double)));

    #endregion

    #region ColorPickerHeight

    public double ColorPickerHeight
    {
        get => (double)GetValue(ColorPickerHeightProperty);
        set => SetValue(ColorPickerHeightProperty, value);
    }

    public static readonly DependencyProperty ColorPickerHeightProperty =
        DependencyProperty.Register(nameof(ColorPickerHeight), typeof(double), typeof(MinimizedColorPickerControl), new PropertyMetadata(default(double)));

    #endregion

    #region SelectedColor

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    public static readonly DependencyProperty SelectedColorProperty =
        DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(MinimizedColorPickerControl), new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedColorChanged));

    private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not MinimizedColorPickerControl picker) return;

        picker.Color = (Color)e.NewValue;
    }

    #endregion

    public override IEnumerable<SolidColorBrush> RecentBrushes => _recentBrushes;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _applyButton = GetTemplateChild(ApplyButtonName) as Button ?? throw new ElementNotAvailableException($"Part element is not available in {GetType()} template!");
        _cancelButton = GetTemplateChild(CancelButtonName) as Button ?? throw new ElementNotAvailableException($"Part element is not available in {GetType()} template!");
        _rootPopup = GetTemplateChild(RootPopupName) as Popup ?? throw new ElementNotAvailableException($"Part element is not available in {GetType()} template!");

        _applyButton.Click += OnApplyButtonClick;
        _cancelButton.Click += OnCancelButtonClick;
        _rootPopup.Opened += OnRootPopupOpened;
        _rootPopup.Closed += OnRootPopupClosed;
    }

    private void OnRootPopupOpened(object? sender, EventArgs e) => Focus();

    private void OnRootPopupClosed(object? sender, EventArgs e) => Color = SelectedColor;

    private void OnApplyButtonClick(object sender, RoutedEventArgs e)
    {
        SelectedColor = Color;
        UpdateRecentColors(SelectedColor);
        _rootPopup!.IsOpen = false;
    }

    private void OnCancelButtonClick(object sender, RoutedEventArgs e)
    {
        Color = SelectedColor;
        _rootPopup!.IsOpen = false;
    }

    protected override void OnColorSelected(Color color)
    {
        SelectedColor = Color = color;
        UpdateRecentColors(color);
        _rootPopup!.IsOpen = false;
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