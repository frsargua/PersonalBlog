using System;
using PersonalBlog.Models;

namespace PersonalBlog.Data.ViewModels
{
    public class UpdateUserVM
	{
        public string Email { get; set; }
        public string UserName { get; set; }
        public AppUser? User { get; set; }

    }
}

