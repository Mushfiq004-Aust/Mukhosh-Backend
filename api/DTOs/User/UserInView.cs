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
        [MinLength(5, ErrorMessage = "Name must be at least 5 character long.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public required string Name { get; set; }

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
        public string Phone { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<PostInView> Post { get; set; } = new List<PostInView>();

        public Guid UserId { get; set; }
    }
}