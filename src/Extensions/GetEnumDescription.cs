using System.ComponentModel;

namespace codecrafters.shell.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDescription<TEnum>(this TEnum item) => item.GetType()
        .GetField(item.ToString() ?? string.Empty)
        ?.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>()
        .FirstOrDefault()?.Description ?? string.Empty;
}