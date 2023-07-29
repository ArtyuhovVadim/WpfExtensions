using System.Windows;
using System.Windows.Controls.Primitives;
using Microsoft.Xaml.Behaviors;

namespace WpfExtensions.Controls.ColorPicker;

public class ColorPaletteBehaviour : Behavior<UniformGrid>
{
    #region BarWidth

    public double BarWidth
    {
        get => (double)GetValue(BarWidthProperty);
        set => SetValue(BarWidthProperty, value);
    }

    public static readonly DependencyProperty BarWidthProperty =
        DependencyProperty.Register(nameof(BarWidth), typeof(double), typeof(ColorPaletteBehaviour), new PropertyMetadata(default(double)));

    #endregion

    protected override void OnAttached()
    {
        AssociatedObject.Loaded += OnLoaded;
        AssociatedObject.SizeChanged += OnSizeChanged;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.SizeChanged -= OnSizeChanged;
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (!AssociatedObject.IsLoaded)
            return;

        CalculateMargin();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        AssociatedObject.Loaded -= OnLoaded;

        CalculateMargin();
    }

    private void CalculateMargin()
    {
        var margin = (AssociatedObject.ActualWidth / AssociatedObject.Columns - BarWidth) / 2;

        if (!double.IsNormal(margin))
            return;

        AssociatedObject.Margin = new Thickness(-margin, 0, -margin, 0);
    }
}