using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Universities")]
    public class University
    {
        public Guid UniversityId { get; set; } // must provide value, cannot be null

        public required string Name { get; set; } // must provide value, cannot be null

        // EF migration will create a foreign key relationship between universities and reviews.
        public List<Review> Review { get; set; } = new List<Review>();
    }
}