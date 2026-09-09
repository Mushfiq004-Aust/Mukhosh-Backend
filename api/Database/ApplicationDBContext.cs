using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        // all the tables in the database will be created based on the models defined in the api/Models folder.
        public DbSet<User> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}