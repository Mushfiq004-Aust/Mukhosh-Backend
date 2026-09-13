using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// for database context
using api.Database;
using api.DTOs.User;
using api.Helper;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserRepository _userRepo; // The IUserRepository interface is injected into the controller through dependency injection

        public UserController(IUserRepository userRepository)
        {
            _userRepo = userRepository; // initialize the user repository through dependency injection
            //_context = context; // initialize the database context through dependency injection
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] QueryObject query)
        {
            var users = await _userRepo.GetAllUsersAsync(query); // only async part is the database call, not the mapping

            var usersInView = users.Select(user => user.ToUserInView());
            //map the user model to the response model using the mapper class
            //Select is used to project each user object to a UserInView object using the ToUserInView extension method defined in the UserMapper class.

            return Ok(usersInView); //200 OK status code with the list of users in the response body
        }

        [Authorize]
        [HttpGet]
        [Route("me")] // api/User/{userId}
        public async Task<IActionResult> GetUserById()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userRepo.GetUserByIdAsync(userId); // FirstOrDefault also works, but Find is more efficient because it uses the primary key to find the user.
            if (user == null)
            {
                return NotFound();//404 Not Found status code if the user is not found
            }
            return Ok(user.ToSingleUserInView()); //200 OK status code with the user in the response body
        }

        [Authorize]
        [HttpPut]
        [Route("me")]
        public async Task<IActionResult> UpdateUser([FromBody] UserInUpdate userInUpdateObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var userUpdates = await _userRepo.UpdateUserAsync(userId, userInUpdateObject);
            if (userUpdates == null)
            {
                return NotFound();
            }
            return Ok(userUpdates.ToSingleUserInView());
        }

        [Authorize]
        [HttpDelete]
        [Route("me")]
        public async Task<IActionResult> DeleteUser()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userRepo.DeleteUserAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return NoContent(); //204 No Content status code, because the user has been deleted and there is no content to return
        }

    }
}



