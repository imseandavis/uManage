using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using S203.uManage.Models;

namespace S203.uManage.Controllers
{
    [RoutePrefix("api")]
    public class SystemController : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult GetApiHome()
        {
            var result = new Information
            {
                Application = "uManage Api",
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                ApiVersion = "1.0.0",
                CurrentUser = User.Identity.Name
            };

            result.Vitals.Add("api_server", true);

            var apis = Configuration.Services.GetApiExplorer();
            foreach (var api in apis.ApiDescriptions)
            {
                var name = api.ActionDescriptor.ActionName;
                var route = "/" + api.Route.RouteTemplate;

                // Check for root endpoint
                var currentRoute = "/" + RequestContext.RouteData.Route.RouteTemplate;
                result._links.Add(route == currentRoute ? "self" : name, new Link(route));
            }

            return Ok(result);
        }

        [HttpGet, Route("vitals")]
        public IHttpActionResult GetVitals()
        {
            var result = new Dictionary<string, bool>();

            result.Add("api_server", true);

            return Ok(result);
        }
    }
}
