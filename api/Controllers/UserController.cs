using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// for database context
using api.Database;

using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")] // api/User
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context; // immutable variable, cannot be changed after initialization
        public UserController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.User.ToList();
            return Ok(users); //200 OK status code with the list of users in the response body
        }

        [HttpGet]
        [Route("{id:guid}")] // api/User/{id}
        public IActionResult GetUserById([FromRoute] Guid id)
        {
            var user = _context.User.Find(id); // FirstOrDefault also works, but Find is more efficient because it uses the primary key to find the user.
            if (user == null)
            {
                return NotFound();//404 Not Found status code if the user is not found
            }
            return Ok(user);
        }
    }
}