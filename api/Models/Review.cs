using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Reviews")]
    public class Review
    {
        public Guid ReviewId { get; set; } // must provide value, cannot be null

        public required string Content { get; set; }

        // All rating fields are out of 5, default 0 until the user rates them.
        public int Environment { get; set; } = 0;
        public int Faculty { get; set; } = 0;
        public int ResearchFacilities { get; set; } = 0;
        public int EducationQuality { get; set; } = 0;
        public int Pressure { get; set; } = 0;
        public int CanteenFood { get; set; } = 0;
        public int Administration { get; set; } = 0;
        public int Extracurricular { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // foreign key relationship between reviews and universities
        public Guid UniversityId { get; set; }
        public University University { get; set; } = null!;

        // foreign key relationship between reviews and users (Identity's string id)
        public string? UserId { get; set; }
        public User User { get; set; } = null!;
    }
}