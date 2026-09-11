using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;

namespace api.DTOs.Post
{
    public class PostInView
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        //add comments too
        public List<CommentInView> Comments { get; set; }
    }
}