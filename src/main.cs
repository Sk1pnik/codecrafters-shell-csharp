using System.Net;
using System.Net.Sockets;


// Wait for user input

while (true)
{
    Console.Write("$ ");
    var command = Console.ReadLine();
    if (command.Contains("exit"))
    {
        break;
    }

    Console.WriteLine($"{command}: command not found");
}