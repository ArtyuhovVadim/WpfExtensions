using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfExtensions.Controls;

[TemplatePart(Name = ApplyButtonName, Type = typeof(Button))]
[TemplatePart(Name = CancelButtonName, Type = typeof(Button))]
public class ColorPicker : Control
{
    private const string ApplyButtonName = "PART_ApplyButton";
    private const string CancelButtonName = "PART_CancelButton";

    private ICommand _selectedColorCommand;

    private Button _applyButton;
    private Button _cancelButton;

    static ColorPicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
    }

    #region SelectedColor

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    public static readonly DependencyProperty SelectedColorProperty =
        DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Black));

    #endregion

    #region CurrentColor

    public Color CurrentColor
    {
        get => (Color)GetValue(CurrentColorProperty);
        set => SetValue(CurrentColorProperty, value);
    }

    public static readonly DependencyProperty CurrentColorProperty =
        DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Black, OnCurrentColorChanged));

    private static void OnCurrentColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ColorPicker picker) return;

        var color = (Color)e.NewValue;

        if (!picker.IsTransparencySupported && color.A != 255)
            color.A = 255;

        picker.RComponent = color.R;
        picker.GComponent = color.G;
        picker.BComponent = color.B;

        picker.HexColor = ColorToString(color, picker.IsTransparencySupported);
    }

    #endregion

    #region IsTransparencySupported

    public bool IsTransparencySupported
    {
        get => (bool)GetValue(IsTransparencySupportedProperty);
        set => SetValue(IsTransparencySupportedProperty, value);
    }

    public static readonly DependencyProperty IsTransparencySupportedProperty =
        DependencyProperty.Register(nameof(IsTransparencySupported), typeof(bool), typeof(ColorPicker), new PropertyMetadata(false, OnIsTransparencySupportedChanged));

    private static void OnIsTransparencySupportedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ColorPicker picker) return;

        picker.InvalidateProperty(HexColorProperty);
    }

    #endregion

    #region IsDefaultPaletteVisible

    public bool IsDefaultPaletteVisible
    {
        get => (bool)GetValue(IsDefaultPaletteVisibleProperty);
        set => SetValue(IsDefaultPaletteVisibleProperty, value);
    }

    public static readonly DependencyProperty IsDefaultPaletteVisibleProperty =
        DependencyProperty.Register(nameof(IsDefaultPaletteVisible), typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));

    #endregion
    
    #region IsRecentPaletteVisible

    public bool IsRecentPaletteVisible
    {
        get => (bool)GetValue(IsRecentPaletteVisibleProperty);
        set => SetValue(IsRecentPaletteVisibleProperty, value);
    }

    public static readonly DependencyProperty IsRecentPaletteVisibleProperty =
        DependencyProperty.Register(nameof(IsRecentPaletteVisible), typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));

    #endregion

    #region HexColor

    public string HexColor
    {
        get => (string)GetValue(HexColorProperty);
        set => SetValue(HexColorProperty, value);
    }

    public static readonly DependencyProperty HexColorProperty =
        DependencyProperty.Register(nameof(HexColor), typeof(string), typeof(ColorPicker), new PropertyMetadata(string.Empty, OnHexColorChanged, OnCoerceHexColor));

    private static object OnCoerceHexColor(DependencyObject d, object basevalue)
    {
        if (d is not ColorPicker picker) return basevalue;

        return ColorToString(StringToColor((string)basevalue, picker.IsTransparencySupported, picker.CurrentColor), picker.IsTransparencySupported);
    }

    private static void OnHexColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ColorPicker picker) return;

        picker.CurrentColor = StringToColor((string)e.NewValue, picker.IsTransparencySupported, Colors.Black);
    }

    #endregion

    #region RComponent

    public int RComponent
    {
        get => (int)GetValue(RComponentProperty);
        set => SetValue(RComponentProperty, value);
    }

    public static readonly DependencyProperty RComponentProperty =
        DependencyProperty.Register(nameof(RComponent), typeof(int), typeof(ColorPicker), new PropertyMetadata(default(int), OnColorComponentChanged, OnCoerceColorComponentValue));

    #endregion

    #region GComponent

    public int GComponent
    {
        get => (int)GetValue(GComponentProperty);
        set => SetValue(GComponentProperty, value);
    }

    public static readonly DependencyProperty GComponentProperty =
        DependencyProperty.Register(nameof(GComponent), typeof(int), typeof(ColorPicker), new PropertyMetadata(default(int), OnColorComponentChanged, OnCoerceColorComponentValue));

    #endregion

    #region BComponent

    public int BComponent
    {
        get => (int)GetValue(BComponentProperty);
        set => SetValue(BComponentProperty, value);
    }

    public static readonly DependencyProperty BComponentProperty =
        DependencyProperty.Register(nameof(BComponent), typeof(int), typeof(ColorPicker), new PropertyMetadata(default(int), OnColorComponentChanged, OnCoerceColorComponentValue));

    #endregion

    public ICommand SelectedColorCommand => _selectedColorCommand ??= new LambdaCommand(p =>
    {
        if (p is not Color color) return;

        OnColorSelected(color);
    });

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _applyButton = GetTemplateChild(ApplyButtonName) as Button ?? throw new ElementNotAvailableException($"{ApplyButtonName} is not available in template");
        _cancelButton = GetTemplateChild(CancelButtonName) as Button ?? throw new ElementNotAvailableException($"{CancelButtonName} is not available in template");

        CurrentColor = Colors.Red;
        CurrentColor = Colors.Black;
    }

    private void OnColorSelected(Color color)
    {
        CurrentColor = color;
        SelectedColor = CurrentColor;
    }

    private static void OnColorComponentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ColorPicker picker) return;

        picker.CurrentColor = Color.FromRgb((byte)picker.RComponent, (byte)picker.GComponent, (byte)picker.BComponent);
    }

    private static object OnCoerceColorComponentValue(DependencyObject d, object basevalue)
    {
        if (d is not ColorPicker) return basevalue;

        if (int.TryParse(basevalue.ToString(), out var result))
        {
            return Math.Min(Math.Max(0, result), 255);
        }

        return 0;
    }

    private static string ColorToString(Color color, bool isTransparencySupported)
    {
        if (!isTransparencySupported)
            return color.ToString().Remove(1, 2);

        return color.ToString();
    }

    private static Color StringToColor(string str, bool isTransparencySupported, Color fallbackColor)
    {
        var pattern = $$"""#[0-9A-Fa-f]{{{(isTransparencySupported ? 8 : 6)}}}$""";
        var matchResult = Regex.Match(str, pattern);

        if (!matchResult.Success)
            return fallbackColor;

        return (Color)ColorConverter.ConvertFromString(str)!;
    }
}