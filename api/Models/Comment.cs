using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; } // must provide value, cannot be null, must match the classname 

        public required string Content { get; set; } // must provide value, cannot be null

        public DateTime CreatedAt { get; set; } = DateTime.Now; // If not provided, database get current date and time value

        //foreign key relationship between the comments and posts tables.
        public Guid PostId { get; set; } // must provide value, cannot be null

        //navigation property to the post that the comment belongs to.
        //I can get the post of a comment through comment.post, and I can get the user of a comment through comment.post.user.
        public Post Post { get; set; } = null!; // to avoid null reference exception
    }
}