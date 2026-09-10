using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class User
    {
        public Guid UserId { get; set; } // must provide value, cannot be null, must match the classname 

        public required string Name { get; set; } // must provide value, cannot be null

        [EmailAddress] // for validating the email format
        public required string Email { get; set; } // must be unique, cannot be null

        public required string Password { get; set; } // must provide value, cannot be null

        public string? Institution { get; set; } // If not provided, database get null value

        public string Phone { get; set; } = string.Empty; // If not provided, database get empty string value

        public DateTime CreatedAt { get; set; } = DateTime.Now; // If not provided, database get current date and time value

        //Ef migration will create a foreign key relationship between the users and posts tables.
        //Also i can use user.posts to get all the posts of a user, and user.posts[x].comments to get all the comments of that post.
        public List<Post> Post { get; set; } = new List<Post>();
    }
}