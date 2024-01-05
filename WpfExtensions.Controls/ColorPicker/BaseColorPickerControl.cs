using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfExtensions.Mvvm.Commands;

namespace WpfExtensions.Controls.ColorPicker;

public abstract class BaseColorPickerControl : Control
{
    private static readonly Dictionary<string, ObservableCollection<SolidColorBrush>> RecentBrushesCache = new();

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

    #region RecentBrushesGroupName

    public string? RecentBrushesGroupName
    {
        get => (string)GetValue(RecentBrushesGroupNameProperty);
        set => SetValue(RecentBrushesGroupNameProperty, value);
    }

    public static readonly DependencyProperty RecentBrushesGroupNameProperty =
        DependencyProperty.Register(nameof(RecentBrushesGroupName), typeof(string), typeof(BaseColorPickerControl), new PropertyMetadata(null, OnRecentBrushesGroupNamePropertyChanged));

    private static void OnRecentBrushesGroupNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not BaseColorPickerControl control) return;
        control.RecentBrushes = GetRecentBrushes((string)e.NewValue);
    }

    #endregion

    #region RecentBrushes

    public IEnumerable<SolidColorBrush> RecentBrushes
    {
        get => (IEnumerable<SolidColorBrush>)GetValue(RecentBrushesPropertyKey.DependencyProperty);
        private set => SetValue(RecentBrushesPropertyKey, value);
    }

    private static readonly DependencyPropertyKey RecentBrushesPropertyKey
        = DependencyProperty.RegisterReadOnly(nameof(RecentBrushes), typeof(IEnumerable<SolidColorBrush>), typeof(BaseColorPickerControl), new PropertyMetadata(default(IEnumerable<SolidColorBrush>), OnRecentBrushesPropertyChanged));

    private static void OnRecentBrushesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not BaseColorPickerControl control) return;

        if (e.OldValue is INotifyCollectionChanged oldCollection)
            oldCollection.CollectionChanged -= control.OnRecentBrushesCollectionChanged;

        if (e.NewValue is INotifyCollectionChanged newCollection)
            newCollection.CollectionChanged += control.OnRecentBrushesCollectionChanged;
    }

    #endregion

    #region RecentBrushesMaxCount

    public int RecentBrushesMaxCount
    {
        get => (int)GetValue(RecentBrushesMaxCountProperty);
        set => SetValue(RecentBrushesMaxCountProperty, value);
    }

    public static readonly DependencyProperty RecentBrushesMaxCountProperty =
        DependencyProperty.Register(nameof(RecentBrushesMaxCount), typeof(int), typeof(BaseColorPickerControl), new PropertyMetadata(10));

    #endregion

    public ICommand ColorSelectedCommand { get; protected set; }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        RecentBrushesGroupName ??= Guid.NewGuid().ToString();
        RecentBrushes = GetRecentBrushes(RecentBrushesGroupName);
        IsRecentColorsEmpty = !RecentBrushes.Any();
    }

    protected abstract void OnColorSelected(Color color);

    protected static ObservableCollection<SolidColorBrush> GetRecentBrushes(string? groupName)
    {
        if (groupName is null)
            return null!;

        if (RecentBrushesCache.TryGetValue(groupName, out var brushes))
            return brushes;

        var newBrushesCollection = new ObservableCollection<SolidColorBrush>();
        RecentBrushesCache[groupName] = newBrushesCollection;
        return newBrushesCollection;
    }

    private void OnRecentBrushesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => IsRecentColorsEmpty = !RecentBrushes.Any();
}