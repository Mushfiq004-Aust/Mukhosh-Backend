using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//import the models
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Database
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Favourite>(x => x.HasKey(f => new { f.UserId, f.PostId }));

            modelBuilder.Entity<Favourite>()
            .HasOne(u => u.User)
            .WithMany(u => u.Favourite)
            .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Favourite>()
            .HasOne(u => u.Post)
            .WithMany(u => u.Favourite)
            .HasForeignKey(u => u.PostId);


            modelBuilder.Entity<Review>()
                .HasIndex(r => new { r.UserId, r.UniversityId })
                .IsUnique();

            modelBuilder.Entity<Review>()
                .HasOne(r => r.University)
                .WithMany(u => u.Review)
                .HasForeignKey(r => r.UniversityId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },

            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }


        //DbSet properties for each model class, representing the tables in the database
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Favourite> Favourite { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<Review> Review { get; set; }
    }
}