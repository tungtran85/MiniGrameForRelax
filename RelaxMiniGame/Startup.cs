using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RelaxMiniGame.Startup))]
namespace RelaxMiniGame
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
