using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace WpfExtensions.Controls;

public class PaletteBrush
{
    public static readonly List<PaletteBrush> Palettes = GetPaletteBrushes();

    public Brush GrayscaleBrush { get; set; }

    public Brush MainBrush { get; set; }

    public List<Brush> Brushes { get; set; }

    private static List<PaletteBrush> GetPaletteBrushes()
    {
        var list = new List<PaletteBrush>
        {
            new()
            {
                MainBrush = CreateBrushFromHex("#980000"),
                GrayscaleBrush = CreateBrushFromHex("#000000"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#e6b8af"),
                    CreateBrushFromHex("#dd7e6b"),
                    CreateBrushFromHex("#cc4125"),
                    CreateBrushFromHex("#a61c00"),
                    CreateBrushFromHex("#85200c"),
                    CreateBrushFromHex("#5b0f00")
                }
            },
            new()
            {
                MainBrush = CreateBrushFromHex("#ff0000"),
                GrayscaleBrush = CreateBrushFromHex("#434343"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#f4cccc"),
                    CreateBrushFromHex("#ea9999"),
                    CreateBrushFromHex("#e06666"),
                    CreateBrushFromHex("#cc0000"),
                    CreateBrushFromHex("#990000"),
                    CreateBrushFromHex("#660000")
                }
            },
            new()
            {
                MainBrush = CreateBrushFromHex("#ff9900"),
                GrayscaleBrush = CreateBrushFromHex("#666666"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#fce5cd"),
                    CreateBrushFromHex("#f9cb9c"),
                    CreateBrushFromHex("#f6b26b"),
                    CreateBrushFromHex("#e69138"),
                    CreateBrushFromHex("#b45f06"),
                    CreateBrushFromHex("#783f04")
                }
            },
            new()
            {
                MainBrush = CreateBrushFromHex("#ffff00"),
                GrayscaleBrush = CreateBrushFromHex("#999999"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#fff2cc"),
                    CreateBrushFromHex("#ffe599"),
                    CreateBrushFromHex("#ffd966"),
                    CreateBrushFromHex("#f1c232"),
                    CreateBrushFromHex("#bf9000"),
                    CreateBrushFromHex("#7f6000")
                }
            },
            new()
            {
                MainBrush = CreateBrushFromHex("#00ff00"),
                GrayscaleBrush = CreateBrushFromHex("#b7b7b7"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#d9ead3"),
                    CreateBrushFromHex("#b6d7a8"),
                    CreateBrushFromHex("#93c47d"),
                    CreateBrushFromHex("#6aa84f"),
                    CreateBrushFromHex("#38761d"),
                    CreateBrushFromHex("#274e13")
                }
            },
            new()
            {
                MainBrush = CreateBrushFromHex("#00ffff"),
                GrayscaleBrush = CreateBrushFromHex("#cccccc"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#d0e0e3"),
                    CreateBrushFromHex("#a2c4c9"),
                    CreateBrushFromHex("#76a5af"),
                    CreateBrushFromHex("#45818e"),
                    CreateBrushFromHex("#134f5c"),
                    CreateBrushFromHex("#0c343d")
                }
            },
            new()
            {
                MainBrush = CreateBrushFromHex("#4a86e8"),
                GrayscaleBrush = CreateBrushFromHex("#d9d9d9"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#c9daf8"),
                    CreateBrushFromHex("#a4c2f4"),
                    CreateBrushFromHex("#6d9eeb"),
                    CreateBrushFromHex("#3c78d8"),
                    CreateBrushFromHex("#1155cc"),
                    CreateBrushFromHex("#1c4587")
                }
            },
            new()
            {
                MainBrush = CreateBrushFromHex("#0000ff"),
                GrayscaleBrush = CreateBrushFromHex("#efefef"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#cfe2f3"),
                    CreateBrushFromHex("#9fc5e8"),
                    CreateBrushFromHex("#6fa8dc"),
                    CreateBrushFromHex("#3d85c6"),
                    CreateBrushFromHex("#0b5394"),
                    CreateBrushFromHex("#073763")
                }
            },
            new()
            {
                MainBrush = CreateBrushFromHex("#9900ff"),
                GrayscaleBrush = CreateBrushFromHex("#f3f3f3"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#d9d2e9"),
                    CreateBrushFromHex("#b4a7d6"),
                    CreateBrushFromHex("#8e7cc3"),
                    CreateBrushFromHex("#674ea7"),
                    CreateBrushFromHex("#351c75"),
                    CreateBrushFromHex("#20124d")
                }
            },
            new()
            {
                MainBrush = CreateBrushFromHex("#ff00ff"),
                GrayscaleBrush = CreateBrushFromHex("#ffffff"),
                Brushes = new List<Brush>
                {
                    CreateBrushFromHex("#ead1dc"),
                    CreateBrushFromHex("#d5a6bd"),
                    CreateBrushFromHex("#c27ba0"),
                    CreateBrushFromHex("#a64d79"),
                    CreateBrushFromHex("#741b47"),
                    CreateBrushFromHex("#4c1130")
                }
            }
        };

        foreach (var brush in list.SelectMany(item => item.Brushes))
        {
            brush.Freeze();
        }

        return list;
    }

    private static SolidColorBrush CreateBrushFromHex(string hex)
    {
        return (SolidColorBrush)new BrushConverter().ConvertFrom(hex);
    }
}