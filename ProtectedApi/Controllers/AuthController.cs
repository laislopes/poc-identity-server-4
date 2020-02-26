using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtectedApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Policy = "AuthorizationPolicy")]
    [Authorize]

    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult FakeData()
        {
            var currentUser = this.HttpContext.User.Identity.Name;
            var users = new List<User>
        {
            new User
            {
                Id = 1, FullName = "John Doe",
                Email = "johndoe@example.com"
            },
            new User
            {
                Id = 2, FullName = "Elizabeth Johnson",
                Email = "elizabeth11245@fakemail.com"
            },
            new User
            {
                Id = 3, FullName = "Bob Rogers",
                Email = "br1964@anotherfake.com"
            }
        };

            return Ok(users);
        }
    }
}
