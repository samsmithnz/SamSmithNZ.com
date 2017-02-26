using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SamSmithNZ2017.Startup))]
namespace SamSmithNZ2017
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
