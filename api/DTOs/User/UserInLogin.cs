using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.User
{
    public class UserInLogin
    {
        [Required]
        [MinLength(5, ErrorMessage = "UserName must be at least 5 character long.")]
        [MaxLength(50, ErrorMessage = "UserName cannot exceed 50 characters.")]
        public required string UserName { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Password must be at least 10 character long.")]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
        public required string Password { get; set; }
    }
}