using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Post
{
    public class PostInUpdate
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 character long.")]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public required string Title { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 character long.")]
        [MaxLength(5000, ErrorMessage = "Content cannot exceed 5000 characters.")]
        public required string Content { get; set; }
    }
}