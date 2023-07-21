using WpfExtensions.Mvvm.Commands.Base;

namespace WpfExtensions.Mvvm.Commands;

public class LambdaCommand<T> : BaseCommand<T>
{
    private readonly Action<T?> _executeAction;
    private readonly Predicate<T?>? _canExecute;

    public LambdaCommand(Action<T?> executeAction)
    {
        ArgumentNullException.ThrowIfNull(executeAction);
        _executeAction = executeAction;
    }

    public LambdaCommand(Action<T?> executeAction, Predicate<T?> canExecute) : this(executeAction)
    {
        ArgumentNullException.ThrowIfNull(canExecute);
        _canExecute = canExecute;
    }

    public override void Execute(T? parameter) => 
        _executeAction.Invoke(parameter);

    public override bool CanExecute(T? parameter) => 
        _canExecute?.Invoke(parameter) ?? base.CanExecute(parameter);
}

public class LambdaCommand : BaseCommand
{
    private readonly Action<object?> _executeAction;
    private readonly Predicate<object?>? _canExecute;

    public LambdaCommand(Action<object?> executeAction)
    {
        ArgumentNullException.ThrowIfNull(executeAction);
        _executeAction = executeAction;
    }

    public LambdaCommand(Action<object?> executeAction, Predicate<object?> canExecute) : this(executeAction)
    {
        ArgumentNullException.ThrowIfNull(canExecute);
        _canExecute = canExecute;
    }

    public LambdaCommand(Action executeAction)
    {
        ArgumentNullException.ThrowIfNull(executeAction);
        _executeAction = _ => executeAction();
    }

    public LambdaCommand(Action executeAction, Func<bool> canExecute) : this(executeAction)
    {
        ArgumentNullException.ThrowIfNull(canExecute);
        _canExecute = _ => canExecute();
    }

    public override void Execute(object? parameter) =>
        _executeAction.Invoke(parameter);

    public override bool CanExecute(object? parameter) =>
        _canExecute?.Invoke(parameter) ?? base.CanExecute(parameter);
}