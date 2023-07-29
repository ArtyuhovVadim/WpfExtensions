using System.Windows;
using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class Crosshair : FrameworkElement
{
    private readonly Pen _blackPen = new(Brushes.Black, 2);
    private readonly Pen _whitePen = new(Brushes.White, 2);
    private double _dpiFactor;
    private ScaleTransform _scaleTransform;

    public Crosshair()
    {
        IsHitTestVisible = false;
        SnapsToDevicePixels = true;
        UseLayoutRounding = true;
    }

    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        if (_scaleTransform is null)
        {
            var presentationSource = PresentationSource.FromVisual(this);

            if (presentationSource?.CompositionTarget is null) return;

            var matrix = presentationSource.CompositionTarget.TransformToDevice;
            _dpiFactor = 1 / matrix.M11;
            _scaleTransform = new ScaleTransform(_dpiFactor, _dpiFactor);
        }

        dc.PushTransform(_scaleTransform);

        dc.DrawEllipse(null, _blackPen, new Point(0, 0), 4, 4);
        dc.DrawEllipse(null, _whitePen, new Point(0, 0), 3, 3);

        dc.Pop();
    }
}