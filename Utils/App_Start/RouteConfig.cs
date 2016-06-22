using System.Web.Mvc;
using System.Web.Routing;

namespace Intouch.Utils
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "QR", action = "BarcodeImage", id = UrlParameter.Optional },
                new[] { "Intouch.Utils.Controllers" }
            );
        }
    }
}