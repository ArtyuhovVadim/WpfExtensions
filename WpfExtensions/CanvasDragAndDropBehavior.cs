using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace WpfExtensions;

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

    #region NormalizedPosition

    public Point NormalizedPosition
    {
        get => (Point)GetValue(NormalizedPositionProperty);
        set => SetValue(NormalizedPositionProperty, value);
    }

    public static readonly DependencyProperty NormalizedPositionProperty =
        DependencyProperty.Register(nameof(NormalizedPosition), typeof(Point), typeof(CanvasDragAndDropBehavior), new PropertyMetadata(default(Point), OnNormalizedPositionChanged));

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
            X = NormalizedPosition.X * AssociatedObject.ActualWidth,
            Y = NormalizedPosition.Y * AssociatedObject.ActualHeight
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
            NormalizedPosition = new Point
            {
                X = pos.X / AssociatedObject.ActualWidth,
                Y = pos.Y / AssociatedObject.ActualHeight
            };
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
            X = behavior.NormalizedPosition.X * behavior.AssociatedObject.ActualWidth,
            Y = behavior.NormalizedPosition.Y * behavior.AssociatedObject.ActualHeight
        }, false);
    }
}