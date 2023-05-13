using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using MySql.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PersonalBlog.Models;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PersonalBlog.Data
{
	public class AppDbContext:IdentityDbContext<AppUser>
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration):base(options)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
            .HasMany(c => c.Posts)
            .WithOne(e => e.AppUser).HasForeignKey(p => p.AppUserId);
            ;

            modelBuilder.Entity<AppUser>()
            .HasMany(c => c.Comments)
            .WithOne(e => e.AppUser).HasForeignKey(p => p.AppUserId);
            ;

            //modelBuilder.Entity<Post>()
            //.HasMany(c => c.Comment)
            //.WithOne(e => e.Post);
        }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

    }
}

