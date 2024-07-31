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

    protected override void OnExecute(T? parameter) => _executeAction.Invoke(parameter);

    protected override bool OnCanExecute(T? parameter) => _canExecute?.Invoke(parameter) ?? base.OnCanExecute(parameter);
}