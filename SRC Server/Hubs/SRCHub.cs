using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;
using System.Threading;

namespace SRC_Server.Hubs
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
    /// SRC hub
    /// </summary>
    public class SRCHub : Hub
    {
        // Store user's ID and Name
        private static Dictionary<string, string> userDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Join user to the server
        /// </summary>
        /// <param name="name">The input name of user</param>
        public void Join(string name)
        {
            // Send message to all clients
            Clients.All.join(name + " is connected!");

            // Print message in the server
            Console.WriteLine(name + " is connected to the server!");
            Console.Write("[SRC Server] > ");

            // Add user's ID and Name to userDictionary
            userDictionary.Add(Context.ConnectionId, name);
        }

        /// <summary>
        /// User's messages
        /// </summary>
        /// <param name="name">The name of the user</param>
        /// <param name="message">The message of the user</param>
        public void Message(string name, string message)
        {
            UserMessage userMessage = new UserMessage(name, message);

            // Send message to other clients
            Clients.Others.message(userMessage);

            // Print message in the server
            Console.WriteLine(userMessage.from + ": " + userMessage.message);
            Console.Write("[SRC Server] > ");
        }

        /// <summary>
        /// Send message from server to all clients
        /// </summary>
        /// <param name="message">The server message</param>
        public void ServerMessage(string message)
        {
            // Send message to all clients
            var context = GlobalHost.ConnectionManager.GetHubContext<SRCHub>();
            context.Clients.All.servermessage(message);
        }

        /// <summary>
        /// Get all online users
        /// </summary>
        public void AllOnlines()
        {
            // Send all online users to the caller client
            List<string> onlines = userDictionary.Values.ToList<string>();
            Clients.Caller.allonlines(onlines);
        }

        /// <summary>
        /// When user connected to the server
        /// </summary>
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        /// <summary>
        /// When user disconnected from the server
        /// </summary>
        public override Task OnDisconnected(bool stopCalled)
        {
            // Remove the user from userDictionary
            string name = userDictionary[Context.ConnectionId];
            userDictionary.Remove(Context.ConnectionId);

            // Send disconnect message to all users
            Clients.All.ondisconnected(name + " is disconnected!");

            // Print message in the server
            Console.WriteLine(name + " is disconnected from the server!");
            Console.Write("[SRC Server] > ");

            return base.OnDisconnected(stopCalled);
        }
    }
}
