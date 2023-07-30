using System.Windows;
using System.Windows.Input;

namespace DemoApp;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            

            Application.Current.Resources.MergedDictionaries.RemoveAt(0);
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/WpfExtensions.Controls;component/Styles/Brushes/LightBrushes.xaml", UriKind.Relative) });
            var a = Application.Current.Resources.MergedDictionaries;


        }
    }
}