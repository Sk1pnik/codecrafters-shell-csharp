using System.Diagnostics;
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
            var commands = new string[] { "echo", "exit", "type", "pwd", "cd" };
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
                            Environment.Exit(0);
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
                                var path = FindInPath(secondCommand);

                                if (path == null)
                                {
                                    Console.WriteLine($"{secondCommand}: not found");
                                }
                                else
                                {
                                    Console.WriteLine($"{secondCommand} is {path}");
                                }
                            }

                            break;
                        }
                        case "pwd":
                        {
                            Console.WriteLine(Directory.GetCurrentDirectory());
                            break;
                        }
                        case "cd":
                        {
                            var currentPath = Directory.GetCurrentDirectory();
                            var path = Path.GetFullPath(tokens[1]);
                            var joinedPath = Path.Join(currentPath, path);

                            if (Path.Exists(joinedPath))
                            {
                                Directory.SetCurrentDirectory(joinedPath);
                            }
                            else if (Path.IsPathFullyQualified(path) && Path.Exists(path))
                            {
                                Directory.SetCurrentDirectory(path);
                            }
                            else
                            {
                                Console.WriteLine($"{path}: No such file or directory");
                            }

                            break;
                        }
                        default:
                        {
                            var path = FindInPath(firstCommand);

                            if (path == null)
                            {
                                CommandNotFound(firstCommand);
                            }
                            else if (path is string pathDir)
                            {
                                var programArgs = command[firstCommand.Length..].TrimStart();
                                Process.Start(firstCommand, programArgs);
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

        private static string? FindInPath(string command)
        {
            return Environment.GetEnvironmentVariable("PATH")?
                .Split(":")
                .Select(p => Path.Combine(p, command))
                .FirstOrDefault(File.Exists);
        }
    }
}