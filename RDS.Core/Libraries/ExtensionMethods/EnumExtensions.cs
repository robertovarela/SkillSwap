using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RDS.Core.Libraries.ExtensionMethods;

public static class EnumExtensions
{
    public static string GetEnumDisplayName(this Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var memberInfo = enumType.GetMember(enumValue.ToString()).FirstOrDefault();

        if (memberInfo == null) return enumValue.ToString();
        var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
        return displayAttribute?.Name ?? enumValue.ToString();

        /*return enumValue
                   .GetType()
                   .GetMember(enumValue.ToString())
                   .First()
                   .GetCustomAttribute<DisplayAttribute>()?
                   .GetName()
               ?? enumValue.ToString();*/
    }
}