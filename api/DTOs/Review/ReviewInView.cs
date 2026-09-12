using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Review
{
    public class ReviewInView
    {
        public Guid ReviewId { get; set; }
        public required string Content { get; set; }

        public int Environment { get; set; }
        public int Faculty { get; set; }
        public int ResearchFacilities { get; set; }
        public int EducationQuality { get; set; }
        public int Pressure { get; set; }
        public int CanteenFood { get; set; }
        public int Administration { get; set; }
        public int Extracurricular { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid UniversityId { get; set; }
        public string? UserId { get; set; }
    }
}