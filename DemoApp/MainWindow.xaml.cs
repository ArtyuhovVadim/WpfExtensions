using System.Windows.Media;
using WpfExtensions.Controls.ColorPicker;

namespace DemoApp;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ComplexColor(Colors.Green);
    }
}