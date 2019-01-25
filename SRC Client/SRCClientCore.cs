using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;

namespace SRC_Client
{
    /// <summary>
    /// Send and receive user's messages
    /// </summary>
    public class UserMessage
    {
        public string from { get; set; }
        public string message { get; set; }

        public UserMessage(string from, string message)
        {
            this.from = from;
            this.message = message;
        }
    }

    /// <summary>
    /// SRC messages format
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Normal message format
        /// </summary>
        /// <param name="message">The input message</param>
        public static void Normal(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message);
        }

        /// <summary>
        /// Message format from SRC
        /// </summary>
        /// <param name="message">The input message</param>
        public static void FromSRC(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[SRC] > ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
        }

        /// <summary>
        /// Message format from SRC
        /// </summary>
        /// <param name="message">The input message</param>
        /// <param name="color">The input color</param>
        public static void FromSRC(string message, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[SRC] > ");

            Console.ForegroundColor = color;
            Console.Write(message);
        }

        /// <summary>
        /// Message format from Server
        /// </summary>
        /// <param name="message">The input message</param>
        public static void FromServer(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Server: ");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Message format from Server
        /// </summary>
        /// <param name="message">The input message</param>
        /// <param name="color">The input color</param>
        public static void FromServer(string message, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Server: ");

            Console.ForegroundColor = color;
            Console.Write(message);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Message format from client
        /// </summary>
        /// <param name="from">The user that send message</param>
        /// <param name="message">The input message</param>
        public static void FromClient(string from, string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(from + ": ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
        }
    }

    /// <summary>
    /// SignalR Chat client class
    /// </summary>
    class SRCClient
    {
        static string uri = "http://localhost:8989";
        static HubConnection connection;
        static IHubProxy proxy;

        /// <summary>
        /// Run SignalRClient and initialize
        /// </summary>
        public static bool RunSignalRClient()
        {
            try
            {
                connection = new HubConnection(uri);
                proxy = connection.CreateHubProxy("srchub");

                connection.Start().Wait();

                proxy.On<string>("join", userJoin =>
                {
                    Message.FromServer(userJoin, ConsoleColor.Green);

                    Console.Write("\n\n");
                    Message.FromSRC("");

                });

                proxy.On<UserMessage>("message", userMessage =>
                {
                    Message.FromClient(userMessage.from, userMessage.message);

                    Console.Write("\n\n");
                    Message.FromSRC("");
                });

                proxy.On<string>("servermessage", serverMessage =>
                {
                    Message.FromServer(serverMessage, ConsoleColor.DarkYellow);

                    Console.Write("\n\n");
                    Message.FromSRC("");
                });

                proxy.On<string>("ondisconnected", userDisconnect =>
                {
                    Message.FromServer(userDisconnect, ConsoleColor.Red);

                    Console.Write("\n\n");
                    Message.FromSRC("");
                });

                proxy.On<List<string>>("allonlines", userOnline =>
                {
                    Message.FromServer("All online users:\n");

                    foreach (string online in userOnline)
                    {
                        Message.Normal(" - " + online + "\n");
                    }

                    Console.Write("\n");
                    Message.FromSRC("");
                });

                return true;

            }
            catch (Exception)
            {
                Message.FromSRC("Can't connect to the server\n\n", ConsoleColor.Red);
                
                return false;
            }
        }

        /// <summary>
        /// Join user to the sever
        /// </summary>
        /// <param name="name">The input name of user</param>
        public static void JoinUser(string name)
        {
            proxy.Invoke("Join", new object[] { name });
        }

        /// <summary>
        /// Send message to the server
        /// </summary>
        /// <param name="message">The input message</param>
        public static void SendMessage(UserMessage message)
        {
            proxy.Invoke<UserMessage>("Message", new object[] { message.from, message.message });
            Console.Write("\n");
        }

        /// <summary>
        /// Get all online users
        /// </summary>
        public static void AllOnlineUsers()
        {
            proxy.Invoke("AllOnlines");
        }

        /// <summary>
        /// Leave the server
        /// </summary>
        public static void Leave()
        {
            connection.Dispose();
        }

        /// <summary>
        /// Disconnect the user from server when he close the window
        /// </summary>
        public static void Close()
        {
            proxy.Invoke("OnDisconnected");
        }

        /// <summary>
        /// Clear the screen and show header
        /// </summary>
        public static void ClearScreen()
        {
            Console.Clear();

            Message.Normal("SignalR chat (SRC) application - Version 1.0\n");
            Message.Normal("SRC  is licensed under the MIT License. Copyright (C) 2019 Ehsan Mohammadi\n\n");

            Message.Normal("Welcome to SRC!\n");
            Message.Normal("SRC is a place to anonymous chat with other online users.\n");
            Message.Normal("For more information type \"/help\"\n\n");
        }

        /// <summary>
        /// Show help menu
        /// </summary>
        public static void ShowHelp()
        {
            Message.Normal("- /join\t\t\tJoin to the server.\n");
            Message.Normal("- /set\t\t\tTake a name for you.\n");
            Message.Normal("- /onlines\t\tGet all online users.\n");
            Message.Normal("- /leave\t\tLeave the server.\n");
            Message.Normal("- /clear\t\tClear the screen.\n");
            Message.Normal("- /quit\t\t\tQuit from SRC.\n\n");
        }
    }
}
