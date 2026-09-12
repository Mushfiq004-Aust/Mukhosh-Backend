using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Review;
using api.Models;

namespace api.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review?> GetReviewByUserAndUniversityAsync(string userId, Guid universityId);
        Task<Review?> GetReviewByIdAsync(Guid reviewId);
        Task<Review> CreateReviewAsync(ReviewInCreate review, string userId);
        Task<Review?> UpdateReviewAsync(Guid reviewId, string userId, ReviewInUpdate review);
    }
}