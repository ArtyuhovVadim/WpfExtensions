using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class BrushesBar
{
    public Brush GrayscaleBrush { get; set; } = System.Windows.Media.Brushes.Black;

    public Brush MainBrush { get; set; } = System.Windows.Media.Brushes.Black;

    public List<Brush> Brushes { get; set; } = new();
}