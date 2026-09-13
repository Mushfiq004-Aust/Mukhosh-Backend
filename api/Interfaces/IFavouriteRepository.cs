using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Favourite;
using api.Models;

namespace api.Interfaces
{
    public interface IFavouriteRepository
    {
        Task<List<Post>> GetFavourites(User user);
        Task<Favourite> CreateFavourite(FavouriteInCreate favourite);

        Task<Favourite?> DeleteFavourite(string userId, Guid postId);

        Task<bool> FavouriteExistsAsync(string userId, Guid postId);
    }
}