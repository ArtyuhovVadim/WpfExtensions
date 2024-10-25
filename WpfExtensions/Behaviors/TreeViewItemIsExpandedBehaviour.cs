using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using WpfExtensions.Utils;

namespace WpfExtensions.Behaviors;

public class TreeViewItemIsExpandedBehaviour : Behavior<FrameworkElement>
{
    #region IsExpanded

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(TreeViewItemIsExpandedBehaviour), new PropertyMetadata(default(bool)));

    #endregion

    protected override void OnAttached() => AssociatedObject.Loaded += OnLoaded;

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        AssociatedObject.Loaded -= OnLoaded;

        var treeViewItem = AssociatedObject.FindVisualParent<TreeViewItem>();

        if (treeViewItem is null)
            return;

        BindingOperations.SetBinding(treeViewItem, TreeViewItem.IsExpandedProperty, new Binding(nameof(IsExpanded)) { Source = this, Mode = BindingMode.TwoWay });
    }
}
