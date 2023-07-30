using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfExtensions.Controls.Styles.Brushes;

public static class BrushesKeys
{
    public static class ColorPickerControl
    {
        public static readonly ComponentResourceKey Background = CreateInstance();

        public static readonly ComponentResourceKey Foreground = CreateInstance();
    }

    private static ComponentResourceKey CreateInstance([CallerMemberName] string fieldName = null!)
    {
        return new ComponentResourceKey(typeof(BrushesKeys), fieldName);
    }
}