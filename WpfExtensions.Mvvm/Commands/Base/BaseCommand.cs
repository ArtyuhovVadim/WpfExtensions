using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public abstract class BaseCommand<T> : BaseCommand, ICommand<T>
{
    public abstract void Execute(T? parameter);

    public virtual bool CanExecute(T? parameter) => true;

    public override void Execute(object? parameter)
    {
        if (parameter is not T t)
            throw new ArgumentException($"Parameter must be {typeof(T)} type.");

        Execute(t);
    }

    public override bool CanExecute(object? parameter)
    {
        if (parameter is not T t)
            throw new ArgumentException($"Parameter must be {typeof(T)} type.");

        return CanExecute(t);
    }
}

public abstract class BaseCommand : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public abstract void Execute(object? parameter);

    public virtual bool CanExecute(object? parameter) => true;
}