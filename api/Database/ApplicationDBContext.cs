using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//import the models
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }


        //override the OnModelCreating method to configure the model
        //Making sure that the email column in the user table is unique, so that no two users can have the same email address.
        protected override void OnModelCreating(ModelBuilder modelBuilder) // for unique constraint on email column in user table
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        //DbSet properties for each model class, representing the tables in the database
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}