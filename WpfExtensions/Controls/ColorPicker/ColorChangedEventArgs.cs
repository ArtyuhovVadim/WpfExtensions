using System.Windows;
using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker;

public class ColorChangedEventArgs : RoutedEventArgs
{
    public Color OldColor { get; }

    public Color NewColor { get; }

    public ColorChangedEventArgs(RoutedEvent routedEvent, object source, Color newColor, Color oldColor) : base(routedEvent, source)
    {
        OldColor = oldColor;
        NewColor = newColor;
    }
}