using System.Windows;
using System.Windows.Controls;

namespace WpfExtensions.Controls;

public class StackPanelEx : StackPanel
{
    #region Gap

    public double Gap
    {
        get => (double)GetValue(GapProperty);
        set => SetValue(GapProperty, value);
    }

    public static readonly DependencyProperty GapProperty =
        DependencyProperty.Register(nameof(Gap), typeof(double), typeof(StackPanelEx),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure));

    #endregion

    protected override Size MeasureOverride(Size constraint)
    {
        if (Gap == 0)
            return base.MeasureOverride(constraint);

        var baseSize = base.MeasureOverride(constraint);
        var childrenCount = GetNonZeroSizeChildren().Count();

        var resultSize = Orientation switch
        {
            Orientation.Horizontal => new Size(baseSize.Width + Math.Max(0, childrenCount - 1) * Gap, baseSize.Height),
            Orientation.Vertical => new Size(baseSize.Width, baseSize.Height + Math.Max(0, childrenCount - 1) * Gap),
            _ => throw new NotSupportedException()
        };

        return resultSize;
    }

    protected override Size ArrangeOverride(Size arrangeSize)
    {
        if (Gap == 0)
            return base.ArrangeOverride(arrangeSize);

        _ = base.ArrangeOverride(arrangeSize);

        var acc = 0d;

        foreach (var child in GetNonZeroSizeChildren())
        {
            var rect = Orientation switch
            {
                Orientation.Horizontal => new Rect(new Point(acc, 0), new Size(child.DesiredSize.Width, arrangeSize.Height)),
                Orientation.Vertical => new Rect(new Point(0, acc), new Size(arrangeSize.Width, child.DesiredSize.Height)),
                _ => throw new NotSupportedException()
            };

            child.Arrange(rect);

            acc += Orientation switch
            {
                Orientation.Horizontal => rect.Width + Gap,
                Orientation.Vertical => rect.Height + Gap,
                _ => throw new NotSupportedException()
            };
        }

        return arrangeSize;
    }

    private IEnumerable<UIElement> GetNonZeroSizeChildren() => InternalChildren
        .Cast<UIElement>()
        .Where(uiElement => uiElement.Visibility != Visibility.Collapsed)
        .Where(element => element is not { DesiredSize: not { Width: > 0, Height: > 0 } });
}