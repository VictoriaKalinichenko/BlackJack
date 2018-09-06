using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using BlackJack.UI.Config;

[assembly: OwinStartup(typeof(BlackJack.UI.Startup))]

namespace BlackJack.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutofacConfig.ConfigureContainer();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}