using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.University
{
    public class UniversityInCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        [MaxLength(150, ErrorMessage = "Name cannot exceed 150 characters.")]
        public required string Name { get; set; }
    }
}