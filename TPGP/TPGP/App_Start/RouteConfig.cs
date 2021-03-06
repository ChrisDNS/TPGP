﻿using System.Web.Mvc;
using System.Web.Routing;

namespace TPGP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Portfolios",
                url: "Portfolio/{id}",
                defaults: new { controller = "Portfolio", action = "Contracts" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}