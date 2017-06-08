using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WOM3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes


            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "api/{controller}/{Login}",
            //    defaults: new { Action = "Login" }
            // );
            //config.Routes.MapHttpRoute(
            //   name: "custom",
            //   routeTemplate: "api/{controller}/set/{action}",
            //   defaults: new { },
            //   constraints: new { action = @"[A-Za-z]+",HttpMethodConstraint= }
            //  );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
