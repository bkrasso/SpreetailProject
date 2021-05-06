using System;
using Microsoft.Extensions.DependencyInjection;

namespace SpreetailConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddSingleton<BusinessLibrary.Interfaces.IBusiness, BusinessLibrary.Business>().BuildServiceProvider();
            var business = serviceProvider.GetService<BusinessLibrary.Interfaces.IBusiness>();
            string command;

            do
            {
                DisplayCommands();
                command = Console.ReadLine();
                RunCommand(command, business);

            } while (!command.Equals("exit", StringComparison.OrdinalIgnoreCase));
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("List of Commands");
            Console.WriteLine("Items\t\t(Display all Keys and Members ex: ITEMS)");
            Console.WriteLine("Add\t\t(Will add a new key and member to that key ex: ADD MyKey MyValue)");
            Console.WriteLine("Keys\t\t(Will display all the keys ex: KEYS)");
            Console.WriteLine("KeyExists\t(Will return True if key exists ex: KEYEXISTS MyKey)");
            Console.WriteLine("Members\t\t(Will display all the members in a key ex: MEMBERS MyKey)");
            Console.WriteLine("AllMembers\t(Will display all members ex: ALLMEMBERS)");
            Console.WriteLine("ValueExists\t(Will return True if value exists in a specific key ex: VALUEEXISTS MyKey MyValue)");
            Console.WriteLine("AllMembers\t(Will display all members for all keys ex: ALLMEMBERS)");
            Console.WriteLine("Remove\t\t(Will remove a specific value from a specific key ex: REMOVE MyKey MyValue)");
            Console.WriteLine("RemoveAll\t(Will remove all values from a specific key ex: REMOVEALL MyKey)");
            Console.WriteLine("Clear\t\t(Will remove all keys and all values ex: CLEAR)");
            Console.WriteLine("Exit\t\t(Will exit the application ex: EXIT)");
            Console.Write("Please enter command:");
        }

        private static void RunCommand(string commandLine, BusinessLibrary.Interfaces.IBusiness business)
        {
            int count = 1;
            string[] commandPieces = commandLine.Split(' ');
            switch(commandPieces[0].ToLower())
            {
                case "items":
                    var itemsResult = business.Items();

                    if (itemsResult.StatusCode != 200)
                    {
                        Console.WriteLine($"Status Code:{itemsResult.StatusCode}, Result:{itemsResult.Message}");
                        break;
                    }

                    foreach (var member in itemsResult.Members)
                    {
                        Console.WriteLine(member);
                    }
                    break;
                case "add":
                    if (!IsValidCommandArguments(commandPieces, 3)) break;

                    var result = business.AddKey(commandPieces[1], commandPieces[2]);
                    Console.WriteLine($"Result:{result.Message}");
                    break;
                case "keys":
                    var allKeys = business.GetKeys();
                    foreach (var key in allKeys)
                    {
                        Console.WriteLine($"{count}) {key}");
                        count++;
                    }
                    break;
                case "keyexists":
                    if (!IsValidCommandArguments(commandPieces, 2)) break;

                    var keyExistsResult = business.KeyExists(commandPieces[1]);
                    Console.WriteLine($"Result:{keyExistsResult.Message}");
                    break;
                case "members":
                    if (!IsValidCommandArguments(commandPieces, 2)) break;
                    var membersResult = business.GetMembers(commandPieces[1]);

                    if (membersResult.StatusCode != 200)
                    {
                        Console.WriteLine($"Status Code:{membersResult.StatusCode}, Result:{membersResult.Message}");
                        break;
                    }

                    foreach(var member in membersResult.Members)
                    {
                        Console.WriteLine($"{count}) {member}");
                        count++;
                    }
                    break;
                case "allmembers":
                    var allMembersResult = business.AllMembers();
                    if (allMembersResult.StatusCode != 200)
                    {
                        Console.WriteLine($"Status Code:{allMembersResult.StatusCode}, Result:{allMembersResult.Message}");
                        break;
                    }

                    foreach (var member in allMembersResult.Members)
                    {
                        Console.WriteLine($"{count}) {member}");
                        count++;
                    }
                    break;
                case "valueexists":
                    if (!IsValidCommandArguments(commandPieces, 3)) break;

                    var valueExistsResult = business.ValueExists(commandPieces[1], commandPieces[2]);
                    Console.WriteLine($"Result:{valueExistsResult.Message}");
                    break;
                case "remove":
                    if (!IsValidCommandArguments(commandPieces, 3)) break;
                    var removeResult = business.Remove(commandPieces[1], commandPieces[2]);
                    Console.WriteLine($"Result:{removeResult.Message}");
                    break;
                case "removeall":
                    if (!IsValidCommandArguments(commandPieces, 2)) break;
                    var removeAllResult = business.Remove(commandPieces[1]);
                    Console.WriteLine($"Result:{removeAllResult.Message}");
                    break;
                case "clear":
                    var clearResult = business.Clear();
                    Console.WriteLine($"Result:{clearResult.Message}");
                    break;
                case "exit":
                    Console.WriteLine("Goodbye");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    break;
            }
        }

        private static bool IsValidCommandArguments(string[] commandPieces, int numArguments)
        {
            if (commandPieces.Length != numArguments)
            {
                Console.WriteLine("Invalid Arguments for Command");
                return false;
            }

            return true;
        }
    }
}
