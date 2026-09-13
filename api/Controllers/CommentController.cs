using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Helper;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IPostRepository _postRepo;

        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepo = commentRepository;
            _postRepo = postRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllComments([FromQuery] QueryObject query)
        {
            var comments = await _commentRepo.GetAllCommentsAsync(query);
            var commentViews = comments.Select(c => c.ToCommentView()).ToList();
            return Ok(commentViews);
        }

        [Authorize]
        [HttpGet]
        [Route("{commentId:guid}")]
        public async Task<IActionResult> GetCommentById([FromRoute] Guid commentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentView());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentInCreate commentInCreateObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Check if the post exists before creating a comment for it
            var existingPost = await _postRepo.GetPostByIdAsync(commentInCreateObject.PostId);
            //could use anyasync method to check if the post exists, but this is a simple way to do it

            if (existingPost == null)
            {
                return BadRequest("Post not found");
            }

            var comment = await _commentRepo.CreateCommentAsync(commentInCreateObject);
            return CreatedAtAction(nameof(GetCommentById), new { commentId = comment.CommentId }, comment.ToCommentView());
        }

        [Authorize]
        [HttpPut]
        [Route("{commentId:guid}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid commentId, [FromBody] CommentInUpdate commentInUpdateObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedComment = await _commentRepo.UpdateCommentAsync(commentId, commentInUpdateObject);
            if (updatedComment == null)
            {
                return NotFound();
            }
            return Ok(updatedComment.ToCommentView());
        }

        [Authorize]
        [HttpDelete]
        [Route("{commentId:guid}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid commentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedComment = await _commentRepo.DeleteCommentAsync(commentId);
            if (deletedComment == null)
            {
                return NotFound();
            }
            return NoContent(); // 204 No Content status code indicating successful deletion    
        }

    }
}