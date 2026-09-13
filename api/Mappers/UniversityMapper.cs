using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.University;
using api.Models;

namespace api.Mappers
{
    public static class UniversityMapper
    {
        public static University ToUniversityCreate(this UniversityInCreate dto)
        {
            return new University
            {
                Name = dto.Name
            };
        }

        // Builds the university view WITH the aggregated, rounded rating summary.

        public static UniversityInView ToUniversitySingleView(this University university)
        {
            var reviews = university.Review;
            int count = reviews.Count;

            double Avg(Func<Review, int> selector) =>
                count == 0 ? 0 : Math.Round(reviews.Average(r => (double)selector(r)), 1);

            double overall = count == 0
                ? 0
                : Math.Round(reviews.Average(r =>
                    (r.Environment + r.Faculty + r.ResearchFacilities + r.EducationQuality +
                     r.Pressure + r.CanteenFood + r.Administration + r.Extracurricular) / 8.0), 1);

            return new UniversityInView
            {
                UniversityId = university.UniversityId,
                Name = university.Name,
                ReviewCount = count,
                AverageRatings = new UniversityRatingSummary
                {
                    Environment = Avg(r => r.Environment),
                    Faculty = Avg(r => r.Faculty),
                    ResearchFacilities = Avg(r => r.ResearchFacilities),
                    EducationQuality = Avg(r => r.EducationQuality),
                    Pressure = Avg(r => r.Pressure),
                    CanteenFood = Avg(r => r.CanteenFood),
                    Administration = Avg(r => r.Administration),
                    Extracurricular = Avg(r => r.Extracurricular),
                    Overall = overall
                },
                Reviews = reviews.Select(r => r.ToReviewView()).ToList()
            };
        }

        public static UniversityInView ToUniversityView(this University university)
        {
            var reviews = university.Review;
            int count = reviews.Count;

            double Avg(Func<Review, int> selector) =>
                count == 0 ? 0 : Math.Round(reviews.Average(r => (double)selector(r)), 1);

            double overall = count == 0
                ? 0
                : Math.Round(reviews.Average(r =>
                    (r.Environment + r.Faculty + r.ResearchFacilities + r.EducationQuality +
                     r.Pressure + r.CanteenFood + r.Administration + r.Extracurricular) / 8.0), 1);

            return new UniversityInView
            {
                UniversityId = university.UniversityId,
                Name = university.Name,
                ReviewCount = count,
                AverageRatings = new UniversityRatingSummary
                {
                    Environment = Avg(r => r.Environment),
                    Faculty = Avg(r => r.Faculty),
                    ResearchFacilities = Avg(r => r.ResearchFacilities),
                    EducationQuality = Avg(r => r.EducationQuality),
                    Pressure = Avg(r => r.Pressure),
                    CanteenFood = Avg(r => r.CanteenFood),
                    Administration = Avg(r => r.Administration),
                    Extracurricular = Avg(r => r.Extracurricular),
                    Overall = overall
                }
            };
        }
    }
}