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
            var commands = new string[] { "echo", "exit", "type" };
            while (true)
            {
                Console.Write("$ ");
                var command = Console.ReadLine();
                if (command != null)
                {
                    var tokens = command.Split(' ');
                    var firstCommand = tokens[0];
                    switch (firstCommand)
                    {
                        case "echo":
                        {
                            if (command.Length > firstCommand.Length)
                            {
                                Console.WriteLine(command.Substring(firstCommand.Length).TrimStart());
                            }

                            break;
                        }
                        case "exit":
                        {
                            break;
                        }
                        case "type":
                        {
                            var secondCommand = tokens[1];
                            if (commands.Contains(secondCommand))
                            {
                                Console.WriteLine($"{secondCommand} is a shell builtin");
                            }
                            else
                            {
                                var pathVariable = Environment.GetEnvironmentVariable("PATH");

                                if (pathVariable == null)
                                {
                                    CommandNotFound(secondCommand);
                                }

                                var isFound = false;
                                var pathArr = pathVariable.Split(":");
                                foreach (var path in pathArr)
                                {
                                    var fullPath = Path.Combine(path, secondCommand);
                                    if (File.Exists(fullPath))
                                    {
                                        isFound = true;
                                        Console.WriteLine($"{secondCommand} is {fullPath}");
                                        break;
                                    }
                                }

                                if (!isFound)
                                {
                                    Console.WriteLine($"{secondCommand}: not found");
                                }
                            }

                            break;
                        }
                        default:
                        {
                            //refactor if there is another occurence of path 
                            var pathVariable = Environment.GetEnvironmentVariable("PATH");

                            if (pathVariable == null)
                            {
                                CommandNotFound(firstCommand);
                            }

                            var isFound = false;
                            var pathArr = pathVariable.Split(":");
                            foreach (var path in pathArr)
                            {
                                var fullPath = Path.Combine(path, firstCommand);
                                if (File.Exists(fullPath))
                                {
                                    var rnd = new Random();
                                    isFound = true;
                                    Console.WriteLine(
                                        $"Program was passed {tokens.Length} args (including program name).");
                                    Console.WriteLine($"Arg #0 (program name): {tokens[0]}");
                                    for (int i = 1; i < tokens.Length; i++)
                                    {
                                        Console.WriteLine($"Arg #{i}: {tokens[i]}");
                                    }

                                    Console.WriteLine($"Program Signature: {rnd.Next()}");
                                    break;
                                }
                            }

                            if (!isFound)
                            {
                                CommandNotFound(firstCommand);
                            }

                            break;
                        }
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