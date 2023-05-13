using System;
using PersonalBlog.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PersonalBlog.Data.ViewModels
{
	public class NewCommentVM
	{

        [Display(Name = "Comment Text")]
        [Required(ErrorMessage = "Comment text is required")]
        public string CommentText { get; set; }

        public string PostId { get; set; }

    }
}

