using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace PersonalBlog.Data.ViewModels
{
	public class NewPostVM
	{
        [Display(Name = "Post Title")]
        [Required(ErrorMessage = "Post title is required")]
        public string PostTitle { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string PostText { get; set; }

        public PostCategory PostCategory { get; set; }

    }
}

