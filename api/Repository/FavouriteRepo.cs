using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.DTOs.Favourite;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FavouriteRepo : IFavouriteRepository
    {
        private readonly ApplicationDBContext _context;
        public FavouriteRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Post>> GetFavourites(User user)
        {
            return await _context.Favourite
                .Where(f => f.UserId == user.Id)
                .Select(f => f.Post)
                .ToListAsync();
        }

        public async Task<Favourite> CreateFavourite(FavouriteInCreate favourite)
        {
            var newFavourite = favourite.ToFavouriteCreate();
            await _context.Favourite.AddAsync(newFavourite);
            await _context.SaveChangesAsync();
            return newFavourite;
        }

        public async Task<Favourite?> DeleteFavourite(string userId, Guid postId)
        {
            var favourite = await _context.Favourite
                .FirstOrDefaultAsync(f =>
                    f.UserId == userId &&
                    f.PostId == postId);

            if (favourite == null)
            {
                return null;
            }

            _context.Favourite.Remove(favourite);
            await _context.SaveChangesAsync();

            return favourite;
        }
    }
}