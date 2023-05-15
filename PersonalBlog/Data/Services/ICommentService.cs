using System;
using PersonalBlog.Data.Base;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;

namespace PersonalBlog.Data.Services
{
	public interface ICommentService : IEntityBaseRepository<Comment>
    {
        public Task AddNewCommentAsync(Comment newComment);
        public int GetCommentCountForPost(int id);

    }
}

