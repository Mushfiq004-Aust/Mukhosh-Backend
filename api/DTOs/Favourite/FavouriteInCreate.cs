using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Favourite
{
    public class FavouriteInCreate
    {
        [Required]
        public string? UserId { get; set; }
        [Required]
        public Guid PostId { get; set; }
    }
}