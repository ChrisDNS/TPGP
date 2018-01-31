using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Routing;
using TPGPServices.Controllers;

namespace TPGPServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuration et services API Web

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            RouteTable.Routes.MapHttpRoute(
            name: "DefaultApi",
             routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
              ).RouteHandler = new SessionStateRouteHandler();

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
          
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
