using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserInCreate userInCreateObject)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newUser = userInCreateObject.ToUserInCreate();

                var createdUser = await _userManager.CreateAsync(newUser, userInCreateObject.Password);

                if (createdUser.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(newUser, "User");

                    if (role.Succeeded)
                    {
                        return Ok(newUser.ToUserToken(_tokenService));
                    }
                    else
                    {
                        return StatusCode(500, role.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }


            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserInLogin userLoginInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userLoginInfo.UserName);

            if (user == null)
            {
                return Unauthorized("Invalid Username!");
            }

            var matchedPassword = await _signInManager.CheckPasswordSignInAsync(user, userLoginInfo.Password, false);

            if (!matchedPassword.Succeeded)
            {
                return Unauthorized("Username Not macthed and/or wrong Password!");
            }

            return Ok(
                user.ToUserToken(_tokenService)
            );
        }

    }
}