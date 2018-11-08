using System.Web.Mvc;
using System.Web.Routing;

namespace BlackJack.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "EndRound",
                "Round/End/",
                new { controller = "Round", action = "End", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "StartRound",
                "Round/Start/",
                new { controller = "Round", action = "Start", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Start", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
