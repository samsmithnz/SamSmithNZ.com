using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SSNZ.IntFootball.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Enable CORS: http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter); //Remove the XML
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html")); //Make the JSON the default;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApiPost",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
