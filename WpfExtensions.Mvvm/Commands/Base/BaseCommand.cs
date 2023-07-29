using System.Windows;
using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public abstract class BaseCommand<T> : BaseCommand, IRelayCommand<T>
{
    void IRelayCommand<T>.Execute(T? parameter) => OnExecute(parameter);

    bool IRelayCommand<T>.CanExecute(T? parameter) => OnCanExecute(parameter);

    protected override void OnExecute(object? parameter)
    {
        if (parameter is not T t)
            throw new ArgumentException($"Parameter must be {typeof(T)} type.");

        Execute(t);
    }

    protected override bool OnCanExecute(object? parameter)
    {
        if (parameter is not T t)
            throw new ArgumentException($"Parameter must be {typeof(T)} type.");

        return CanExecute(t);
    }

    protected abstract void OnExecute(T? parameter);

    protected virtual bool OnCanExecute(T? parameter) => true;
}

public abstract class BaseCommand : BindableBase, IRelayCommand
{
    private bool _isEnabled = true;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool IsEnabled
    {
        get => _isEnabled;
        set => Set(ref _isEnabled, value);
    }

    public bool CanExecute(object? parameter) => IsEnabled && OnCanExecute(parameter);

    public void Execute(object? parameter)
    {
        if (CanExecute(parameter))
            OnExecute(parameter);
    }

    protected virtual bool OnCanExecute(object? parameter) => true;

    protected abstract void OnExecute(object? parameter);
}