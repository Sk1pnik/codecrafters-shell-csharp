using System.ComponentModel;

namespace codecrafters.shell.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDescription<TEnum>(this TEnum item) => item.GetType()
        .GetField(item.ToString() ?? string.Empty)
        ?.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>()
        .FirstOrDefault()?.Description ?? string.Empty;

    public static IEnumerable<string> GetEnumDescriptions(Type type)
    {
        var descs = new List<string>();
        var names = Enum.GetNames(type);
        foreach (var name in names)
        {
            var field = type.GetField(name);
            var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
            foreach (DescriptionAttribute fd in fds)
            {
                descs.Add(fd.Description);
            }
        }

        return descs;
    }
}