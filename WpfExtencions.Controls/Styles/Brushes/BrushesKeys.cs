using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfExtensions.Controls.Styles.Brushes;

public static class BrushesKeys
{
    public static readonly ComponentResourceKey ColorPickerControlBackground = CreateInstance();

    private static ComponentResourceKey CreateInstance([CallerMemberName] string fieldName = null!)
    {
        return new ComponentResourceKey(typeof(BrushesKeys), fieldName!);
    }
}