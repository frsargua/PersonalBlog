using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PersonalBlog.Models
{
	public class Comment
	{
            [Key]
            public int Id { get; set; }


            [Display(Name = "Description")]
            [Required(ErrorMessage = "Description is required")]
            public string DateCreated { get; set; }

            [Display(Name = "Comment Text")]
            [Required(ErrorMessage = "Comment text is required")]
            public string CommentText { get; set; }

            //Relationships

            //CommentOwner
            [Display(Name = "Comment Owner")]
            [Required(ErrorMessage = "Comment must have an owner")]
            public int ApplicationUserId { get; set; }
            public ApplicationUser CommentOwner { get; set; }

            //PostOrigin
            [Display(Name = "Post Origin")]
            [Required(ErrorMessage = "Post must have an origin")]
            public int PostId { get; set; }
            public Post CommentPostId { get; set; }
    }
}

