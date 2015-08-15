using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pdia.Web.Startup))]
namespace Pdia.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
