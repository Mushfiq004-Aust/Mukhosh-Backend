using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Favourites")]
    public class Favourite
    {
        public string UserId { get; set; }
        public Guid PostId { get; set; }

        public User User { get; set; }

        public Post Post { get; set; }
    }
}