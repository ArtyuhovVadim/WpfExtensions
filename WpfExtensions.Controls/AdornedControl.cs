using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using WpfExtensions.Utils;

namespace WpfExtensions.Controls;

//Source: https://www.codeproject.com/Articles/54472/Defining-WPF-Adorners-in-XAML
public class AdornedControl : ContentControl
{
    private AdornerLayer? _adornerLayer;
    private FrameworkElementAdorner? _adorner;

    #region IsAdornerVisible

    public bool IsAdornerVisible
    {
        get => (bool)GetValue(IsAdornerVisibleProperty);
        set => SetValue(IsAdornerVisibleProperty, value);
    }

    public static readonly DependencyProperty IsAdornerVisibleProperty =
        DependencyProperty.Register(nameof(IsAdornerVisible), typeof(bool), typeof(AdornedControl), new PropertyMetadata(OnIsAdornerVisiblePropertyChanged));

    private static void OnIsAdornerVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AdornedControl control) return;

        control.ShowOrHideAdorner();
    }

    #endregion

    #region AdornerContent

    public FrameworkElement? AdornerContent
    {
        get => (FrameworkElement?)GetValue(AdornerContentProperty);
        set => SetValue(AdornerContentProperty, value);
    }

    public static readonly DependencyProperty AdornerContentProperty =
        DependencyProperty.Register(nameof(AdornerContent), typeof(FrameworkElement), typeof(AdornedControl), new FrameworkPropertyMetadata(OnAdornerContentPropertyChanged));

    private static void OnAdornerContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AdornedControl control) return;

        control.ShowOrHideAdorner();
    }

    #endregion

    #region HorizontalAdornerPlacement

    public AdornerPlacement HorizontalAdornerPlacement
    {
        get => (AdornerPlacement)GetValue(HorizontalAdornerPlacementProperty);
        set => SetValue(HorizontalAdornerPlacementProperty, value);
    }

    public static readonly DependencyProperty HorizontalAdornerPlacementProperty =
        DependencyProperty.Register(nameof(HorizontalAdornerPlacement), typeof(AdornerPlacement), typeof(AdornedControl), new PropertyMetadata(AdornerPlacement.Inside, OnHorizontalAdornerPlacementPropertyChanged));

    private static void OnHorizontalAdornerPlacementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AdornedControl control) return;

        if (control._adorner is null) return;

        control._adorner.HorizontalAdornerPlacement = (AdornerPlacement)e.NewValue;
        control._adorner.InvalidateMeasure();
    }

    #endregion

    #region VerticalAdornerPlacement

    public AdornerPlacement VerticalAdornerPlacement
    {
        get => (AdornerPlacement)GetValue(VerticalAdornerPlacementProperty);
        set => SetValue(VerticalAdornerPlacementProperty, value);
    }

    public static readonly DependencyProperty VerticalAdornerPlacementProperty =
        DependencyProperty.Register(nameof(VerticalAdornerPlacement), typeof(AdornerPlacement), typeof(AdornedControl), new PropertyMetadata(AdornerPlacement.Inside, OnVerticalAdornerPlacementPropertyChanged));

    private static void OnVerticalAdornerPlacementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AdornedControl control) return;

        if (control._adorner is null) return;

        control._adorner.VerticalAdornerPlacement = (AdornerPlacement)e.NewValue;
        control._adorner.InvalidateMeasure();
    }

    #endregion

    #region AdornerOffsetX

    public double AdornerOffsetX
    {
        get => (double)GetValue(AdornerOffsetXProperty);
        set => SetValue(AdornerOffsetXProperty, value);
    }

    public static readonly DependencyProperty AdornerOffsetXProperty =
        DependencyProperty.Register(nameof(AdornerOffsetX), typeof(double), typeof(AdornedControl), new PropertyMetadata(default(double), OnAdornerOffsetXPropertyChanged));

    private static void OnAdornerOffsetXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AdornedControl control) return;

        if (control._adorner is null) return;

        control._adorner.OffsetX = (double)e.NewValue;
        control._adorner.InvalidateMeasure();
    }

    #endregion

    #region AdornerOffsetY

    public double AdornerOffsetY
    {
        get => (double)GetValue(AdornerOffsetYProperty);
        set => SetValue(AdornerOffsetYProperty, value);
    }

    public static readonly DependencyProperty AdornerOffsetYProperty =
        DependencyProperty.Register(nameof(AdornerOffsetY), typeof(double), typeof(AdornedControl), new PropertyMetadata(default(double), OnAdornerOffsetYPropertyChanged));

    private static void OnAdornerOffsetYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AdornedControl control) return;

        if (control._adorner is null) return;

        control._adorner.OffsetY = (double)e.NewValue;
        control._adorner.InvalidateMeasure();
    }

    #endregion

    #region AdornedTemplatePartName

    public string AdornedTemplatePartName
    {
        get => (string)GetValue(AdornedTemplatePartNameProperty);
        set => SetValue(AdornedTemplatePartNameProperty, value);
    }

    public static readonly DependencyProperty AdornedTemplatePartNameProperty =
        DependencyProperty.Register(nameof(AdornedTemplatePartName), typeof(string), typeof(AdornedControl), new PropertyMetadata(string.Empty));

    #endregion

    public AdornedControl()
    {
        Focusable = false;
        DataContextChanged += (_, _) => UpdateAdornerDataContext();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        ShowOrHideAdorner();
    }

    private void UpdateAdornerDataContext()
    {
        if (AdornerContent is null) return;
        AdornerContent.DataContext = DataContext;
    }

    private void ShowOrHideAdorner()
    {
        if (IsAdornerVisible) ShowAdorner();
        else HideAdorner();
    }

    private void ShowAdorner()
    {
        if (_adorner is not null || AdornerContent is null) return;
        _adornerLayer ??= AdornerLayer.GetAdornerLayer(this);
        if (_adornerLayer is null) return;

        FrameworkElement? adornedControl = this;

        if (AdornedTemplatePartName != string.Empty)
        {
            adornedControl = this.FindVisualChildByName<FrameworkElement>(AdornedTemplatePartName);

            if (adornedControl is null)
                throw new ApplicationException($"Failed to find a FrameworkElement in the visual-tree with the part name '{AdornedTemplatePartName}'.");
        }

        _adorner = new FrameworkElementAdorner(AdornerContent, adornedControl, HorizontalAdornerPlacement, VerticalAdornerPlacement, AdornerOffsetX, AdornerOffsetY);
        _adornerLayer.Add(_adorner);

        UpdateAdornerDataContext();
    }

    private void HideAdorner()
    {
        if (_adornerLayer is null || _adorner is null)
            return;

        _adornerLayer.Remove(_adorner);
        _adorner.DisconnectChild();
        _adorner = null;
        _adornerLayer = null;
    }
}