using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(App.Server.Api.Startup))]

namespace App.Server.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
