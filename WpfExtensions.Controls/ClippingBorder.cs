﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfExtensions.Controls;

public class ClippingBorder : Border
{
    private readonly RectangleGeometry _clipRect = new();
    private object? _oldClip;

    public override UIElement? Child
    {
        get => base.Child;
        set
        {
            if (Child == value) return;
            Child?.SetValue(ClipProperty, _oldClip);
            _oldClip = value?.ReadLocalValue(ClipProperty);
            base.Child = value;
        }
    }

    protected override void OnRender(DrawingContext dc)
    {
        ApplyChildClip();
        base.OnRender(dc);
    }

    private void ApplyChildClip()
    {
        var child = Child;
        if (child is null)
            return;
        _clipRect.RadiusX = _clipRect.RadiusY = Math.Max(0.0, CornerRadius.TopLeft - BorderThickness.Left * 0.5);
        _clipRect.Rect = new Rect(Child!.RenderSize);
        child.Clip = _clipRect;
    }
}