using Microsoft.Xaml.Behaviors;
using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows;

namespace WpfExtensions.Behaviors;

public class DataGridScrollToLastItemBehaviour : Behavior<DataGrid>
{
    #region ScrollOnLoaded

    public bool ScrollOnLoaded
    {
        get => (bool)GetValue(ScrollOnLoadedProperty);
        set => SetValue(ScrollOnLoadedProperty, value);
    }

    public static readonly DependencyProperty ScrollOnLoadedProperty =
        DependencyProperty.Register(nameof(ScrollOnLoaded), typeof(bool), typeof(DataGridScrollToLastItemBehaviour), new PropertyMetadata(false));

    #endregion

    protected override void OnAttached()
    {
        if (!AssociatedObject.IsLoaded)
        {
            AssociatedObject.Loaded += OnLoaded;
            return;
        }

        if (AssociatedObject.ItemsSource is not INotifyCollectionChanged collection) return;

        collection.CollectionChanged += OnCollectionChanged;
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject.ItemsSource is not INotifyCollectionChanged collection) return;

        collection.CollectionChanged -= OnCollectionChanged;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            ScrollToLastItem();
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (AssociatedObject.ItemsSource is not INotifyCollectionChanged collection) return;

        if (ScrollOnLoaded)
            ScrollToLastItem();

        collection.CollectionChanged += OnCollectionChanged;
        AssociatedObject.Loaded -= OnLoaded;
    }

    private void ScrollToLastItem() => AssociatedObject.ScrollIntoView(AssociatedObject.ItemsSource.Cast<object>().Last());
}