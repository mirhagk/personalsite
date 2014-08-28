using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace webserver
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Robots.txt",
                "robots.txt",
                new { controller = "Meta", action = "Robots" });

            routes.MapRoute("Sitemap.xml",
                "sitemap.xml",
                new { controller = "Meta", action = "Sitemap" });
            routes.MapRoute(
                "Partials",
                "Partial/{action}/{origController}/{origAction}/{id}",
                defaults: new { controller = "Partial", action = "Render", id = UrlParameter.Optional, origAction = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}