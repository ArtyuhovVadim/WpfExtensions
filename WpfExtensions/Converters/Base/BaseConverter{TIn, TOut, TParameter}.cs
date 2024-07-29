using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfExtensions.Converters.Base;

public abstract class BaseConverter<TIn, TOut> : BaseConverter<TIn, TOut, object>;

public abstract class BaseConverter<TIn, TOut, TParameter> : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public abstract TOut? Convert(TIn? value, TParameter? parameter, CultureInfo culture);

    public virtual TIn? ConvertBack(TOut? value, TParameter? parameter, CultureInfo culture) => throw new NotImplementedException();

    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!SafeCast(value, out TIn? valueT))
            throw new InvalidCastException($"Value must be {typeof(TIn)} type.");

        if (!SafeCast(parameter, out TParameter? parameterT))
            throw new InvalidCastException($"Parameter must be {typeof(TParameter)} type.");

        return Convert(valueT, parameterT, culture);
    }

    object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!SafeCast(value, out TOut? valueT))
            throw new InvalidCastException($"Value must be {typeof(TIn)} type.");

        if (!SafeCast(parameter, out TParameter? parameterT))
            throw new InvalidCastException($"Parameter must be {typeof(TParameter)} type.");

        return ConvertBack(valueT, parameterT, culture);
    }

    private static bool SafeCast<T>(object? value, out T? valueT)
    {
        if (value is null && default(T) is null)
        {
            valueT = default;
            return true;
        }

        if (value is T argument)
        {
            valueT = argument;
            return true;
        }

        valueT = default;
        return false;
    }
}