using System;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data.Base;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;

namespace PersonalBlog.Data.Services
{
    public class CommentService : EntityBaseRepository<Comment>, ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddNewCommentAsync(Comment newComment)
        {
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();
        }

        public int GetCommentCountForPost(string id)
        {
            var count = _context.Comments.Count(n => n.AppUserId == id);
            return count;
        }
    }
}
