using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfExtensions.Controls;

public class PropertyValue : ContentControl
{
    static PropertyValue()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyValue), new FrameworkPropertyMetadata(typeof(PropertyValue)));
    }

    #region Title

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(PropertyValue), new PropertyMetadata(string.Empty));

    #endregion

    #region GroupName

    public static readonly DependencyProperty GroupNameProperty = DependencyProperty.RegisterAttached(
        "GroupName", typeof(string), typeof(PropertyValue), new PropertyMetadata(default(string), OnGroupNamePropertyChanged));

    private static void OnGroupNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Panel panel) return;

        Grid.SetIsSharedSizeScope(panel, true);

        if (!panel.IsLoaded)
            panel.Loaded += OnPanelLoaded;
        else
            SetGroupNameByVisualTree(panel, (string)e.NewValue);
    }

    private static void OnPanelLoaded(object sender, RoutedEventArgs e)
    {
        var root = (Panel)sender;
        root.Loaded -= OnPanelLoaded;
        SetGroupNameByVisualTree(root, GetGroupName(root));
    }

    public static void SetGroupName(DependencyObject element, string value) => element.SetValue(GroupNameProperty, value);

    public static string GetGroupName(DependencyObject element) => (string)element.GetValue(GroupNameProperty);

    #endregion

    #region HorizontalTitleAlignment

    public HorizontalAlignment HorizontalTitleAlignment
    {
        get => (HorizontalAlignment)GetValue(HorizontalTitleAlignmentProperty);
        set => SetValue(HorizontalTitleAlignmentProperty, value);
    }

    public static readonly DependencyProperty HorizontalTitleAlignmentProperty =
        DependencyProperty.Register(nameof(HorizontalTitleAlignment), typeof(HorizontalAlignment), typeof(PropertyValue), new PropertyMetadata(default(HorizontalAlignment)));

    #endregion

    #region VerticalTitleAlignment

    public VerticalAlignment VerticalTitleAlignment
    {
        get => (VerticalAlignment)GetValue(VerticalTitleAlignmentProperty);
        set => SetValue(VerticalTitleAlignmentProperty, value);
    }

    public static readonly DependencyProperty VerticalTitleAlignmentProperty =
        DependencyProperty.Register(nameof(VerticalTitleAlignment), typeof(VerticalAlignment), typeof(PropertyValue), new PropertyMetadata(default(VerticalAlignment)));

    #endregion

    private static void SetGroupNameByVisualTree(Panel root, string name)
    {
        var visualTree = new Stack<DependencyObject>(new[] { root });

        while (visualTree.Count != 0)
        {
            var current = visualTree.Pop();

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
            {
                var child = VisualTreeHelper.GetChild(current, i);

                if (child is PropertyValue propertyValue)
                {
                    SetGroupName(propertyValue, name);
                }

                visualTree.Push(child);
            }
        }
    }
}