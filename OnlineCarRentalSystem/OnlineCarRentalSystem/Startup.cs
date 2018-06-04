using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineCarRentalSystem.Startup))]
namespace OnlineCarRentalSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
