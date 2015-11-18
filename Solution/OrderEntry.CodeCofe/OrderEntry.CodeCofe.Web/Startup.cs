using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrderEntry.CodeCofe.Web.Startup))]
namespace OrderEntry.CodeCofe.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
