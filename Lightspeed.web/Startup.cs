using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lightspeed.web.Startup))]
namespace Lightspeed.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
