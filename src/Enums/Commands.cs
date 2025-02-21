using System.ComponentModel;

namespace codecrafters.shell.Enums;

public enum Commands
{
    [Description("exit")] Exit = 0,
    [Description("echo")] Echo = 1,
    [Description("type")] Type = 2
}