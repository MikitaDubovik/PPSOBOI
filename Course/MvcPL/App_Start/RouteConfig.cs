using System.Web.Mvc;
using System.Web.Routing;

namespace MvcPL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Posts", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Error",
                url: "{controller}/{action}/{id}/{*catchall}", 
                defaults: new { controller = "Error", action = "NotFound", id = UrlParameter.Optional });
        }
    }
}
