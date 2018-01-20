using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketingPostManager.Startup))]
namespace MarketingPostManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
