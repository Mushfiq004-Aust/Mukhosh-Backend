using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class User
    {
        public Guid userId { get; set; } // must provide value, cannot be null

        public required string name { get; set; } // must provide value, cannot be null

        public required string  email { get; set; } // must be unique, cannot be null

        public string? Institution { get; set; } // If not provided, database get null value

        public DateTime createdAt { get; set; } = DateTime.Now; // If not provided, database get current date and time value
        
        public string phone { get; set; } = string.Empty; // If not provided, database get empty string value

        //Ef migration will create a foreign key relationship between the users and posts tables.
        //Also i can use user.posts to get all the posts of a user, and user.posts[x].comments to get all the comments of that post.
        public List<Posts> posts { get; set; } = new List<Posts>();
    }
}