using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepo.GetAllCommentsAsync();
            var commentViews = comments.Select(c => c.ToCommentView()).ToList();
            return Ok(commentViews);
        }

        [HttpGet]
        [Route("{commentId:guid}")]
        public async Task<IActionResult> GetCommentById([FromRoute] Guid commentId)
        {
            var comment = await _commentRepo.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentView());
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentInCreate commentInCreateObject)
        {
            var comment = await _commentRepo.CreateCommentAsync(commentInCreateObject);
            return CreatedAtAction(nameof(GetCommentById), new { commentId = comment.CommentId }, comment.ToCommentView());
        }

        [HttpPut]
        [Route("{commentId:guid}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid commentId, [FromBody] CommentInUpdate commentInUpdateObject)
        {
            var updatedComment = await _commentRepo.UpdateCommentAsync(commentId, commentInUpdateObject);
            if (updatedComment == null)
            {
                return NotFound();
            }
            return Ok(updatedComment.ToCommentView());
        }

        [HttpDelete]
        [Route("{commentId:guid}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid commentId)
        {
            var deletedComment = await _commentRepo.DeleteCommentAsync(commentId);
            if (deletedComment == null)
            {
                return NotFound();
            }
            return NoContent(); // 204 No Content status code indicating successful deletion    
        }

    }
}