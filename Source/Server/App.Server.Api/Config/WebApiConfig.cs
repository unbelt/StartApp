﻿using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace App.Server.Api.Config
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // JSON Formatters
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Cross-origin resource sharing
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }
    }
}