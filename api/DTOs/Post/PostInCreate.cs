using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Post
{
    public class PostInCreate
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public Guid UserId { get; set; }
    }
}