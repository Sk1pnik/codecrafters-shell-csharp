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
                            Console.WriteLine(command.Substring(firstCommand.Length));
                        }
                    }
                    else if (firstCommand == Commands.Exit.GetEnumDescription())
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{command}: command not found");
                    }
                }
                else
                {
                    Console.WriteLine($"{command}: command not found");
                }
            }
        }
    }
}