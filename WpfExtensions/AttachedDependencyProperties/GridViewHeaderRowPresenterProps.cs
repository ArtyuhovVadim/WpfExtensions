using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfExtensions.AttachedDependencyProperties;

public static class GridViewHeaderRowPresenterProps
{
    #region FloatingIndicatorBrush

    public static readonly DependencyProperty FloatingIndicatorBrushProperty = DependencyProperty.RegisterAttached(
        "FloatingIndicatorBrush", typeof(Brush), typeof(GridViewHeaderRowPresenterProps), new PropertyMetadata(default(Brush), OnFloatingIndicatorBrushChanged));

    private static void OnFloatingIndicatorBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not GridViewHeaderRowPresenter presenter)
            throw new InvalidOperationException($"{nameof(GridViewHeaderRowPresenter)} can not be set for {d.GetType()}!");

        if (!presenter.IsLoaded)
            presenter.Loaded += OnBrushPresenterLoaded;
        else
            SetBackground(presenter);
    }

    private static void OnBrushPresenterLoaded(object sender, RoutedEventArgs e)
    {
        var presenter = (GridViewHeaderRowPresenter)sender;
        presenter.Loaded -= OnBrushPresenterLoaded;
        SetBackground(presenter);
    }

    public static void SetFloatingIndicatorBrush(GridViewHeaderRowPresenter element, Brush value) => element.SetValue(FloatingIndicatorBrushProperty, value);

    public static Brush GetFloatingIndicatorBrush(GridViewHeaderRowPresenter element) => (Brush)element.GetValue(FloatingIndicatorBrushProperty);

    #endregion

    #region FloatingIndicatorWidth

    public static readonly DependencyProperty FloatingIndicatorWidthProperty = DependencyProperty.RegisterAttached(
        "FloatingIndicatorWidth", typeof(double), typeof(GridViewHeaderRowPresenterProps), new PropertyMetadata(default(double), PropertyChangedCallback));

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not GridViewHeaderRowPresenter presenter)
            throw new InvalidOperationException($"{nameof(GridViewHeaderRowPresenter)} can not be set for {d.GetType()}!");

        if (!presenter.IsLoaded)
            presenter.Loaded += OnWidthPresenterLoaded;
        else
            SetWidth(presenter);
    }

    private static void OnWidthPresenterLoaded(object sender, RoutedEventArgs e)
    {
        var presenter = (GridViewHeaderRowPresenter)sender;
        presenter.Loaded -= OnWidthPresenterLoaded;
        SetWidth(presenter);
    }

    public static void SetFloatingIndicatorWidth(GridViewHeaderRowPresenter element, double value) => element.SetValue(FloatingIndicatorWidthProperty, value);

    public static double GetFloatingIndicatorWidth(GridViewHeaderRowPresenter element) => (double)element.GetValue(FloatingIndicatorWidthProperty);

    #endregion

    #region FloatingIndicatorMargin

    public static readonly DependencyProperty FloatingIndicatorMarginProperty = DependencyProperty.RegisterAttached(
        "FloatingIndicatorMargin", typeof(Thickness), typeof(GridViewHeaderRowPresenterProps), new PropertyMetadata(default(Thickness), OnFloatingIndicatorMarginChanged));

    private static void OnFloatingIndicatorMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not GridViewHeaderRowPresenter presenter)
            throw new InvalidOperationException($"{nameof(GridViewHeaderRowPresenter)} can not be set for {d.GetType()}!");

        if (!presenter.IsLoaded)
            presenter.Loaded += OnMarginPresenterLoaded;
        else
            SetMargin(presenter);
    }

    private static void OnMarginPresenterLoaded(object sender, RoutedEventArgs e)
    {
        var presenter = (GridViewHeaderRowPresenter)sender;
        presenter.Loaded -= OnWidthPresenterLoaded;
        SetMargin(presenter);
    }

    public static void SetFloatingIndicatorMargin(GridViewHeaderRowPresenter element, Thickness value) => element.SetValue(FloatingIndicatorMarginProperty, value);

    public static Thickness GetFloatingIndicatorMargin(GridViewHeaderRowPresenter element) => (Thickness)element.GetValue(FloatingIndicatorMarginProperty);

    #endregion

    private static Separator? GetSeparator(GridViewHeaderRowPresenter presenter) =>
        typeof(GridViewHeaderRowPresenter).GetField("_indicator", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(presenter) as Separator;

    private static ControlTemplate CreateSeparatorControlTemplate(Brush brush)
    {
        var frameworkElementFactory = new FrameworkElementFactory(typeof(Border));
        frameworkElementFactory.SetValue(Border.BackgroundProperty, brush);
        var controlTemplate = new ControlTemplate(typeof(Separator))
        {
            VisualTree = frameworkElementFactory
        };
        controlTemplate.Seal();

        return controlTemplate;
    }

    private static void SetBackground(GridViewHeaderRowPresenter presenter)
    {
        if (GetSeparator(presenter) is { } separator)
            separator.Template = CreateSeparatorControlTemplate(GetFloatingIndicatorBrush(presenter));
    }

    private static void SetWidth(GridViewHeaderRowPresenter presenter)
    {
        if (GetSeparator(presenter) is { } separator)
            separator.Width = GetFloatingIndicatorWidth(presenter);
    }

    private static void SetMargin(GridViewHeaderRowPresenter presenter)
    {
        if (GetSeparator(presenter) is { } separator)
            separator.Margin = GetFloatingIndicatorMargin(presenter);
    }
}
