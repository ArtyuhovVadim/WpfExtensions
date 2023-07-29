using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace WpfExtensions.Behaviors;

public class CanvasDragAndDropBehavior : Behavior<Canvas>
{
    #region ObjectToDrag

    public UIElement ObjectToDrag
    {
        get => (UIElement)GetValue(ObjectToDragProperty);
        set => SetValue(ObjectToDragProperty, value);
    }

    public static readonly DependencyProperty ObjectToDragProperty =
        DependencyProperty.Register(nameof(ObjectToDrag), typeof(UIElement), typeof(CanvasDragAndDropBehavior), new PropertyMetadata(default(UIElement)));

    #endregion

    #region NormalizedX

    public double NormalizedX
    {
        get => (double)GetValue(NormalizedXProperty);
        set => SetValue(NormalizedXProperty, value);
    }

    public static readonly DependencyProperty NormalizedXProperty =
        DependencyProperty.Register(nameof(NormalizedX), typeof(double), typeof(CanvasDragAndDropBehavior), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNormalizedPositionChanged));

    #endregion

    #region NormalizedY

    public double NormalizedY
    {
        get => (double)GetValue(NormalizedYProperty);
        set => SetValue(NormalizedYProperty, value);
    }

    public static readonly DependencyProperty NormalizedYProperty =
        DependencyProperty.Register(nameof(NormalizedY), typeof(double), typeof(CanvasDragAndDropBehavior), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNormalizedPositionChanged));

    #endregion

    protected override void OnAttached()
    {
        AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
        AssociatedObject.MouseMove += OnMouseMove;
        AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;

        AssociatedObject.Loaded += OnLoaded;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
        AssociatedObject.PreviewMouseMove -= OnMouseMove;
        AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;

        AssociatedObject.Loaded -= OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!AssociatedObject.IsMeasureValid) return;

        SetPosition(new Point
        {
            X = NormalizedX * AssociatedObject.ActualWidth,
            Y = NormalizedY * AssociatedObject.ActualHeight
        }, true);
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (ObjectToDrag is null) return;

        AssociatedObject.CaptureMouse();

        var pos = e.GetPosition(AssociatedObject);

        SetPosition(pos, true);
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (ObjectToDrag is null) return;

        if (e.LeftButton != MouseButtonState.Pressed) return;

        var pos = e.GetPosition(AssociatedObject);

        SetPosition(pos, true);
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (ObjectToDrag is null) return;

        Mouse.Capture(null);
    }

    private void SetPosition(Point pos, bool updateNormalizedPosition)
    {
        if (ObjectToDrag is null) return;

        if (pos.X < 0) pos.X = 0;
        if (pos.X > AssociatedObject.ActualWidth) pos.X = AssociatedObject.ActualWidth;
        if (pos.Y < 0) pos.Y = 0;
        if (pos.Y > AssociatedObject.ActualHeight) pos.Y = AssociatedObject.ActualHeight;

        if (updateNormalizedPosition)
        {
            NormalizedX = pos.X / AssociatedObject.ActualWidth;
            NormalizedY = pos.Y / AssociatedObject.ActualHeight;
        }

        Canvas.SetLeft(ObjectToDrag, pos.X);
        Canvas.SetTop(ObjectToDrag, pos.Y);
    }

    private static void OnNormalizedPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not CanvasDragAndDropBehavior behavior) return;

        if (behavior.AssociatedObject is null) return;

        behavior.SetPosition(new Point
        {
            X = behavior.NormalizedX * behavior.AssociatedObject.ActualWidth,
            Y = behavior.NormalizedY * behavior.AssociatedObject.ActualHeight
        }, false);
    }
}