using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WpfExtensions.AttachedDependencyProperties;

public static class GridProperties
{
    private static readonly char[] Separators = { ',', ' ' };

    private const char GroupSeparator = ':';

    private const string AutoString = "auto";

    private const char StarChar = '*';

    public static readonly DependencyProperty ColumnDefinitionsExProperty =
        DependencyProperty.RegisterAttached("ColumnDefinitionsEx", typeof(string), typeof(GridProperties), new PropertyMetadata(string.Empty, OnPropertyChanged));

    private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Grid grid)
            throw new InvalidOperationException($"{nameof(ColumnDefinitionsExProperty)} can not be set for {d.GetType()}!");

        grid.ColumnDefinitions.Clear();

        var columnsString = (string)e.NewValue;

        if (string.IsNullOrWhiteSpace(columnsString))
            throw new ArgumentException("Columns string is null or white space!");

        var columnStrings = columnsString.Split(Separators, StringSplitOptions.TrimEntries);

        foreach (var columnString in columnStrings)
        {
            if (columnString.Contains(GroupSeparator))
            {
                var columnParts = columnString.Split(GroupSeparator, StringSplitOptions.TrimEntries);

                if (columnParts.Length != 2)
                    throw new ArgumentException($"Can not parse '{columnString}' string!");

                var column = columnParts[0];
                var groupGroupName = columnParts[1];

                AddColumnToGrid(grid, ParseGridLength(column), groupGroupName);
            }
            else
            {
                AddColumnToGrid(grid, ParseGridLength(columnString));
            }
        }
    }

    /// <summary>
    /// Set <see cref="Grid.ColumnDefinitions"/> as string.
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
    /// </summary>
    /// <param name="o">Grid object</param>
    /// <param name="value">ColumnsDefinition as string</param>
    /// <exception cref="InvalidOperationException">An exception is thrown if you try to set a property other than <see cref="Grid"/></exception>
    /// <exception cref="ArgumentException">An exception is thrown if input <see cref="string"/> incorrect</exception>
    public static void SetColumnDefinitionsEx(DependencyObject o, string value) => o.SetValue(ColumnDefinitionsExProperty, value);

    /// <summary>
    /// Returns <see cref="Grid.ColumnDefinitions"/> as string.
    /// </summary>
    /// <param name="o">Grid object</param>
    /// <returns><see cref="Grid.ColumnDefinitions"/> as string</returns>
    public static string GetColumnDefinitionsEx(DependencyObject o) => (string)o.GetValue(ColumnDefinitionsExProperty);

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
}