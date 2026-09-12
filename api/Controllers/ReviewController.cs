using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.DTOs.Review;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IUniversityRepository _universityRepo;

        public ReviewController(IReviewRepository reviewRepository, IUniversityRepository universityRepository)
        {
            _reviewRepo = reviewRepository;
            _universityRepo = universityRepository;
        }

        [Authorize]
        [HttpGet]
        [Route("{reviewId:guid}")]
        public async Task<IActionResult> GetReviewById([FromRoute] Guid reviewId)
        {
            var review = await _reviewRepo.GetReviewByIdAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review.ToReviewView());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewInCreate reviewInCreateObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var university = await _universityRepo.GetUniversityByIdAsync(reviewInCreateObject.UniversityId);
            if (university == null)
            {
                return BadRequest("University not found");
            }

            var existingReview = await _reviewRepo.GetReviewByUserAndUniversityAsync(userId, reviewInCreateObject.UniversityId);
            if (existingReview != null)
            {
                return Conflict("You have already reviewed this university. Try updating your existing review instead.");
            }

            var review = await _reviewRepo.CreateReviewAsync(reviewInCreateObject, userId);
            return CreatedAtAction(nameof(GetReviewById), new { reviewId = review.ReviewId }, review.ToReviewView());
        }

        [Authorize]
        [HttpPut]
        [Route("{reviewId:guid}")]
        public async Task<IActionResult> UpdateReview([FromRoute] Guid reviewId, [FromBody] ReviewInUpdate reviewInUpdateObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var review = await _reviewRepo.UpdateReviewAsync(reviewId, userId, reviewInUpdateObject);
            if (review == null)
            {
                // Either it doesn't exist, or it belongs to someone else — same response either way,
                // so we don't leak which reviews exist and who owns them.
                return NotFound("Review not found, or it doesn't belong to you.");
            }

            return Ok(review.ToReviewView());
        }
    }
}