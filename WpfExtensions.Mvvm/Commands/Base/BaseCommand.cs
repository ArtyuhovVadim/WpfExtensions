using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public abstract class BaseCommand : BindableBase, IRelayCommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    bool ICommand.CanExecute(object? parameter) => OnCanExecute();

    void ICommand.Execute(object? parameter) => OnExecute();

    public bool CanExecute() => OnCanExecute();

    public void Execute() => OnExecute();

    protected abstract void OnExecute();

    protected virtual bool OnCanExecute() => true;
}