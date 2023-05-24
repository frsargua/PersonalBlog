using System;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data.Base;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;

namespace PersonalBlog.Data.Services
{
    public class PostService : EntityBaseRepository<Post>, IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewPostAsync(Post data)
        {
            if (_context != null)
            {
                await _context.Posts.AddAsync(data);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException("_context object is null");
            }
        }

        public async Task<IEnumerable<Post>> GetAllByUserId(string userId)
        {
            var result = _context.Posts.Where(n => n.AppUserId == userId).Include(n=>n.AppUser).ToList();

            return result;
        }

        public async Task<IEnumerable<Post>> GetAllByCategory(int categoryId)
        {
            var result = _context.Posts.Where(n => (int)n.PostCategory == categoryId).Include(n => n.AppUser).ToList();

            return result;
        }

    }
}

