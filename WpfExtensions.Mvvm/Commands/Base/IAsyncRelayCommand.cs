using System.ComponentModel;
using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public interface IAsyncRelayCommand : ICommand, INotifyPropertyChanged
{
    Task? ExecutionTask { get; }

    bool IsCancellationRequested { get; }

    bool IsRunning { get; }

    bool CanBeCanceled { get; }

    Task ExecuteAsync();

    bool CanExecute();

    void NotifyCanExecuteChanged();

    void Cancel();
}