using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.User
{
    public class UserInUpdate
    {
        public required string Name { get; set; }

        //will add password change in seperate operation
        //public required string Password { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}