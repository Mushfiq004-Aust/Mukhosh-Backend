using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.University
{
    // Aggregated for rounded averages across all reviews for a university.
    public class UniversityRatingSummary
    {
        public double Environment { get; set; }
        public double Faculty { get; set; }
        public double ResearchFacilities { get; set; }
        public double EducationQuality { get; set; }
        public double Pressure { get; set; }
        public double CanteenFood { get; set; }
        public double Administration { get; set; }
        public double Extracurricular { get; set; }
        public double Overall { get; set; }
    }
}