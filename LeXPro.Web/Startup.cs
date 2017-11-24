using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeXPro.Startup))]
namespace LeXPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
