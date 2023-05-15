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
            _context = context;
        }

        public async Task AddNewCommentAsync(Comment newComment)
        {

            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();
        }


        public int GetCommentCountForPost(int id)
        {

            return _context.Comments.Where(n=> n.AppUserId==id.ToString()).Count();
        }
    }
}

