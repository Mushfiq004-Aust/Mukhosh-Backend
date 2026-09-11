using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{

    public enum Vibe{ Positive, Negative, Mixed }
    public class Post
    {
        public Guid PostId { get; set; } // must provide value, cannot be null, must match the classname 

        public required string Title { get; set; } // must provide value, cannot be null

        public required string Content { get; set; } // must provide value, cannot be null

        public required Vibe Vibe { get; set; } = Vibe.Mixed;

        public DateTime CreatedAt { get; set; } = DateTime.Now; // If not provided, database get current date and time value

        //Empty List is a c# Object, so it will not be null, but it can be empty.
        //EF migration will create a foreign key relationship between the posts and comments tables.
        //Also i can use post.comments to get all the comments of a post.
        public List<Comment> Comment { get; set; } = new List<Comment>(); 

        //foreign key relationship between the posts and users tables.
        public Guid UserId { get; set; } // must provide value, cannot be null

        //navigation property to the user who created the post.Through post.user, I can access the user who created the post.
        public User User { get; set; } = null!; // to avoid null reference exception

        
    }
}