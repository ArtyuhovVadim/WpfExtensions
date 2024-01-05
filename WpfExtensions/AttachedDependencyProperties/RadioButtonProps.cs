using System.Windows;
using System.Windows.Controls;
using WpfExtensions.Utils;

namespace WpfExtensions.AttachedDependencyProperties;

public static class RadioButtonProps
{
    public static readonly DependencyProperty GroupNameProperty = DependencyProperty.RegisterAttached(
        "GroupName", typeof(string), typeof(RadioButtonProps), new PropertyMetadata(string.Empty, OnGroupNamePropertyChanged));

    public static void SetGroupName(DependencyObject element, string value) => element.SetValue(GroupNameProperty, value);

    public static string GetGroupName(DependencyObject element) => (string)element.GetValue(GroupNameProperty);

    private static void OnGroupNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement frameworkElement) return;

        if (!frameworkElement.IsLoaded)
            frameworkElement.Loaded += OnLoaded;
        else
            SetGroupNameByVisualTree(frameworkElement, (string)e.NewValue);
    }

    private static void OnLoaded(object sender, RoutedEventArgs e)
    {
        var root = (FrameworkElement)sender;
        root.Loaded -= OnLoaded;
        SetGroupNameByVisualTree(root, GetGroupName(root));
    }

    private static void SetGroupNameByVisualTree(FrameworkElement panel, string groupName)
    {
        panel.ProcessVisualTreeNodes<RadioButton>(button =>
        {
            button.GroupName = groupName;
        });
    }
}