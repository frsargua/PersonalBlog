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
        private readonly int pageSize = 10;

        public PostService(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

        public async Task<PageWithPagination<Post>> GetAllByUserId(string userId, int pageNumber)
        {
            var query = _context.Posts.Where(n => n.AppUserId == userId).Include(n => n.AppUser);

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var result = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var newPageData = new PageWithPagination<Post>
            {
                CurrentPage = pageNumber,
                List = result,
                TotalPages = totalPages,
                ActionName = "Dashboard",
                Controller = "Account"
            };

            return newPageData;
        }

        public async Task<PageWithPagination<Post>> GetAllByCategory(int categoryId, int pageNumber)
        {
            var query = _context.Posts.Where(n => (int)n.PostCategory == categoryId).Include(n => n.AppUser);

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var result = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var newPageData = new PageWithPagination<Post>
            {
                ActionName = "PostsByCategory",
                Controller = "Post",
                CurrentPage = pageNumber,
                List = result,
                TotalPages = totalPages
            };

            return newPageData;
        }

        public async Task<SinglePostLoggedIn> GetByIdAsync(int id, int skip, int take, params Expression<Func<Post, object>>[] includeProperties)
        {
            var post = await _context.Posts
                .Where(p => p.Id == id)
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new ArgumentException("Post not found.");
            }

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
