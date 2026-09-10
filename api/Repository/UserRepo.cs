using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.Models;
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

        public Task<List<User>> GetAllUsers()
        {
            return _context.User.ToListAsync();
        }
    }
}