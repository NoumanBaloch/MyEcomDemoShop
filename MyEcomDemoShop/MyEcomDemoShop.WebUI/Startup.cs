using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyEcomDemoShop.WebUI.Startup))]
namespace MyEcomDemoShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
