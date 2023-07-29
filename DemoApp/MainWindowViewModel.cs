using System.Windows.Media;
using WpfExtensions.Mvvm;

namespace DemoApp;

public class MainWindowViewModel : BindableBase
{
    #region Color

    private Color _color = Colors.Pink;

    public Color Color
    {
        get => _color;
        set => Set(ref _color, value);
    }

    #endregion
}