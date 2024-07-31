using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public abstract class BaseAsyncCommand<T> : BindableBase, IAsyncRelayCommand<T>
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

    bool ICommand.CanExecute(object? parameter)
    {
        // Special case a null value for a value type argument type.
        // This ensures that no exceptions are thrown during initialization.
        if (parameter is null && default(T) is not null)
            return false;

        if (!TryGetCommandArgument(parameter, out var parameterT))
            throw new ArgumentException($"Parameter must be {typeof(T)} type.");

        return CanExecuteInternal(parameterT);
    }

    async void ICommand.Execute(object? parameter)
    {
        if (!TryGetCommandArgument(parameter, out var parameterT))
            throw new ArgumentException($"Parameter must be {typeof(T)} type.");

        await ExecuteAsyncInternal(parameterT);
    }

    public Task ExecuteAsync(T? parameter) => ExecuteAsyncInternal(parameter);

    public bool CanExecute(T? parameter) => CanExecuteInternal(parameter);

    public void Cancel()
    {
        if (!CanBeCanceled) return;
        _tokenSource.Cancel();
        IsCancellationRequested = true;
    }

    protected abstract Task OnExecuteAsync(T? parameter, CancellationToken token);

    protected virtual bool OnCanExecute(T? parameter) => true;

    private async Task ExecuteAsyncInternal(T? parameter)
    {
        try
        {
            _tokenSource = new CancellationTokenSource();
            IsCancellationRequested = false;

            IsRunning = true;

            ExecutionTask = OnExecuteAsync(parameter, _tokenSource.Token);
            await ExecutionTask;
        }
        finally
        {
            IsRunning = false;
        }
    }

    private bool CanExecuteInternal(T? parameter) => !IsRunning && !IsCancellationRequested && OnCanExecute(parameter);

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