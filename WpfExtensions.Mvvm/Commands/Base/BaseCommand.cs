using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public abstract class BaseCommand : BindableBase, IRelayCommand
{
    public event EventHandler? CanExecuteChanged;

    bool ICommand.CanExecute(object? parameter) => OnCanExecute();

    void ICommand.Execute(object? parameter) => OnExecute();

    public bool CanExecute() => OnCanExecute();

    public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public void Execute() => OnExecute();

    protected abstract void OnExecute();

    protected virtual bool OnCanExecute() => true;
}