using System.Windows.Media;
using System.Windows;

namespace WpfExtensions.Utils;

// Source: https://github.com/Infarh/MathCore.WPF/blob/dev/MathCore.WPF/Extensions/VisualTreeHelperExtensions.cs
public static class VisualTreeUtils
{
    public static T? FindVisualParent<T>(this DependencyObject? obj) where T : class
    {
        if (obj is null) return null;
        var target = obj;
        do { target = VisualTreeHelper.GetParent(target); } while (target != null && target is not T);
        return target as T;
    }

    public static DependencyObject? FindLogicalRoot(this DependencyObject? obj)
    {
        if (obj is null) return null;
        do
        {
            var parent = LogicalTreeHelper.GetParent(obj);
            if (parent is null) return obj;
            obj = parent;
        } while (true);
    }

    public static DependencyObject? FindVisualRoot(this DependencyObject? obj)
    {
        if (obj is null) return null;
        do
        {
            var parent = VisualTreeHelper.GetParent(obj);
            if (parent is null) return obj;
            obj = parent;
        } while (true);
    }

    public static T? FindLogicalParent<T>(this DependencyObject? obj) where T : class
    {
        if (obj is null) return null;
        var target = obj;
        do { target = LogicalTreeHelper.GetParent(target); } while (target != null && target is not T);
        return target as T;
    }

    public static IEnumerable<DependencyObject> GetAllVisualChildren(this DependencyObject? obj)
    {
        if (obj is null) yield break;
        var toProcess = new Stack<DependencyObject>(obj.GetVisualChildren());
        do
        {
            obj = toProcess.Pop();
            yield return obj;

            foreach (var dependencyObject in obj.GetVisualChildren())
            {
                toProcess.Push(dependencyObject);
            }

        } while (toProcess.Count > 0);
    }

    public static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject? obj)
    {
        if (obj is null) yield break;
        for (int i = 0, count = VisualTreeHelper.GetChildrenCount(obj); i < count; i++)
            yield return VisualTreeHelper.GetChild(obj, i);
    }

    public static IEnumerable<DependencyObject> GetAllLogicalChildren(this DependencyObject? obj)
    {
        if (obj is null) yield break;
        var toProcess = new Stack<DependencyObject>(obj.GetLogicalChildren());
        do
        {
            obj = toProcess.Pop();
            yield return obj;

            foreach (var dependencyObject in obj.GetLogicalChildren())
            {
                toProcess.Push(dependencyObject);
            }

        } while (toProcess.Count > 0);
    }

    public static IEnumerable<DependencyObject> GetLogicalChildren(this DependencyObject? obj) => obj is null
        ? Enumerable.Empty<DependencyObject>()
        : LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>();

    public static IEnumerable<DependencyObject> GetLogicalParents(this DependencyObject? obj)
    {
        if (obj is null) yield break;

        var current = obj;
        do
        {
            current = LogicalTreeHelper.GetParent(current);
            if (current != null)
                yield return current;
        }
        while (current != null);
    }

    public static IEnumerable<DependencyObject> GetVisualParents(this DependencyObject? obj)
    {
        if (obj is null) yield break;

        var current = obj;
        do
        {
            current = VisualTreeHelper.GetParent(current);
            if (current != null)
                yield return current;
        }
        while (current != null);
    }

    public static void ProcessVisualTreeNodes<T>(this DependencyObject root, Action<T> processAction) where T : DependencyObject
    {
        var visualTree = new Stack<DependencyObject>(new[] { root });

        while (visualTree.Count != 0)
        {
            var current = visualTree.Pop();

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
            {
                var child = VisualTreeHelper.GetChild(current, i);

                if (child is T childT)
                {
                    processAction(childT);
                }

                visualTree.Push(child);
            }
        }
    }
}