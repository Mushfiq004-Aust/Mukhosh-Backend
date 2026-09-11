using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Post;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepo;

        public PostController(IPostRepository postRepository)
        {
            _postRepo = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepo.GetAllPostsAsync();
            var postView = posts.Select(p => p.ToPostView()).ToList();
            return Ok(postView);
        }

        [HttpGet]
        [Route("{postId:guid}")]
        public async Task<IActionResult> GetPostById([FromRoute] Guid postId)
        {
            var post = await _postRepo.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post.ToPostView());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostInCreate postInCreateObject)
        {
            var post = await _postRepo.CreatePostAsync(postInCreateObject);
            return CreatedAtAction(nameof(GetPostById), new { postId = post.PostId }, post.ToPostView());
        }

        [HttpPut]
        [Route("{postId:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid postId, [FromBody] PostInUpdate postInUpdateObject)
        {
            var post = await _postRepo.UpdatePostAsync(postId, postInUpdateObject);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post.ToPostView());
        }

        [HttpDelete]
        [Route("{postId:guid}")]
        public async Task<IActionResult> DeletePost([FromRoute] Guid postId)
        {
            var post = await _postRepo.DeletePostAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}