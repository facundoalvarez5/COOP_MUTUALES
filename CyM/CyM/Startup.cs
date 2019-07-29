using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CyM.Startup))]
namespace CyM
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
