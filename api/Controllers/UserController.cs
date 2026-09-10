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
        private readonly IUserRepository _userRepo; // The IUserRepository interface is injected into the controller through dependency injection

        public UserController(ApplicationDBContext context, IUserRepository userRepository)
        {
            _userRepo = userRepository; // initialize the user repository through dependency injection
            _context = context; // initialize the database context through dependency injection
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync(); // only async part is the database call, not the mapping

            var usersInView = users.Select(user => user.ToUserInView());
            //map the user model to the response model using the mapper class
            //Select is used to project each user object to a UserInView object using the ToUserInView extension method defined in the UserMapper class.

            return Ok(usersInView); //200 OK status code with the list of users in the response body
        }

        [HttpGet]
        [Route("{id:guid}")] // api/User/{id}
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await _userRepo.GetUserByIdAsync(id); // FirstOrDefault also works, but Find is more efficient because it uses the primary key to find the user.
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
            var user = await _userRepo.CreateUserAsync(userInCreateObject); //map the request body to the User model using the mapper class
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user.ToUserInView());
            //201 Created status code with the user in the response body
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserInUpdate userInUpdateObject)
        {
            var userUpdates = await _userRepo.UpdateUserAsync(id, userInUpdateObject);
            if (userUpdates == null)
            {
                return NotFound();
            }
            return Ok(userUpdates.ToUserInView());
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _userRepo.DeleteUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return NoContent(); //204 No Content status code, because the user has been deleted and there is no content to return
        }

    }
}



