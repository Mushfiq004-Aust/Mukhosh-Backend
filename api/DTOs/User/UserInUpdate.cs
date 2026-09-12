using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.User
{
    public class UserInUpdate
    {
        [Required]
        [MinLength(5, ErrorMessage = "First name must be at least 5 character long.")]
        [MaxLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        public required string FirstName { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Last name must be at least 5 character long.")]
        [MaxLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
        public required string LastName { get; set; }
        
        [Required]
        [MinLength(5, ErrorMessage = "Name must be at least 5 character long.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public required string UserName { get; set; }

        //will add password change in seperate operation
        //public required string Password { get; set; }
        [Required]
        [MinLength(11, ErrorMessage = "Phone Number must be at least 11 character long.")]
        [MaxLength(11, ErrorMessage = "Phone Number cannot exceed 11 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}