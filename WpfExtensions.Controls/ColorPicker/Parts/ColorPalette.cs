using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfExtensions.Controls.ColorPicker.Parts;

public class ColorPalette : Control
{
    private static readonly BrushConverter BrushConverter = new();

    public static List<BrushesBar> DefaultBrushes => CreateDefaultBrushes();

    static ColorPalette()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPalette), new FrameworkPropertyMetadata(typeof(ColorPalette)));
    }

    public List<BrushesBar> Brushes { get; set; } = new();

    #region ColorSelectedCommand

    public ICommand ColorSelectedCommand
    {
        get => (ICommand)GetValue(ColorSelectedCommandProperty);
        set => SetValue(ColorSelectedCommandProperty, value);
    }

    public static readonly DependencyProperty ColorSelectedCommandProperty =
        DependencyProperty.Register(nameof(ColorSelectedCommand), typeof(ICommand), typeof(ColorPalette), new PropertyMetadata(default(ICommand)));

    #endregion

    private static List<BrushesBar> CreateDefaultBrushes() => new()
    {
        new BrushesBar
        {
            MainBrush = CreateBrush("#980000"),
            GrayscaleBrush = CreateBrush("#000000"),
            Brushes = new List<Brush>
            {
                CreateBrush("#e6b8af"),
                CreateBrush("#dd7e6b"),
                CreateBrush("#cc4125"),
                CreateBrush("#a61c00"),
                CreateBrush("#85200c"),
                CreateBrush("#5b0f00"),
            }
        },
        new BrushesBar
        {
            MainBrush = CreateBrush("#ff0000"),
            GrayscaleBrush = CreateBrush("#434343"),
            Brushes = new List<Brush>
            {
                CreateBrush("#f4cccc"),
                CreateBrush("#ea9999"),
                CreateBrush("#e06666"),
                CreateBrush("#cc0000"),
                CreateBrush("#990000"),
                CreateBrush("#660000")
            }
        },
        new BrushesBar
        {
            MainBrush = CreateBrush("#ff9900"),
            GrayscaleBrush = CreateBrush("#666666"),
            Brushes = new List<Brush>
            {
                CreateBrush("#fce5cd"),
                CreateBrush("#f9cb9c"),
                CreateBrush("#f6b26b"),
                CreateBrush("#e69138"),
                CreateBrush("#b45f06"),
                CreateBrush("#783f04")
            }
        },
        new BrushesBar
        {
            MainBrush = CreateBrush("#ffff00"),
            GrayscaleBrush = CreateBrush("#999999"),
            Brushes = new List<Brush>
            {
                CreateBrush("#fff2cc"),
                CreateBrush("#ffe599"),
                CreateBrush("#ffd966"),
                CreateBrush("#f1c232"),
                CreateBrush("#bf9000"),
                CreateBrush("#7f6000")
            }
        },
        new BrushesBar
        {
            MainBrush = CreateBrush("#00ff00"),
            GrayscaleBrush = CreateBrush("#b7b7b7"),
            Brushes = new List<Brush>
            {
                CreateBrush("#d9ead3"),
                CreateBrush("#b6d7a8"),
                CreateBrush("#93c47d"),
                CreateBrush("#6aa84f"),
                CreateBrush("#38761d"),
                CreateBrush("#274e13")
            }
        },
        new BrushesBar
        {
            MainBrush = CreateBrush("#00ffff"),
            GrayscaleBrush = CreateBrush("#cccccc"),
            Brushes = new List<Brush>
            {
                CreateBrush("#d0e0e3"),
                CreateBrush("#a2c4c9"),
                CreateBrush("#76a5af"),
                CreateBrush("#45818e"),
                CreateBrush("#134f5c"),
                CreateBrush("#0c343d")
            }
        },
        new BrushesBar
        {
            MainBrush = CreateBrush("#4a86e8"),
            GrayscaleBrush = CreateBrush("#d9d9d9"),
            Brushes = new List<Brush>
            {
                CreateBrush("#c9daf8"),
                CreateBrush("#a4c2f4"),
                CreateBrush("#6d9eeb"),
                CreateBrush("#3c78d8"),
                CreateBrush("#1155cc"),
                CreateBrush("#1c4587")
            }
        },
        new BrushesBar
        {
            MainBrush = CreateBrush("#0000ff"),
            GrayscaleBrush = CreateBrush("#efefef"),
            Brushes = new List<Brush>
            {
                CreateBrush("#cfe2f3"),
                CreateBrush("#9fc5e8"),
                CreateBrush("#6fa8dc"),
                CreateBrush("#3d85c6"),
                CreateBrush("#0b5394"),
                CreateBrush("#073763")
            }
        },
        new BrushesBar
        {
            MainBrush = CreateBrush("#9900ff"),
            GrayscaleBrush = CreateBrush("#f3f3f3"),
            Brushes = new List<Brush>
            {
                CreateBrush("#d9d2e9"),
                CreateBrush("#b4a7d6"),
                CreateBrush("#8e7cc3"),
                CreateBrush("#674ea7"),
                CreateBrush("#351c75"),
                CreateBrush("#20124d")
            }
        },
        new BrushesBar
        {
            MainBrush = CreateBrush("#ff00ff"),
            GrayscaleBrush = CreateBrush("#ffffff"),
            Brushes = new List<Brush>
            {
                CreateBrush("#ead1dc"),
                CreateBrush("#d5a6bd"),
                CreateBrush("#c27ba0"),
                CreateBrush("#a64d79"),
                CreateBrush("#741b47"),
                CreateBrush("#4c1130")
            }
        }
    };

    private static Brush CreateBrush(string brushHex)
    {
        var brush = (Brush)BrushConverter.ConvertFrom(brushHex)!;
        brush.Freeze();
        return brush;
    }
}