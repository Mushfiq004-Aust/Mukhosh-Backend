using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// for database context
using api.Database;
using api.DTOs.User;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

//Controller is a class that handles the HTTP requests and responses
//Also it directly interacts with the database through the ApplicationDBContext class, which is injected into the controller through dependency injection.
namespace api.Controllers
{
    [Route("api/[controller]")] // api/User
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context; // immutable variable, cannot be changed after initialization
        public UserController(ApplicationDBContext context)
        {
            _context = context; // initialize the database context through dependency injection
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.User.ToList().Select(user => user.ToUserInView());
            //map the user model to the response model using the mapper class
            //Select is used to project each user object to a UserInView object using the ToUserInView extension method defined in the UserMapper class.

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
            return Ok(user.ToUserInView()); //200 OK status code with the user in the response body
        }

        [HttpPost]
        //FromBody is used to bind the request body to the user object
        //Taking request body object and mapping it to the User model using the mapper class, then saving it to the database.
        public IActionResult CreateUser([FromBody] UserInCreate userInCreateObject)
        {
            var user = userInCreateObject.ToUserInCreate(); //map the request body to the User model using the mapper class
            _context.User.Add(user); // add the user to the database context
            _context.SaveChanges(); // save the changes to the database

            //201 Created status code with the user in the response body
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user.ToUserInView());
        }


        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UserInUpdate userInUpdateObject)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var updatedUser = userInUpdateObject.ToUserInUpdate(); //map the request body to the User model using the mapper class
            user.Name = updatedUser.Name;
            user.Phone = updatedUser.Phone;

            _context.SaveChanges();
            return Ok(user.ToUserInView());
        }
        

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            _context.SaveChanges();
            return NoContent(); //204 No Content status code, because the user has been deleted and there is no content to return
        }

    }
}