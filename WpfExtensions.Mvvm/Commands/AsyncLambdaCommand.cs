using WpfExtensions.Mvvm.Commands.Base;

namespace WpfExtensions.Mvvm.Commands;

public class AsyncLambdaCommand : BaseAsyncCommand
{
    private readonly Func<CancellationToken, Task> _executeAction;
    private readonly Func<bool>? _canExecute;

    public AsyncLambdaCommand(Func<CancellationToken, Task> executeAction)
    {
        ArgumentNullException.ThrowIfNull(executeAction);
        _executeAction = executeAction;
    }

    public AsyncLambdaCommand(Func<CancellationToken, Task> executeAction, Func<bool> canExecute) : this(executeAction)
    {
        ArgumentNullException.ThrowIfNull(canExecute);
        _canExecute = canExecute;
    }

    public AsyncLambdaCommand(Func<Task> executeAction)
    {
        ArgumentNullException.ThrowIfNull(executeAction);
        _executeAction = _ => executeAction();
    }

    public AsyncLambdaCommand(Func<Task> executeAction, Func<bool> canExecute) : this(executeAction)
    {
        ArgumentNullException.ThrowIfNull(canExecute);
        _canExecute = canExecute;
    }

    protected override Task OnExecuteAsync(CancellationToken token) => _executeAction.Invoke(token);

    protected override bool OnCanExecute() => _canExecute?.Invoke() ?? base.OnCanExecute();
}