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

        public async Task<SinglePostLoggedIn> GetByIdAsync(int id, int skip, int take, params Expression<Func<Post, object>>[] includeProperties)
        {

            //var query = _context.Posts.Where(o => o.Id == id);
            //query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(c => c.AppUser).Include(c => c.Comment).ThenInclude(pc => pc.AppUser));
            //query = query.Skip(skip).Take(take);
            //var result = await query.FirstOrDefaultAsync();

            var post = await _context.Posts
             .Where(p => p.Id == id)
             .Include(p => p.AppUser)
             .FirstOrDefaultAsync();

            var comments = await _context.Comments
            .Where(c => c.PostId == id)
            .Include(c => c.AppUser)
            .OrderByDescending(c => c.DateCreated)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

            var result = new SinglePostLoggedIn
            {
                post = post,
                Comments = comments
            };

            return result;
        }
    }
}

