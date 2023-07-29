using System.ComponentModel;
using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public interface IRelayCommand : ICommand, INotifyPropertyChanged
{
    bool IsEnabled { get; set; }
}

public interface IRelayCommand<in T> : IRelayCommand
{
    void Execute(T? parameter);

    bool CanExecute(T? parameter);
}