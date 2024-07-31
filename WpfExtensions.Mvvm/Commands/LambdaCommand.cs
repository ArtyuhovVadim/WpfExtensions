using WpfExtensions.Mvvm.Commands.Base;

namespace WpfExtensions.Mvvm.Commands;

public class LambdaCommand : BaseCommand
{
    private readonly Action _executeAction;
    private readonly Func<bool>? _canExecute;

    public LambdaCommand(Action executeAction)
    {
        ArgumentNullException.ThrowIfNull(executeAction);
        _executeAction = executeAction;
    }

    public LambdaCommand(Action executeAction, Func<bool> canExecute) : this(executeAction)
    {
        ArgumentNullException.ThrowIfNull(canExecute);
        _canExecute = canExecute;
    }

    protected override void OnExecute() => _executeAction.Invoke();

    protected override bool OnCanExecute() => _canExecute?.Invoke() ?? base.OnCanExecute();
}