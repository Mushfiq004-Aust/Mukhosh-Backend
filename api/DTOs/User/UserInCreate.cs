using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.User
{
    public class UserInCreate
    {
        public required string Name { get; set; }

        [EmailAddress] // for validating the email format
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Institution { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}