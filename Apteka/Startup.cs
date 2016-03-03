using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Apteka.Startup))]
namespace Apteka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
