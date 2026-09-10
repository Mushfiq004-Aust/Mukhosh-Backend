using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comment
{
    public class CommentInCreate
    {
        public required string Content { get; set; } // must provide value, cannot be null
        public Guid PostId { get; set; } // must provide value, cannot be null
    }
}