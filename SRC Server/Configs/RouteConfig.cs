using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;

namespace SRC_Server.Configs
{
    class RouteConfig
    {
        public static void RegisterRoute(HttpConfiguration route)
        {
            route.Routes.MapHttpRoute(
                name: "SRCApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
