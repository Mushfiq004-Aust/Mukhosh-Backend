using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Comments")]
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

        //foreign key relationship between the Comments and users tables.
        public string? UserId { get; set; } // must provide value, cannot be null

        //navigation property to the user who created the Comment.Through comment.user, I can access the user who created the comment.
        public User User { get; set; } = null!; // to avoid null reference exception
    }
}