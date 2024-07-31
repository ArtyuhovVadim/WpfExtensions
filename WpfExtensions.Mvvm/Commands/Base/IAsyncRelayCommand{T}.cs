using System.ComponentModel;
using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public interface IAsyncRelayCommand<in T> : ICommand, INotifyPropertyChanged
{
    Task? ExecutionTask { get; }

    bool IsCancellationRequested { get; }

    bool IsRunning { get; }

    bool CanBeCanceled { get; }

    Task ExecuteAsync(T? parameter);

    bool CanExecute(T? parameter);

    void Cancel();
}