using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace S203.uManage.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController:ApiController
    {
        [HttpGet, Route("me")]
        public Task<IHttpActionResult> GetCurrentUser()
        {
            var userName = User.Identity.Name;
            throw new NotImplementedException();
        }

        [HttpGet, Route("{id}")]
        public Task<IHttpActionResult> GetUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
