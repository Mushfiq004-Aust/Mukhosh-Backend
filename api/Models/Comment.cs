using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public Guid commentId { get; set; } // must provide value, cannot be null

        public required string content { get; set; } // must provide value, cannot be null

        public DateTime createdAt { get; set; } = DateTime.Now; // If not provided, database get current date and time value

        //foreign key relationship between the comments and posts tables.
        public Guid postId { get; set; } // must provide value, cannot be null

        //navigation property to the post that the comment belongs to.
        //I can get the post of a comment through comment.post, and I can get the user of a comment through comment.post.user.
        public Posts post { get; set; } = null!; // to avoid null reference exception
    }
}