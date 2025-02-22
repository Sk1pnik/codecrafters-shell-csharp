using System.Net;
using System.Net.Sockets;
using codecrafters.shell.Enums;
using codecrafters.shell.Extensions;

namespace codecrafters.shell
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Wait for user input
            while (true)
            {
                Console.Write("$ ");
                var command = Console.ReadLine();
                if (command != null)
                {
                    var tokens = command.Split(' ');
                    var firstCommand = tokens[0];
                    if (firstCommand == Commands.Echo.GetEnumDescription())
                    {
                        if (command.Length > firstCommand.Length)
                        {
                            Console.WriteLine(command.Substring(firstCommand.Length).TrimStart());
                        }
                    }
                    else if (firstCommand == Commands.Exit.GetEnumDescription())
                    {
                        break;
                    }
                    else if (firstCommand == Commands.Type.GetEnumDescription())
                    {
                        var commands = EnumExtensions.GetEnumDescriptions(typeof(Commands));
                        var secondCommand = tokens[1];
                        if (commands.Contains(secondCommand))
                        {
                            var pathVariable = Environment.GetEnvironmentVariable("PATH");
                            var pathArr = pathVariable.Split(":");
                            var isFound = false;
                            foreach (var path in pathArr)
                            {
                                var fullPath = Path.Combine(path, secondCommand);
                                if (File.Exists(fullPath))
                                {
                                    isFound = true;
                                    Console.WriteLine($"{secondCommand} is {fullPath}");
                                }
                            }

                            if (!isFound)
                            {
                                Console.WriteLine($"{secondCommand}: not found");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{secondCommand}: not found");
                        }
                    }
                    else
                    {
                        CommandNotFound(command);
                    }
                }
                else
                {
                    CommandNotFound(command);
                }
            }
        }

        private static void CommandNotFound(string command)
        {
            Console.WriteLine($"{command}: command not found");
        }
    }
}