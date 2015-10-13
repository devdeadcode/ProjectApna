using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StripeIntegrationApp.Startup))]
namespace StripeIntegrationApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
