using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Favourite
    {
        public string UserId { get; set; }
        public string PostId { get; set; }

        public User User { get; set; }

        public Post Post { get; set; }
    }
}