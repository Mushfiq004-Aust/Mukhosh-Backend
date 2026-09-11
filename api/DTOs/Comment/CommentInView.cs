using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comment
{
    public class CommentInView
    {
        [Required]
        [MinLength(1, ErrorMessage = "Content must be at least 1 character long.")]
        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid CommentId { get; set; }
    }
}