using System;
using System.Web.Http;
using S203.uManage.Data.Interfaces;
using S203.uManage.Models;

namespace S203.uManage.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IDirectory<User> _usersDirectory;

        public UsersController(IDirectory<User> usersDirectory)
        {
            if (_usersDirectory == null)
                _usersDirectory = usersDirectory;
        }

        [HttpGet, Route()]
        public IHttpActionResult GetAllUsers()
        {
            var users = _usersDirectory.All;

            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpGet, Route("me")]
        public IHttpActionResult GetCurrentUser()
        {
            var user = _usersDirectory.Find(User.Identity.Name);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet, Route("{id}")]
        public IHttpActionResult GetUser(Guid id)
        {
            var user = _usersDirectory.Find(User.Identity.Name);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
