using System.Runtime.CompilerServices;
using FluentValidation.Results;

namespace WpfExtensions.Mvvm.FluentValidation;

public abstract class FluentValidatableBindableBase : ValidatableBindableBase
{
    protected abstract ValidationResult OnValidateProperty(string propertyName);

    protected abstract ValidationResult OnValidateAllProperties();

    protected bool ValidateProperty([CallerMemberName] string? propertyName = null)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        var result = OnValidateProperty(propertyName);

        ClearErrors(propertyName);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                AddError(error.ErrorMessage, propertyName);
            }
        }

        OnErrorsChanged(propertyName);

        return result.IsValid;
    }

    protected bool ValidateAllProperties()
    {
        var result = OnValidateAllProperties();

        ClearAllErrors();

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                AddError(error.ErrorMessage, error.PropertyName);
            }
        }

        foreach (var propertyName in GetAllValidatedProperties())
        {
            OnErrorsChanged(propertyName);
        }

        return result.IsValid;
    }

    protected void SetAndValidate<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Set(ref field, value, propertyName))
        {
            ValidateProperty(propertyName);
        }
    }

    protected void SetAndValidate<T>(ref T field, T value, IEqualityComparer<T> comparer, [CallerMemberName] string? propertyName = null)
    {
        if (Set(ref field, value, comparer, propertyName))
        {
            ValidateProperty(propertyName);
        }
    }
}