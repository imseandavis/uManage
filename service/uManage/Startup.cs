using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;

namespace S203.uManage
{
    internal class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            // Enable CORS
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
                EnableDirectoryBrowsing = true,
                RequestPath = new PathString("/app"),
                FileSystem = new EmbeddedResourceFileSystem("S203.uManage.Static.Web")
            });
        }
    }
}
