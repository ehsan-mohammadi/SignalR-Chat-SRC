using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Owin;

namespace SRC_Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthConfig(app);
        }
    }
}
