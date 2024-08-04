using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public abstract class BaseCommand<T> : BindableBase, IRelayCommand<T>
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    bool ICommand.CanExecute(object? parameter)
    {
        // Special case a null value for a value type argument type.
        // This ensures that no exceptions are thrown during initialization.
        if (parameter is null && default(T) is not null)
            return false;

        if (!TryGetCommandArgument(parameter, out var parameterT))
            throw new ArgumentException($"Parameter must be {typeof(T)} type.");

        return OnCanExecute(parameterT);
    }

    void ICommand.Execute(object? parameter)
    {
        if (!TryGetCommandArgument(parameter, out var parameterT))
            throw new ArgumentException($"Parameter must be {typeof(T)} type.");

        OnExecute(parameterT);
    }

    public bool CanExecute(T? parameter) => OnCanExecute(parameter);

    public void Execute(T? parameter) => OnExecute(parameter);

    protected abstract void OnExecute(T? parameter);

    protected virtual bool OnCanExecute(T? parameter) => true;

    private static bool TryGetCommandArgument(object? parameter, out T? result)
    {
        // If the argument is null and the default value of T is also null, then the
        // argument is valid. T might be a reference type or a nullable value type.
        if (parameter is null && default(T) is null)
        {
            result = default;

            return true;
        }

        // Check if the argument is a T value, so either an instance of a type or a derived
        // type of T is a reference type, an interface implementation if T is an interface,
        // or a boxed value type in case T was a value type.
        if (parameter is T argument)
        {
            result = argument;

            return true;
        }

        result = default;

        return false;
    }
}