using System.Windows;
using System.Windows.Controls;

namespace WpfExtensions.Controls;

public class StackPanelEx : StackPanel
{
    #region Spacing

    public Thickness Spacing
    {
        get => (Thickness)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public static readonly DependencyProperty SpacingProperty =
        DependencyProperty.Register(nameof(Spacing), typeof(Thickness), typeof(StackPanelEx),
            new FrameworkPropertyMetadata(new Thickness(), FrameworkPropertyMetadataOptions.AffectsMeasure));

    #endregion

    protected override Size MeasureOverride(Size constraint)
    {
        var count = InternalChildren.Count;

        for (var i = 0; i < count; i++)
        {
            var child = InternalChildren[i];

            if (child is StackPanelExDecorator decorator)
            {
                if (decorator.Margin != Spacing)
                    decorator.Margin = Spacing;

                continue;
            }

            InternalChildren.RemoveAt(i);
            InternalChildren.Insert(i, new StackPanelExDecorator { Child = child, Margin = Spacing });
        }

        return base.MeasureOverride(constraint);
    }
}