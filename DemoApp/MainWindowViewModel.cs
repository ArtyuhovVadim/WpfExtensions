using System.Windows;
using System.Windows.Input;
using WpfExtensions.Mvvm;
using WpfExtensions.Mvvm.Commands;

namespace DemoApp;

public class MainWindowViewModel : BindableBase
{
    #region Status

    private string _status = "Статус...";

    public string Status
    {
        get => _status;
        set => Set(ref _status, value);
    }

    #endregion

    #region TestCommand

    private ICommand? _testCommand;

    public ICommand TestCommand => _testCommand ??= new LambdaCommand(() =>
    {
        MessageBox.Show("Test command");
    });

    #endregion
}