using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SWC.Startup))]
namespace SWC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
