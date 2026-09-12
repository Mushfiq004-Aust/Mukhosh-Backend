using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.DTOs.Review;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReviewRepo : IReviewRepository
    {
        private readonly ApplicationDBContext _context;

        public ReviewRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Review> CreateReviewAsync(ReviewInCreate review, string userId)
        {
            var newReview = review.ToReviewCreate(userId);
            await _context.Review.AddAsync(newReview);
            await _context.SaveChangesAsync(); // if a duplicate slips through, the unique index throws here
            return newReview;
        }

        public async Task<Review?> GetReviewByIdAsync(Guid reviewId)
        {
            return await _context.Review.FindAsync(reviewId);
        }

        public async Task<Review?> GetReviewByUserAndUniversityAsync(string userId, Guid universityId)
        {
            return await _context.Review
                .FirstOrDefaultAsync(r => r.UserId == userId && r.UniversityId == universityId);
        }

        public async Task<Review?> UpdateReviewAsync(Guid reviewId, string userId, ReviewInUpdate review)
        {
            var existingReview = await _context.Review.FindAsync(reviewId);

            // Not found, OR found but belongs to someone else — treat both as "can't update."
            if (existingReview == null || existingReview.UserId != userId)
            {
                return null;
            }

            existingReview = review.ToReviewUpdate(existingReview);
            await _context.SaveChangesAsync();
            return existingReview;
        }
    }
}