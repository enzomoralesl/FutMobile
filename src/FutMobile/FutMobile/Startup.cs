using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FutMobile.Startup))]
namespace FutMobile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
