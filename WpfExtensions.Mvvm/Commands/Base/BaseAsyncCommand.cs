namespace WpfExtensions.Mvvm.Commands.Base;

public class BaseAsyncCommand : BaseCommand, IAsyncCommand
{
    protected override void OnExecute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public Task? ExecutionTask { get; }
    public bool CanBeCanceled { get; }
    public bool IsCancellationRequested { get; }
    public bool IsRunning { get; }
    public Task ExecuteAsync(object? parameter)
    {
        throw new NotImplementedException();
    }

    public void Cancel()
    {
        throw new NotImplementedException();
    }
}