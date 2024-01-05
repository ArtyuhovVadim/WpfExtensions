using System.Windows.Controls;
using System.Windows;

namespace WpfExtensions.AttachedDependencyProperties;

public static class ListBoxProps
{
    #region AutoScrollToSelectedItemProperty

    public static readonly DependencyProperty AutoScrollToSelectedItemProperty =
        DependencyProperty.RegisterAttached("AutoScrollToSelectedItem", typeof(bool), typeof(ListBoxProps), new PropertyMetadata(false, OnAutoScrollToSelectedItemPropertyChanged));

    private static void OnAutoScrollToSelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ListBox selector)
            throw new InvalidOperationException($"{nameof(AutoScrollToSelectedItemProperty)} can not be set for {d.GetType()}!");

        var newValue = (bool)e.NewValue;

        if (newValue) selector.SelectionChanged += OnSelectionChanged;
        else selector.SelectionChanged -= OnSelectionChanged;
    }

    private static void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var listBox = (ListBox)sender;

        listBox.ScrollIntoView(listBox.SelectedItem);
    }

    public static void SetAutoScrollToSelectedItem(ListBox o, bool value) => o.SetValue(AutoScrollToSelectedItemProperty, value);

    public static bool GetAutoScrollToSelectedItem(ListBox o) => (bool)o.GetValue(AutoScrollToSelectedItemProperty);

    #endregion
}