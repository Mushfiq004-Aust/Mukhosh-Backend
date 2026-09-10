using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comment
{
    public class CommentInView
    {
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}