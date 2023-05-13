using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Models
{
	public class AppUser: IdentityUser
    {
        [Display(Name = "full name")]
        public string FullName { get; set; }


        // Navigation Property

        public virtual ICollection<Post> Posts { get; set; }

        // Navigation Property

        public virtual ICollection<Comment> Comments { get; set; }

    }
}

