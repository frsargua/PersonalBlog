using System;
using PersonalBlog.Models;

namespace PersonalBlog.Data.ViewModels
{
    public class PaginationModel
    {
        public List<Comment> Comments { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

