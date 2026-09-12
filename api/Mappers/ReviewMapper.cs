using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Review;
using api.Models;

namespace api.Mappers
{
    public static class ReviewMapper
    {
        // userId comes from the JWT claim in the controller.
        public static Review ToReviewCreate(this ReviewInCreate dto, string userId)
        {
            return new Review
            {
                Content = dto.Content,
                Environment = dto.Environment,
                Faculty = dto.Faculty,
                ResearchFacilities = dto.ResearchFacilities,
                EducationQuality = dto.EducationQuality,
                Pressure = dto.Pressure,
                CanteenFood = dto.CanteenFood,
                Administration = dto.Administration,
                Extracurricular = dto.Extracurricular,
                UniversityId = dto.UniversityId,
                UserId = userId
            };
        }

        public static Review ToReviewUpdate(this ReviewInUpdate dto, Review review)
        {
            review.Content = dto.Content;
            review.Environment = dto.Environment;
            review.Faculty = dto.Faculty;
            review.ResearchFacilities = dto.ResearchFacilities;
            review.EducationQuality = dto.EducationQuality;
            review.Pressure = dto.Pressure;
            review.CanteenFood = dto.CanteenFood;
            review.Administration = dto.Administration;
            review.Extracurricular = dto.Extracurricular;
            return review;
        }

        public static ReviewInView ToReviewView(this Review review)
        {
            return new ReviewInView
            {
                ReviewId = review.ReviewId,
                Content = review.Content,
                Environment = review.Environment,
                Faculty = review.Faculty,
                ResearchFacilities = review.ResearchFacilities,
                EducationQuality = review.EducationQuality,
                Pressure = review.Pressure,
                CanteenFood = review.CanteenFood,
                Administration = review.Administration,
                Extracurricular = review.Extracurricular,
                CreatedAt = review.CreatedAt,
                UniversityId = review.UniversityId,
                UserId = review.UserId
            };
        }
    }
}