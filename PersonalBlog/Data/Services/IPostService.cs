using System;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.Data.Base;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;

namespace PersonalBlog.Data.Services
{
    public interface IPostService : IEntityBaseRepository<Post>
    {
        public Task AddNewPostAsync(Post data);
        public Task<IEnumerable<Post>> GetAllByUserId(string userId);
        public Task<IEnumerable<Post>> GetAllByCategory(int categoryId);

}
}

