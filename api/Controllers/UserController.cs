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
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.User.ToListAsync(); // only async part is the database call, not the mapping
            var usersInView = users.Select(user => user.ToUserInView());
            //map the user model to the response model using the mapper class
            //Select is used to project each user object to a UserInView object using the ToUserInView extension method defined in the UserMapper class.

            return Ok(usersInView); //200 OK status code with the list of users in the response body
        }

        [HttpGet]
        [Route("{id:guid}")] // api/User/{id}
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await _context.User.FindAsync(id); // FirstOrDefault also works, but Find is more efficient because it uses the primary key to find the user.
            if (user == null)
            {
                return NotFound();//404 Not Found status code if the user is not found
            }
            return Ok(user.ToUserInView()); //200 OK status code with the user in the response body
        }

        [HttpPost]
        //FromBody is used to bind the request body to the user object
        //Taking request body object and mapping it to the User model using the mapper class, then saving it to the database.
        public async Task<IActionResult> CreateUser([FromBody] UserInCreate userInCreateObject)
        {
            var user = userInCreateObject.ToUserInCreate(); //map the request body to the User model using the mapper class
            await _context.User.AddAsync(user); // add the user to the database context
            await _context.SaveChangesAsync(); // save the changes to the database

            //201 Created status code with the user in the response body
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user.ToUserInView());
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserInUpdate userInUpdateObject)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var updatedUser = userInUpdateObject.ToUserInUpdate(); //map the request body to the User model using the mapper class
            user.Name = updatedUser.Name;
            user.Phone = updatedUser.Phone;

            await _context.SaveChangesAsync();
            return Ok(user.ToUserInView());
        }
        

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            // remove is not async, because it only marks the entity for deletion, and does not actually delete it from the database until SaveChangesAsync is called.
            await _context.SaveChangesAsync();
            
            return NoContent(); //204 No Content status code, because the user has been deleted and there is no content to return
        }

    }
}