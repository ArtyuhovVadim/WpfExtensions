using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker.Parts;

[TemplatePart(Name = ThumbCanvasName, Type = typeof(Canvas))]
[TemplatePart(Name = ThumbName, Type = typeof(UIElement))]
public class ColorSlider : Control
{
    private const string ThumbCanvasName = "PART_ThumbCanvasHolder";
    private const string ThumbName = "PART_Thumb";

    private Canvas _thumbCanvas;
    private UIElement _thumb;

    static ColorSlider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSlider), new FrameworkPropertyMetadata(typeof(ColorSlider)));
    }

    #region Maximum

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(ColorSlider), new PropertyMetadata(1d));

    #endregion

    #region Minimum

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(ColorSlider), new PropertyMetadata(0d));

    #endregion

    #region ThumbBrush

    public Brush ThumbBrush
    {
        get => (Brush)GetValue(ThumbBrushProperty);
        set => SetValue(ThumbBrushProperty, value);
    }

    public static readonly DependencyProperty ThumbBrushProperty =
        DependencyProperty.Register(nameof(ThumbBrush), typeof(Brush), typeof(ColorSlider), new PropertyMetadata(default(Brush)));

    #endregion

    #region ThumbWidth

    public double ThumbWidth
    {
        get => (double)GetValue(ThumbWidthProperty);
        set => SetValue(ThumbWidthProperty, value);
    }

    public static readonly DependencyProperty ThumbWidthProperty =
        DependencyProperty.Register(nameof(ThumbWidth), typeof(double), typeof(ColorSlider), new PropertyMetadata(default(double)));

    #endregion

    #region ThumbHeight

    public double ThumbHeight
    {
        get => (double)GetValue(ThumbHeightProperty);
        set => SetValue(ThumbHeightProperty, value);
    }

    public static readonly DependencyProperty ThumbHeightProperty =
        DependencyProperty.Register(nameof(ThumbHeight), typeof(double), typeof(ColorSlider), new PropertyMetadata(default(double)));

    #endregion

    #region Value

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double), typeof(ColorSlider), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ColorSlider slider) return;

        if (slider._thumbCanvas is null || slider._thumb is null) return;

        var v = Map(slider.Value, slider.Minimum, slider.Maximum, 0, slider._thumbCanvas.ActualWidth);

        var currentPos = Canvas.GetLeft(slider._thumb) + slider._thumb.RenderSize.Width / 2;

        if (Math.Abs(v - currentPos) < 0.001)
            return;

        slider.UpdateThumbPosition(v);
    }

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _thumbCanvas = GetTemplateChild(ThumbCanvasName) as Canvas ?? throw new ElementNotAvailableException($"Part element is not available in {GetType()} template!");
        _thumb = GetTemplateChild(ThumbName) as UIElement ?? throw new ElementNotAvailableException($"Part element is not available in {GetType()} template!");

        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var v = Map(Value, Minimum, Maximum, 0, _thumbCanvas.ActualWidth);

        UpdateThumbPosition(v);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        CaptureMouse();

        UpdateThumbPosition(e.GetPosition(_thumbCanvas).X);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            var pos = e.GetPosition(_thumbCanvas).X;

            UpdateThumbPosition(pos);
        }
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        Mouse.Capture(null);

        base.OnMouseLeftButtonUp(e);
    }

    private void UpdateThumbPosition(double pos)
    {
        pos = Math.Clamp(pos, 0, _thumbCanvas.ActualWidth) - _thumb.RenderSize.Width / 2;

        Canvas.SetLeft(_thumb, pos);

        Value = Map(pos, -_thumb.RenderSize.Width / 2, _thumbCanvas.ActualWidth - _thumb.RenderSize.Width / 2, Minimum, Maximum);
    }

    private static double Map(double x, double inMin, double inMax, double outMin, double outMax) =>
        (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
}