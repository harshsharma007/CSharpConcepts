using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UnderstandingOAuth.Startup))]
namespace UnderstandingOAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
