using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace WpfExtensions.Behaviors;

public class SelectAllTextOnFocusBehaviour : Behavior<TextBox>
{
    protected override void OnAttached() => AssociatedObject.GotFocus += OnAssociatedObjectGotFocus;

    protected override void OnDetaching() => AssociatedObject.GotFocus -= OnAssociatedObjectGotFocus;

    private void OnAssociatedObjectGotFocus(object sender, RoutedEventArgs e)
    {
        AssociatedObject.SelectionChanged += OnAssociatedObjectSelectionChanged;
    }

    private void OnAssociatedObjectSelectionChanged(object sender, RoutedEventArgs e)
    {
        AssociatedObject.SelectionChanged -= OnAssociatedObjectSelectionChanged;
        AssociatedObject.SelectAll();
    }
}