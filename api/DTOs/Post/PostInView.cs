using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;

namespace api.DTOs.Post
{
    public class PostInView
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 character long.")]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public required string Title { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 character long.")]
        [MaxLength(5000, ErrorMessage = "Content cannot exceed 5000 characters.")]
        public required string Content { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        //add comments too
        public List<CommentInView> Comments { get; set; } = new List<CommentInView>();
    }
}