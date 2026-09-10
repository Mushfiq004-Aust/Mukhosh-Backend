using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.DTOs.User;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

// In the UserRepo class, we are implementing the IUserRepository interface and providing the implementation methods of the interface
namespace api.Repository
{
    public class UserRepo : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
             return await _context.User.FindAsync(id);
        }

        public async Task<User> CreateUserAsync(UserInCreate user)
        {
            var newUser = user.ToUserInCreate(); //map the request body to the User model using the mapper class
            await _context.User.AddAsync(newUser); // add the user to the database context
            await _context.SaveChangesAsync(); // save the changes to the database
            return newUser;
        }

        public async Task<User?> UpdateUserAsync(Guid id, UserInUpdate user)
        {
            var existingUser = await _context.User.FindAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.Name = user.Name;
            existingUser.Phone = user.Phone;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User?> DeleteUserAsync(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return null;
            }

             _context.User.Remove(user);
            // remove is not async, because it only marks the entity for deletion, and does not actually delete it from the database until SaveChangesAsync is called.
            await _context.SaveChangesAsync();

            return user; // returning the user instead of null
        }
    }
}