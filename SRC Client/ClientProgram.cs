using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRC_Client
{
    class ClientProgram
    {
        static void Main(string[] args)
        {
            // Initialize
            bool isJoin = false;
            bool hasName = false;
            string name = null;

            SRCClient.ClearScreen();

            while (true)
            {
                Message.FromSRC("");
                string instruction = Console.ReadLine();

                if (!string.IsNullOrEmpty(instruction.Replace(" ", "")))
                {
                    if (instruction.Substring(0, 1) == "/") // It's instruction
                    {
                        instruction = instruction.ToLower();

                        if (instruction == "/help")
                        {
                            SRCClient.ShowHelp();
                        }
                        else if (instruction == "/clear")
                        {
                            SRCClient.ClearScreen();
                        }
                        else if (instruction == "/quit")
                        {
                            return;
                        }
                        else if (instruction == "/set")
                        {
                            if (!isJoin)
                            {
                                Message.FromSRC("Pick a name: ");
                                name = Console.ReadLine().Replace(" ", "");

                                if (!string.IsNullOrEmpty(name))
                                {
                                    hasName = true;
                                    Message.FromSRC("Name successfully picked.\n\n", ConsoleColor.Green);
                                }
                                else
                                {
                                    hasName = false;
                                    Message.FromSRC("No name picked.\n\n", ConsoleColor.Red);
                                }
                            }
                            else
                            {
                                Message.FromSRC("You don't set a new name when you join to the server.\n\n", ConsoleColor.Red);
                            }
                        }
                        else if (instruction == "/leave")
                        {
                            if (isJoin)
                            {
                                SRCClient.Leave();
                                isJoin = false;

                                Console.Write("\n");
                                Message.FromSRC("");
                                Message.FromServer(name + " is disconnected!\n\n", ConsoleColor.Red);
                            }
                            else
                            {
                                Message.FromSRC("You have already disconnected.\n\n", ConsoleColor.Red);
                            }
                        }
                        else if (instruction == "/join")
                        {
                            if (!isJoin)
                            {
                                if (hasName)
                                {
                                    Message.FromSRC("Please wait... (It might take a few seconds)\n\n");

                                    // Run SignalR and join to the server
                                    if(SRCClient.RunSignalRClient())
                                    {
                                        SRCClient.JoinUser(name);
                                        SRCClient.Close();
                                        isJoin = true;
                                    }
                                }
                                else
                                {
                                    Message.FromSRC("At first, pick a name. type \"/set\".\n\n", ConsoleColor.Red);
                                }
                            }
                            else
                            {
                                Message.FromSRC("You have already join.\n\n", ConsoleColor.Red);
                            }
                        }
                        else if (instruction == "/onlines")
                        {
                            if (isJoin && hasName)
                            {
                                SRCClient.AllOnlineUsers();
                            }
                            else
                            {
                                Message.FromSRC("You must join to the server to see online users.\n\n", ConsoleColor.Red);
                            }
                        }
                        else
                        {
                            Message.FromSRC("It's not recognized as an instruction.\n\n", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        if (hasName && isJoin) // It's message
                        {
                            UserMessage userMessage = new UserMessage(name, instruction);
                            SRCClient.SendMessage(userMessage);
                        }
                        else
                        {
                            Message.FromSRC("It's not recognized as an instruction.\n\n", ConsoleColor.Red);
                        }
                    }
                }
            }
        }
    }
}
