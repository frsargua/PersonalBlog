using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Models
{
	public class AppUser: IdentityUser
    {
        [Display(Name = "full name")]
        public string FullName { get; set; }

        private string imageUrl;

        public string ImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(imageUrl) ? "https://cdn-icons-png.flaticon.com/512/6596/6596121.png" : imageUrl;
            }
            set
            {
                imageUrl = value;
            }
        }

        // Navigation Property

        public virtual ICollection<Post> Posts { get; set; }

        // Navigation Property

        public virtual ICollection<Comment> Comments { get; set; }

    }
}

