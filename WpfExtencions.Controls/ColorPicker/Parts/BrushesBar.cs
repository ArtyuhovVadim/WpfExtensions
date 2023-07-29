using System.Collections.Generic;
using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class BrushesBar
{
    public Brush GrayscaleBrush { get; set; }

    public Brush MainBrush { get; set; }

    public List<Brush> Brushes { get; set; } = new();
}