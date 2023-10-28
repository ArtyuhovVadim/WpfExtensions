using System.Windows;
using System.Windows.Controls;

namespace WpfExtensions.Controls;

public class GridEx : Grid
{
    #region RowGap

    public double RowGap
    {
        get => (double)GetValue(RowGapProperty);
        set => SetValue(RowGapProperty, value);
    }

    public static readonly DependencyProperty RowGapProperty =
        DependencyProperty.Register(nameof(RowGap), typeof(double), typeof(GridEx), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsMeasure));

    #endregion

    #region ColumnGap

    public double ColumnGap
    {
        get => (double)GetValue(ColumnGapProperty);
        set => SetValue(ColumnGapProperty, value);
    }

    public static readonly DependencyProperty ColumnGapProperty =
        DependencyProperty.Register(nameof(ColumnGap), typeof(double), typeof(GridEx), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsMeasure));

    #endregion

    protected override Size MeasureOverride(Size constraint)
    {
        if (ColumnGap == 0 && RowGap == 0)
            return base.MeasureOverride(constraint);

        var count = InternalChildren.Count;

        for (var i = 0; i < count; i++)
        {
            var child = InternalChildren[i];

            if (child is GridExDecorator decorator)
            {
                if (Math.Abs(decorator.Margin.Top - RowGap) > 0.001d || Math.Abs(decorator.Margin.Left - ColumnGap) > 0.001d)
                    decorator.Margin = GetGap(decorator.Child);

                continue;
            }

            InternalChildren.RemoveAt(i);

            var newDecorator = new GridExDecorator { Child = child, Margin = GetGap(child) };

            SetColumn(newDecorator, GetColumn(child));
            SetRow(newDecorator, GetRow(child));
            SetRowSpan(newDecorator, GetRowSpan(child));
            SetColumnSpan(newDecorator, GetColumnSpan(child));
            SetIsSharedSizeScope(newDecorator, GetIsSharedSizeScope(child));

            InternalChildren.Insert(i, newDecorator);
        }

        return base.MeasureOverride(constraint);
    }

    private Thickness GetGap(UIElement child)
    {
        var gapRow = 0d;
        var gapColumn = 0d;

        if (GetColumn(child) != 0)
            gapColumn = ColumnGap;

        if (GetRow(child) != 0)
            gapRow = RowGap;

        return new Thickness(gapColumn, gapRow, 0, 0);
    }
}