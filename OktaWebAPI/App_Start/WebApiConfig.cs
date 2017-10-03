using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace OktaWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.EnableCors();
            var enableCorsAttribute = new System.Web.Http.Cors.EnableCorsAttribute("*", "Accept, Authorization", "GET, OPTIONS");
            config.EnableCors(enableCorsAttribute);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
