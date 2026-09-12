using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Review
{
    public class ReviewInCreate
    {
        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
        [MaxLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public required string Content { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Environment { get; set; } = 0;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Faculty { get; set; } = 0;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int ResearchFacilities { get; set; } = 0;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int EducationQuality { get; set; } = 0;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Pressure { get; set; } = 0;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int CanteenFood { get; set; } = 0;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Administration { get; set; } = 0;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Extracurricular { get; set; } = 0;

        [Required]
        public Guid UniversityId { get; set; }
    }
}