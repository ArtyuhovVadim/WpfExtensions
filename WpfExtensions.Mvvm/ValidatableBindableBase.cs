using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfExtensions.Mvvm;

public abstract class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errors = new();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public bool HasErrors => _errors.Values.Any(x => x.Count > 0);

    IEnumerable INotifyDataErrorInfo.GetErrors(string? propertyName) => propertyName is null ? [] : GetErrors(propertyName);

    public bool IsPropertyHasErrors([CallerMemberName] string? propertyName = null)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        if (_errors.TryGetValue(propertyName, out var errors))
        {
            return errors.Count != 0;
        }

        return false;
    }

    public IEnumerable<string> GetErrors(string propertyName) =>
        _errors.GetValueOrDefault(propertyName, []);

    protected void ClearErrors([CallerMemberName] string? propertyName = null)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        if (_errors.TryGetValue(propertyName, out var errors))
        {
            errors.Clear();
            OnErrorsChanged(nameof(propertyName));
        }
    }

    protected IEnumerable<string> GetAllValidatedProperties() => _errors.Keys;

    protected void ClearAllErrors()
    {
        foreach (var (propName, errors) in _errors)
        {
            errors.Clear();
            OnErrorsChanged(propName);
        }
    }

    protected void AddError(string message, [CallerMemberName] string? propertyName = null)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        if (!_errors.TryGetValue(propertyName, out var cachedErrors))
        {
            cachedErrors = [];
            _errors[propertyName] = cachedErrors;
        }

        cachedErrors.Add(message);
        OnErrorsChanged(nameof(propertyName));
    }

    protected virtual void OnErrorsChanged([CallerMemberName] string? propertyName = null)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        OnPropertyChanged(nameof(HasErrors));
    }
}