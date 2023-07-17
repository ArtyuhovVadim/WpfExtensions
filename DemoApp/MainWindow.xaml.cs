using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using WpfExtensions;

namespace DemoApp;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        TestButton.Click += OnTestButtonClick;
    }

    private void OnTestButtonClick(object sender, RoutedEventArgs e)
    {
        HsvColorPicker.Color = Colors.Aqua;
    }
}