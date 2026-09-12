using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Favourite;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Mappers
{
    public static class FavouriteMapper
    {
        public static Favourite ToFavouriteCreate(this FavouriteInCreate favourite)
        {
            return new Favourite
            {
                UserId = favourite.UserId,
                PostId = favourite.PostId
            };
        }
    }
}