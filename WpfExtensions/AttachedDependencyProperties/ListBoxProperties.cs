using System.Windows.Controls;
using System.Windows;
using System;

namespace WpfExtensions.AttachedDependencyProperties;

public static class ListBoxProperties
{
    #region AutoScrollToSelectedItemProperty

    public static readonly DependencyProperty AutoScrollToSelectedItemProperty =
        DependencyProperty.RegisterAttached("AutoScrollToSelectedItem", typeof(bool), typeof(ListBoxProperties), new PropertyMetadata(false, OnAutoScrollToSelectedItemPropertyChanged));

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

    public static void SetAutoScrollToSelectedItem(DependencyObject o, bool value) => o.SetValue(AutoScrollToSelectedItemProperty, value);

    public static bool GetAutoScrollToSelectedItem(DependencyObject o) => (bool)o.GetValue(AutoScrollToSelectedItemProperty);

    #endregion
}