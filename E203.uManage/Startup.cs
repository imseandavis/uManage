﻿using System.Net;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using S203.uManage.Handlers;

namespace S203.uManage
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
#if DEBUG
            appBuilder.UseErrorPage();
#endif

            var config = new HttpConfiguration();

            // Enable Windows Auth
            var listener = (HttpListener)appBuilder.Properties[typeof(HttpListener).FullName];
            listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;

            // Enable CORS support
            appBuilder.UseCors(CorsOptions.AllowAll);

            // Disable XML support
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Pretty JSON
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

#if DEBUG
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
#endif

            // Default Router
            config.MapHttpAttributeRoutes();

            // Load Static Files
            appBuilder.UseFileServer(new FileServerOptions()
            {
                RequestPath = PathString.Empty,
                FileSystem = new PhysicalFileSystem(@".\Web")
            });

            // Enable Message Handlers
            config.MessageHandlers.Add(new ApiLoggingHandler()); // TODO: For any non-200 OK response, this throws a 500 error 
            // ArgumentException: The 'DelegatingHandler' list is invalid because the property 'InnerHandler' of 'ApiLoggingHandler' is not null. Parameter name: handlers

            // Endable Ninject
            appBuilder.UseNinjectMiddleware(DependencyConfig.CreateKernel);
            appBuilder.UseNinjectWebApi(config);

            appBuilder.UseWebApi(config);
        }
    }
}
