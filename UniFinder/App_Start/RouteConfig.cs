using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UniFinder
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
            name: "Login",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Dashboard",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Dashboard", action = "Dashboard", id = UrlParameter.Optional }
            );

            routes.MapRoute(
name: "Website",
url: "{controller}/{action}/{id}",
defaults: new { controller = "Website", action = "Index", id = UrlParameter.Optional }
);
        }
    }
}
