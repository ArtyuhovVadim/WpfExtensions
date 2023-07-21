using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public interface ICommand<in T> : ICommand
{
    void Execute(T? parameter);

    bool CanExecute(T? parameter);
}