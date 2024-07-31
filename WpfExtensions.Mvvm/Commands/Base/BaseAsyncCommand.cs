using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public abstract class BaseAsyncCommand : BindableBase, IAsyncRelayCommand
{
    private CancellationTokenSource _tokenSource = new();
    private Task? _executionTask;
    private bool _isCancellationRequested;
    private bool _isRunning;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public Task? ExecutionTask
    {
        get => _executionTask;
        private set => Set(ref _executionTask, value);
    }

    public bool IsCancellationRequested
    {
        get => _isCancellationRequested;
        private set
        {
            if (Set(ref _isCancellationRequested, value))
            {
                OnPropertyChanged(nameof(CanBeCanceled));
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }

    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            if (Set(ref _isRunning, value))
            {
                OnPropertyChanged(nameof(CanBeCanceled));
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }

    public bool CanBeCanceled => IsRunning && !IsCancellationRequested;

    bool ICommand.CanExecute(object? parameter) => CanExecuteInternal();

    async void ICommand.Execute(object? parameter) => await ExecuteAsyncInternal();

    public async Task ExecuteAsync() => await ExecuteAsyncInternal();

    public bool CanExecute() => CanExecuteInternal();

    public void Cancel()
    {
        if (!CanBeCanceled) return;
        _tokenSource.Cancel();
        IsCancellationRequested = true;
    }

    protected abstract Task OnExecuteAsync(CancellationToken token);

    protected virtual bool OnCanExecute() => true;

    private async Task ExecuteAsyncInternal()
    {
        try
        {
            _tokenSource = new CancellationTokenSource();
            IsCancellationRequested = false;

            IsRunning = true;

            ExecutionTask = OnExecuteAsync(_tokenSource.Token);
            await ExecutionTask;
        }
        finally
        {
            IsRunning = false;
        }
    }

    private bool CanExecuteInternal() => !IsRunning && !IsCancellationRequested && OnCanExecute();
}