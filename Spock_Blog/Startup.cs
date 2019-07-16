using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Spock_Blog.Startup))]
namespace Spock_Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
