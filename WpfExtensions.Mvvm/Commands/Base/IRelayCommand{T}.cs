using System.ComponentModel;
using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public interface IRelayCommand<in T> : ICommand, INotifyPropertyChanged
{
    void Execute(T? parameter);

    void NotifyCanExecuteChanged();

    bool CanExecute(T? parameter);
}