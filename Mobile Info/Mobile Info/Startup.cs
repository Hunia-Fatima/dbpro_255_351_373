using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mobile_Info.Startup))]
namespace Mobile_Info
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
