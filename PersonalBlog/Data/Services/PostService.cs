using System;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data.Base;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;

namespace PersonalBlog.Data.Services
{
    public class PostService : EntityBaseRepository<Post>, IPostService
    {

        private readonly AppDbContext _context;

        public PostService(AppDbContext context) : base(context){
            _context = context;

        }

        public async Task AddNewPostAsync(NewPostVM data, string owner)
        {
            //create object
            var newMovie = new Post()
            {
                ApplicationUserId = owner,
                DateCreated = DateTime.Now.ToString("dd/MM/yyyy"),
                PostTitle = data.PostTitle,
                PostText = data.PostText
            };

            await _context.Posts.AddAsync(newMovie);
            await _context.SaveChangesAsync();
        }



    }
}

