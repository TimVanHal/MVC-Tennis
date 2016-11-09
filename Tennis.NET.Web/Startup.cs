using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tennis.NET.Web.Startup))]
namespace Tennis.NET.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
