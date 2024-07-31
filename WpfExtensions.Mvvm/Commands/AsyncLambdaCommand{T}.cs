using WpfExtensions.Mvvm.Commands.Base;

namespace WpfExtensions.Mvvm.Commands;

public class AsyncLambdaCommand<T> : BaseAsyncCommand<T>
{
    private readonly Func<T?, CancellationToken, Task> _executeAction;
    private readonly Func<T?, bool>? _canExecute;

    public AsyncLambdaCommand(Func<T?, CancellationToken, Task> executeAction)
    {
        ArgumentNullException.ThrowIfNull(executeAction);
        _executeAction = executeAction;
    }

    public AsyncLambdaCommand(Func<T?, CancellationToken, Task> executeAction, Func<T?, bool> canExecute) : this(executeAction)
    {
        ArgumentNullException.ThrowIfNull(canExecute);
        _canExecute = canExecute;
    }

    public AsyncLambdaCommand(Func<T?, Task> executeAction)
    {
        ArgumentNullException.ThrowIfNull(executeAction);
        _executeAction = (parameter, _) => executeAction(parameter);
    }

    public AsyncLambdaCommand(Func<T?, Task> executeAction, Func<T?, bool> canExecute) : this(executeAction)
    {
        ArgumentNullException.ThrowIfNull(canExecute);
        _canExecute = canExecute;
    }

    protected override Task OnExecuteAsync(T? parameter, CancellationToken token) => _executeAction.Invoke(parameter, token);

    protected override bool OnCanExecute(T? parameter) => _canExecute?.Invoke(parameter) ?? base.OnCanExecute(parameter);
}