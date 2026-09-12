using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.University
{
    public class UniversityInView
    {
        public Guid UniversityId { get; set; }
        public required string Name { get; set; }
        public int ReviewCount { get; set; }
        public UniversityRatingSummary AverageRatings { get; set; } = new();
    }
}