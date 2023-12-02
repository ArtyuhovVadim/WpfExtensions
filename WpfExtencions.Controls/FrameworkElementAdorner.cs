using System.Collections;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfExtensions.Controls;

//Source: https://www.codeproject.com/Articles/54472/Defining-WPF-Adorners-in-XAML
public class FrameworkElementAdorner : Adorner
{
    private readonly FrameworkElement _child;

    public AdornerPlacement HorizontalAdornerPlacement { get; set; } = AdornerPlacement.Inside;
    public AdornerPlacement VerticalAdornerPlacement { get; set; } = AdornerPlacement.Inside;

    public double OffsetX { get; set; }
    public double OffsetY { get; set; }

    private new FrameworkElement AdornedElement => (FrameworkElement)base.AdornedElement;

    public FrameworkElementAdorner(FrameworkElement adornerChildElement, UIElement adornedElement) : base(adornedElement)
    {
        _child = adornerChildElement;

        AddLogicalChild(adornerChildElement);
        AddVisualChild(adornerChildElement);
    }

    public FrameworkElementAdorner(FrameworkElement adornerChildElement, FrameworkElement adornedElement, AdornerPlacement horizontalAdornerPlacement, AdornerPlacement verticalAdornerPlacement, double offsetX, double offsetY)
        : base(adornedElement)
    {
        _child = adornerChildElement;
        HorizontalAdornerPlacement = horizontalAdornerPlacement;
        VerticalAdornerPlacement = verticalAdornerPlacement;
        OffsetX = offsetX;
        OffsetY = offsetY;

        adornedElement.SizeChanged += OnAdornedElementSizeChanged;

        AddLogicalChild(adornerChildElement);
        AddVisualChild(adornerChildElement);
    }

    public double PositionX { get; set; } = double.NaN;

    public double PositionY { get; set; } = double.NaN;

    public void DisconnectChild()
    {
        RemoveLogicalChild(_child);
        RemoveVisualChild(_child);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var x = PositionX;
        if (double.IsNaN(x))
        {
            x = DetermineX();
        }
        var y = PositionY;
        if (double.IsNaN(y))
        {
            y = DetermineY();
        }
        var adornerWidth = DetermineWidth();
        var adornerHeight = DetermineHeight();
        _child.Arrange(new Rect(x, y, adornerWidth, adornerHeight));
        return finalSize;
    }

    protected override int VisualChildrenCount => 1;

    protected override Visual GetVisualChild(int index) => _child;

    protected override IEnumerator LogicalChildren => new ArrayList { _child }.GetEnumerator();

    protected override Size MeasureOverride(Size constraint)
    {
        _child.Measure(constraint);
        return _child.DesiredSize;
    }

    private double DetermineX()
    {
        switch (_child.HorizontalAlignment)
        {
            case HorizontalAlignment.Left:
                {
                    if (HorizontalAdornerPlacement == AdornerPlacement.Outside)
                    {
                        return -_child.DesiredSize.Width + OffsetX;
                    }

                    return OffsetX;
                }
            case HorizontalAlignment.Right:
                {
                    if (HorizontalAdornerPlacement == AdornerPlacement.Outside)
                    {
                        var adornedWidth = AdornedElement.ActualWidth;
                        return adornedWidth + OffsetX;
                    }
                    else
                    {
                        var adornerWidth = _child.DesiredSize.Width;
                        var adornedWidth = AdornedElement.ActualWidth;
                        var x = adornedWidth - adornerWidth;
                        return x + OffsetX;
                    }
                }
            case HorizontalAlignment.Center:
                {
                    var adornerWidth = _child.DesiredSize.Width;
                    var adornedWidth = AdornedElement.ActualWidth;
                    var x = adornedWidth / 2 - adornerWidth / 2;
                    return x + OffsetX;
                }
            case HorizontalAlignment.Stretch: return 0.0;
            default: return 0.0;
        }
    }

    private double DetermineY()
    {
        switch (_child.VerticalAlignment)
        {
            case VerticalAlignment.Top:
                {
                    if (VerticalAdornerPlacement == AdornerPlacement.Outside)
                    {
                        return -_child.DesiredSize.Height + OffsetY;
                    }

                    return OffsetY;
                }
            case VerticalAlignment.Bottom:
                {
                    if (VerticalAdornerPlacement == AdornerPlacement.Outside)
                    {
                        var adornedHeight = AdornedElement.ActualHeight;
                        return adornedHeight + OffsetY;
                    }
                    else
                    {
                        var adornerHeight = _child.DesiredSize.Height;
                        var adornedHeight = AdornedElement.ActualHeight;
                        var x = adornedHeight - adornerHeight;
                        return x + OffsetY;
                    }
                }
            case VerticalAlignment.Center:
                {
                    var adornerHeight = _child.DesiredSize.Height;
                    var adornedHeight = AdornedElement.ActualHeight;
                    var x = adornedHeight / 2 - adornerHeight / 2;
                    return x + OffsetY;
                }
            case VerticalAlignment.Stretch: return 0.0;
            default: return 0.0;
        }
    }

    private double DetermineWidth()
    {
        if (!double.IsNaN(PositionX)) return _child.DesiredSize.Width;

        return _child.HorizontalAlignment switch
        {
            HorizontalAlignment.Left => _child.DesiredSize.Width,
            HorizontalAlignment.Right => _child.DesiredSize.Width,
            HorizontalAlignment.Center => _child.DesiredSize.Width,
            HorizontalAlignment.Stretch => AdornedElement.ActualWidth,
            _ => 0.0
        };
    }

    private double DetermineHeight()
    {
        if (!double.IsNaN(PositionY)) return _child.DesiredSize.Height;

        return _child.VerticalAlignment switch
        {
            VerticalAlignment.Top => _child.DesiredSize.Height,
            VerticalAlignment.Bottom => _child.DesiredSize.Height,
            VerticalAlignment.Center => _child.DesiredSize.Height,
            VerticalAlignment.Stretch => AdornedElement.ActualHeight,
            _ => 0.0
        };
    }

    private void OnAdornedElementSizeChanged(object sender, SizeChangedEventArgs e) => InvalidateMeasure();
}