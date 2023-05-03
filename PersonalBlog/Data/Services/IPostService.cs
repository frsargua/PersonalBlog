using System;
using PersonalBlog.Data.Base;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;

namespace PersonalBlog.Data.Services
{
    public interface IPostService : IEntityBaseRepository<Post>
    {
        public Task AddNewPostAsync(NewPostVM data, string owner);
        public Task<IEnumerable<Post>> GetAllByIdAsync(int id);

}
}

