using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.Data.Base;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;

namespace PersonalBlog.Data.Services
{
    public interface IPostService : IEntityBaseRepository<Post>
    {
        public Task AddNewPostAsync(Post data);
        public Task<PageWithPagination<Post>> GetAllByUserId(string userId, int pageNumer);
        public Task<PageWithPagination<Post>> GetAllByCategory(int categoryId, int pageNumer);
        Task<SinglePostLoggedIn> GetByIdAsync(int id, int skip, int take, params Expression<Func<Post, object>>[] includeProperties);

    }
}

