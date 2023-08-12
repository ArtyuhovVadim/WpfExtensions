using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfExtensions.AttachedDependencyProperties;

public static class GridProps
{
    private enum RowOrColumn
    {
        Row,
        Column
    }

    private static readonly char[] Separators = { ',', ' ' };

    private const char GroupSeparator = ':';

    private const string AutoString = "auto";

    private const char StarChar = '*';

    #region ShowGridLinesExProperty

    public static readonly DependencyProperty ShowGridLinesExProperty =
        DependencyProperty.RegisterAttached("ShowGridLinesEx", typeof(bool), typeof(GridProps), new PropertyMetadata(false, OnShowGridLinesExPropertyChanged));

    private static void OnShowGridLinesExPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element)
            throw new InvalidOperationException($"{nameof(ShowGridLinesExProperty)} can not be set for {d.GetType()}!");

        element.Loaded += OnElementLoaded;
    }

    private static void OnElementLoaded(object sender, RoutedEventArgs e)
    {
        var element = (FrameworkElement)sender;
        element.Loaded -= OnElementLoaded;
        SetShowGridLinesByVisualTree(element, GetShowGridLinesEx(element));
    }

    public static void SetShowGridLinesEx(FrameworkElement o, bool value) => o.SetValue(ShowGridLinesExProperty, value);

    public static bool GetShowGridLinesEx(FrameworkElement o) => (bool)o.GetValue(ShowGridLinesExProperty);

    #endregion

    #region ColumnDefinitionsExProperty

    public static readonly DependencyProperty ColumnDefinitionsExProperty =
        DependencyProperty.RegisterAttached("ColumnDefinitionsEx", typeof(string), typeof(GridProps), new PropertyMetadata(string.Empty, OnColumnDefinitionsExPropertyChanged));

    private static void OnColumnDefinitionsExPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Grid grid)
            throw new InvalidOperationException($"{nameof(ColumnDefinitionsExProperty)} can not be set for {d.GetType()}!");

        grid.ColumnDefinitions.Clear();

        var columnsString = (string)e.NewValue;

        if (string.IsNullOrWhiteSpace(columnsString))
            throw new ArgumentException("Columns string is null or white space!");

        var columnStrings = columnsString.Split(Separators, StringSplitOptions.TrimEntries);

        AddRowsOrColumnsToGrid(grid, columnStrings, RowOrColumn.Column);
    }

    /// <summary>
    /// Set <see cref="Grid.ColumnDefinitions"/> as string.
    /// <example>
    /// <c>
    /// <list type="bullet">
    /// <item>Use '*' for &lt;ColumnDefinition Width="*"&gt;</item>
    /// <item>Use '1.5*' for &lt;ColumnDefinition Width="1.5*"&gt;</item>
    /// <item>Use 'Auto' for &lt;ColumnDefinition Width="Auto"&gt;</item>
    /// <item>Use '300' for &lt;ColumnDefinition Width="300"&gt;</item>
    /// <item>Use '*:SomeName' for &lt;ColumnDefinition Width="*" SharedSizeGroup="SomeName"&gt;</item>
    /// <item>Use '1.5*:SomeName' for &lt;ColumnDefinition Width="1.5*" SharedSizeGroup="SomeName"&gt;</item>
    /// <item>Use 'Auto:SomeName' for &lt;ColumnDefinition Width="Auto" SharedSizeGroup="SomeName"&gt;</item>
    /// <item>Use '300:SomeName' for &lt;ColumnDefinition Width="300" SharedSizeGroup="SomeName"&gt;</item>
    /// </list>
    /// Example strings:
    /// <list type="table">
    /// <item>"*, 1.5*, 2*, Auto, 300"</item>
    /// <item>"* 1.5* 2* Auto 300"</item>
    /// <item>"*:AGroup 1.5* 2* Auto:BGroup 300"</item>
    /// </list>
    /// </c>
    /// </example>
    /// </summary>
    /// <param name="o">Grid object</param>
    /// <param name="value">ColumnsDefinition as string</param>
    /// <exception cref="InvalidOperationException">An exception is thrown if you try to set a property other than <see cref="Grid"/></exception>
    /// <exception cref="ArgumentException">An exception is thrown if input <see cref="string"/> incorrect</exception>
    public static void SetColumnDefinitionsEx(Grid o, string value) => o.SetValue(ColumnDefinitionsExProperty, value);

    /// <summary>
    /// Returns <see cref="Grid.ColumnDefinitions"/> as string.
    /// </summary>
    /// <param name="o">Grid object</param>
    /// <returns><see cref="Grid.ColumnDefinitions"/> as string</returns>
    public static string GetColumnDefinitionsEx(Grid o) => (string)o.GetValue(ColumnDefinitionsExProperty);

    #endregion

    #region RowDefinitionsExProperty

    public static readonly DependencyProperty RowDefinitionsExProperty =
        DependencyProperty.RegisterAttached("RowDefinitionsEx", typeof(string), typeof(GridProps), new PropertyMetadata(string.Empty, OnRowDefinitionsExPropertyChanged));

    private static void OnRowDefinitionsExPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Grid grid)
            throw new InvalidOperationException($"{nameof(RowDefinitionsExProperty)} can not be set for {d.GetType()}!");

        grid.RowDefinitions.Clear();

        var rowsString = (string)e.NewValue;

        if (string.IsNullOrWhiteSpace(rowsString))
            throw new ArgumentException("Rows string is null or white space!");

        var rowStrings = rowsString.Split(Separators, StringSplitOptions.TrimEntries);

        AddRowsOrColumnsToGrid(grid, rowStrings, RowOrColumn.Row);
    }

    /// <summary>
    /// Set <see cref="Grid.RowDefinitions"/> as string.
    /// <example>
    /// <c>
    /// <list type="bullet">
    /// <item>Use '*' for &lt;RowDefinition Height="*"&gt;</item>
    /// <item>Use '1.5*' for &lt;RowDefinition Height="1.5*"&gt;</item>
    /// <item>Use 'Auto' for &lt;RowDefinition Height="Auto"&gt;</item>
    /// <item>Use '300' for &lt;RowDefinition Height="300"&gt;</item>
    /// <item>Use '*:SomeName' for &lt;RowDefinition Height="*" SharedSizeGroup="SomeName"&gt;</item>
    /// <item>Use '1.5*:SomeName' for &lt;RowDefinition Height="1.5*" SharedSizeGroup="SomeName"&gt;</item>
    /// <item>Use 'Auto:SomeName' for &lt;RowDefinition Height="Auto" SharedSizeGroup="SomeName"&gt;</item>
    /// <item>Use '300:SomeName' for &lt;RowDefinition Height="300" SharedSizeGroup="SomeName"&gt;</item>
    /// </list>
    /// Example strings:
    /// <list type="table">
    /// <item>"*, 1.5*, 2*, Auto, 300"</item>
    /// <item>"* 1.5* 2* Auto 300"</item>
    /// <item>"*:AGroup 1.5* 2* Auto:BGroup 300"</item>
    /// </list>
    /// </c>
    /// </example>
    /// </summary>
    /// <param name="o">Grid object</param>
    /// <param name="value">RowsDefinition as string</param>
    /// <exception cref="InvalidOperationException">An exception is thrown if you try to set a property other than <see cref="Grid"/></exception>
    /// <exception cref="ArgumentException">An exception is thrown if input <see cref="string"/> incorrect</exception>
    public static void SetRowDefinitionsEx(Grid o, string value) => o.SetValue(RowDefinitionsExProperty, value);

    /// <summary>
    /// Returns <see cref="Grid.RowDefinitions"/> as string.
    /// </summary>
    /// <param name="o">Grid object</param>
    /// <returns><see cref="Grid.RowDefinitions"/> as string</returns>
    public static string GetRowDefinitionsEx(Grid o) => (string)o.GetValue(RowDefinitionsExProperty);

    #endregion

    private static void AddRowsOrColumnsToGrid(Grid grid, string[] strings, RowOrColumn rowOrColumn)
    {
        foreach (var str in strings)
        {
            if (str.Contains(GroupSeparator))
            {
                var parts = str.Split(GroupSeparator, StringSplitOptions.TrimEntries);

                if (parts.Length != 2)
                    throw new ArgumentException($"Can not parse '{str}' string!");

                var value = parts[0];
                var groupGroupName = parts[1];

                switch (rowOrColumn)
                {
                    case RowOrColumn.Row: AddRowToGrid(grid, ParseGridLength(value), groupGroupName); break;
                    case RowOrColumn.Column: AddColumnToGrid(grid, ParseGridLength(value), groupGroupName); break;
                }
            }
            else
            {
                switch (rowOrColumn)
                {
                    case RowOrColumn.Row: AddRowToGrid(grid, ParseGridLength(str)); break;
                    case RowOrColumn.Column: AddColumnToGrid(grid, ParseGridLength(str)); break;
                }
            }
        }
    }

    private static GridLength ParseGridLength(string columnString)
    {
        if (columnString.Equals(AutoString, StringComparison.InvariantCultureIgnoreCase))
        {
            return GridLength.Auto;
        }

        if (double.TryParse(columnString, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out var columnsWidth))
        {
            return new GridLength(columnsWidth);
        }

        if (columnString.EndsWith(StarChar))
        {
            var starWidth = 1d;

            if (columnString.Length > 1 && !double.TryParse(columnString.AsSpan(0, columnString.Length - 1), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out starWidth))
            {
                throw new ArgumentException($"Can not parse '{columnString}' string!");
            }

            return new GridLength(starWidth, GridUnitType.Star);
        }

        throw new ArgumentException($"Can not parse '{columnString}' string!");
    }

    private static void AddColumnToGrid(Grid grid, GridLength length)
    {
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = length });
    }

    private static void AddColumnToGrid(Grid grid, GridLength length, string groupName)
    {
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = length, SharedSizeGroup = groupName });
    }

    private static void AddRowToGrid(Grid grid, GridLength length)
    {
        grid.RowDefinitions.Add(new RowDefinition { Height = length });
    }

    private static void AddRowToGrid(Grid grid, GridLength length, string groupName)
    {
        grid.RowDefinitions.Add(new RowDefinition { Height = length, SharedSizeGroup = groupName });
    }

    private static void SetShowGridLinesByVisualTree(DependencyObject root, bool showGridlines)
    {
        if (root is Grid grid)
        {
            grid.ShowGridLines = showGridlines;
        }

        var visualTree = new Stack<DependencyObject>(new[] { root });

        while (visualTree.Count != 0)
        {
            var current = visualTree.Pop();

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
            {
                var child = VisualTreeHelper.GetChild(current, i);

                if (child is Grid newGrid)
                {
                    newGrid.ShowGridLines = showGridlines;
                }

                visualTree.Push(child);
            }
        }
    }
}