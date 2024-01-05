using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfExtensions.Mvvm;

public abstract class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errors = new();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public bool HasErrors => _errors.Values.Any(x => x.Count > 0);

    IEnumerable INotifyDataErrorInfo.GetErrors(string? propertyName) => GetErrors(propertyName);

    public bool IsPropertyHasErrors([CallerMemberName] string? propertyName = null)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        if (_errors.TryGetValue(propertyName, out var errors))
        {
            return errors.Count != 0;
        }

        return false;
    }

    public IEnumerable<string> GetErrors(string? propertyName)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        return _errors.GetValueOrDefault(propertyName, Enumerable.Empty<string>().ToList());
    }

    protected void ClearErrors([CallerMemberName] string? propertyName = null)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        if (_errors.TryGetValue(propertyName, out var errors)) errors.Clear();
    }

    protected IEnumerable<string> GetAllValidatedProperties() => _errors.Keys;

    protected void ClearAllErrors()
    {
        foreach (var errors in _errors.Values)
        {
            errors.Clear();
        }
    }

    protected void AddError(string message, [CallerMemberName] string? propertyName = null)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        if (!_errors.TryGetValue(propertyName, out var cachedErrors))
        {
            cachedErrors = new List<string>();
            _errors[propertyName] = cachedErrors;
        }

        cachedErrors.Add(message);
    }

    protected virtual void OnErrorsChanged([CallerMemberName] string? propertyName = null)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        OnPropertyChanged(nameof(HasErrors));
    }
}