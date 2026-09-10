using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.User
{
    public class UserInCreate
    {
        public required string Name { get; set; } // must provide value, cannot be null
        public required string Email { get; set; } // must be unique, cannot be null
        public required string Password { get; set; } // must provide value, cannot be null
        public string? Institution { get; set; } // If not provided, database get null value
        public string Phone { get; set; } = string.Empty; // If not provided, database get empty string value
    }
}