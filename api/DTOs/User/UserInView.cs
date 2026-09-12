using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Post;

namespace api.DTOs.User
{
    public class UserInView
    {
        [Required]
        [MinLength(5, ErrorMessage = "First name must be at least 5 character long.")]
        [MaxLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        public required string FirstName { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Last name must be at least 5 character long.")]
        [MaxLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
        public required string LastName { get; set; }


        [Required]
        [MinLength(5, ErrorMessage = "Usename must be at least 5 character long.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public required string UserName { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Email must be at least 10 character long.")]
        [MaxLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
        public required string Email { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Institution must be at least 5 character long.")]
        [MaxLength(50, ErrorMessage = "Institution cannot exceed 50 characters.")]
        public required string Institution { get; set; }

        [Required]
        [MinLength(11, ErrorMessage = "Phone Number must be at least 11 character long.")]
        [MaxLength(11, ErrorMessage = "Phone Number cannot exceed 11 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<PostInView> Post { get; set; } = new List<PostInView>();

        public string? UserId { get; set; }
    }
}