using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.DTOs.Favourite;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPostRepository _postRepository;

        private readonly IFavouriteRepository _favoruiteRepository;
        public FavouriteController(UserManager<User> userManager, IPostRepository postRepository, IFavouriteRepository favouriteRepository)
        {
            _userManager = userManager;
            _postRepository = postRepository;
            _favoruiteRepository = favouriteRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserFavourites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var posts = await _favoruiteRepository.GetFavourites(user);

            var postView = posts
                .Select(p => p.ToPostView())
                .ToList();

            return Ok(postView);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFavourite(Guid postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            var alreadyFavourited = await _favoruiteRepository.FavouriteExistsAsync(user.Id, postId);
            if (alreadyFavourited)
            {
                return Conflict("You've already favourited this post.");
            }

            var favourite = await _favoruiteRepository.CreateFavourite(
                new FavouriteInCreate
                {
                    UserId = user.Id,
                    PostId = post.PostId
                }
            );

            return Ok();
        }


        [Authorize]
        [HttpDelete("{postId:guid}")]
        public async Task<IActionResult> DeleteFavourite(Guid postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var favourite = await _favoruiteRepository
                .DeleteFavourite(userId, postId);

            if (favourite == null)
            {
                return NotFound("Favourite not found.");
            }

            return Ok("Favourite deleted successfully.");
        }

    }
}