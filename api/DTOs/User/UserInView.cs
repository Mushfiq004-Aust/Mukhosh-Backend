using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Post;

namespace api.DTOs.User
{
    public class UserInView
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Institution { get; set; }
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<PostInView> Post { get; set; } = new List<PostInView>();
    }
}