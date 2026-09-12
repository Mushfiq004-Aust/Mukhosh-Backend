using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.User
{
    public class UserInCreate
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name must be at least 5 character long.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public required string Name { get; set; }

        [EmailAddress] // for validating the email format
        [Required]
        [MinLength(10, ErrorMessage = "Email must be at least 10 character long.")]
        [MaxLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
        public required string Email { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Password must be at least 10 character long.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        public required string Password { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Institution must be at least 5 character long.")]
        [MaxLength(50, ErrorMessage = "Institution cannot exceed 50 characters.")]
        public required string Institution { get; set; }

        [Required]
        [MinLength(11, ErrorMessage = "Phone Number must be at least 11 character long.")]
        [MaxLength(11, ErrorMessage = "Phone Number cannot exceed 11 characters.")]
        public string Phone { get; set; } = string.Empty;
    }
}