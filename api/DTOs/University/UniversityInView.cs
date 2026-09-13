using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Review;

namespace api.DTOs.University
{
    public class UniversityInView
    {
        public Guid UniversityId { get; set; }
        public required string Name { get; set; }
        public int ReviewCount { get; set; }
        public UniversityRatingSummary AverageRatings { get; set; } = new();

        public List<ReviewInView> Reviews { get; set; } = new();
    }
}