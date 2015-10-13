using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestWebRemake.Startup))]
namespace TestWebRemake
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
