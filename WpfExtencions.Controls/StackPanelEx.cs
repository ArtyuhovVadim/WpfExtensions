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
        var count = InternalChildren.Count;

        for (var i = 0; i < count; i++)
        {
            var child = InternalChildren[i];

            if (child is StackPanelExDecorator decorator)
            {
                if (Math.Abs(decorator.Margin.Top - Gap) > 0.001d || Math.Abs(decorator.Margin.Left - Gap) > 0.001d)
                    decorator.Margin = GetGap(decorator.Child, i);

                continue;
            }

            InternalChildren.RemoveAt(i);
            InternalChildren.Insert(i, new StackPanelExDecorator { Child = child, Margin = GetGap(child, i) });
        }

        return base.MeasureOverride(constraint);
    }

    private Thickness GetGap(UIElement child, int childIndex)
    {
        if (childIndex == 0) return new Thickness();

        return Orientation is Orientation.Horizontal ?
               new Thickness(Gap, 0, 0, 0) :
               new Thickness(0, Gap, 0, 0);
    }
}