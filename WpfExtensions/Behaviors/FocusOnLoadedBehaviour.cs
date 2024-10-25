using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows;

namespace WpfExtensions.Behaviors;

public class FocusOnLoadedBehaviour : Behavior<Control>
{
    protected override void OnAttached() => AssociatedObject.Loaded += OnLoaded;

    protected override void OnDetaching() => AssociatedObject.Loaded -= OnLoaded;

    private void OnLoaded(object sender, RoutedEventArgs e) => AssociatedObject.Focus();
}
