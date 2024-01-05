using System.Windows.Markup;

namespace WpfExtensions.MarkupExtensions;

public class EnumToArrayExtension : MarkupExtension
{
    private readonly Type _enumType;

    public EnumToArrayExtension(Type enumType) => _enumType = enumType;

    public override object ProvideValue(IServiceProvider serviceProvider) => Enum.GetValues(_enumType);
}