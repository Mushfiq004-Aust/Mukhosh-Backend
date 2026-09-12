using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Post;
using api.Helper;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepo;
        private readonly IUserRepository _userRepo;

        public PostController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepo = postRepository;
            _userRepo = userRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] QueryObject query)
        {
            var posts = await _postRepo.GetAllPostsAsync(query);
            var postView = posts.Select(p => p.ToPostView()).ToList();
            return Ok(postView);
        }

        [Authorize]
        [HttpGet]
        [Route("{postId:guid}")]
        public async Task<IActionResult> GetPostById([FromRoute] Guid postId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var post = await _postRepo.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post.ToSinglePostView());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostInCreate postInCreateObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Check if the user exists before creating a post for it
            var existingUser = await _userRepo.GetUserByIdAsync(postInCreateObject.UserId);
            //Could use anyasync method to check if the user exists, but this is a simple way to do it
            if (existingUser == null)
            {
                return BadRequest("User not found");
            }

            var post = await _postRepo.CreatePostAsync(postInCreateObject);
            return CreatedAtAction(nameof(GetPostById), new { postId = post.PostId }, post.ToSinglePostView());
        }

        [Authorize]
        [HttpPut]
        [Route("{postId:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid postId, [FromBody] PostInUpdate postInUpdateObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postRepo.UpdatePostAsync(postId, postInUpdateObject);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post.ToSinglePostView());
        }

        [Authorize]
        [HttpDelete]
        [Route("{postId:guid}")]
        public async Task<IActionResult> DeletePost([FromRoute] Guid postId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postRepo.DeletePostAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}