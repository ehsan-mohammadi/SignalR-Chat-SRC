using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Owin;
using System.Web.Http;
using SRC_Server.Configs;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;

namespace SRC_Server
{
    public partial class Startup
    {
        public void AuthConfig(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            RouteConfig.RegisterRoute(config);
            app.UseCors(CorsOptions.AllowAll);
            app.Map("/signalr", map =>
            {
                HubConfiguration hubConfig = new HubConfiguration();
                map.RunSignalR();
            });
        }
    }
}
