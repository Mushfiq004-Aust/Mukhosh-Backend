using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class User : IdentityUser
    {
        public Guid UserId { get; set; } // must provide value, cannot be null, must match the classname 

        public required string Name { get; set; } // must provide value, cannot be null

        public required string Institution { get; set; } // must provide value, cannot be null

        public string Phone { get; set; } = string.Empty; // If not provided, database get empty string value

        public DateTime CreatedAt { get; set; } = DateTime.Now; // If not provided, database get current date and time value

        //Ef migration will create a foreign key relationship between the users and posts tables.
        //Also i can use user.posts to get all the posts of a user, and user.posts[x].comments to get all the comments of that post.
        public List<Post> Post { get; set; } = new List<Post>();
    }
}