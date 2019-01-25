using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Owin.Hosting;
using SRC_Server.Hubs;

namespace SRC_Server
{
    class ServerProgram
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8989";
            SRCHub srcHub = new SRCHub();
            
            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("SignalR chat (SRC) server - Version 1.0");
                Console.WriteLine("SRC  is licensed under the MIT License. Copyright (C) 2019 Ehsan Mohammadi\n");

                Console.WriteLine("[SRC Server] > Server started at " + uri + " on " + DateTime.UtcNow + "\n");

                while(true)
                {
                    Console.Write("[SRC Server] > ");
                    
                    string serverMessage = Console.ReadLine();
                    srcHub.ServerMessage(serverMessage);
                }
            }
        }
    }
}
