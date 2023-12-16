﻿using System.Windows;
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

    protected override Size ArrangeOverride(Size arrangeSize)
    {
        if (Gap == 0)
            return base.ArrangeOverride(arrangeSize);

        var acc = 0d;

        foreach (UIElement child in InternalChildren)
        {
            var rect = Orientation switch
            {
                Orientation.Horizontal => new Rect(new Point(acc, 0), new Size(child.DesiredSize.Width, arrangeSize.Height)),
                Orientation.Vertical => new Rect(new Point(0, acc), new Size(arrangeSize.Width, child.DesiredSize.Height)),
            };

            child.Arrange(rect);

            acc += Orientation switch
            {
                Orientation.Horizontal => rect.Width + Gap,
                Orientation.Vertical => rect.Height + Gap,
            };
        }

        return arrangeSize;
    }
}