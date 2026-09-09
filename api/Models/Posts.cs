using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Posts
    {
        public Guid postId { get; set; } // must provide value, cannot be null

        public required string title { get; set; } // must provide value, cannot be null

        public required string content { get; set; } // must provide value, cannot be null

        public DateTime createdAt { get; set; } = DateTime.Now; // If not provided, database get current date and time value

        //Empty List is a c# Object, so it will not be null, but it can be empty.
        //EF migration will create a foreign key relationship between the posts and comments tables.
        //Also i can use post.comments to get all the comments of a post.
        public List<Comment> comments { get; set; } = new List<Comment>(); 

        //foreign key relationship between the posts and users tables.
        public Guid userId { get; set; } // must provide value, cannot be null

        //navigation property to the user who created the post.Through post.user, I can access the user who created the post.
        public User user { get; set; } = null!; // to avoid null reference exception
        
    }
}