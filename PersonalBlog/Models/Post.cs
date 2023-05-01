using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PersonalBlog.Models
{
	public class Post
	{
            [Key]
            public int Id { get; set; }

            [Display(Name = "Date Created")]
            [Required(ErrorMessage = "Date created is required")]
            [DataType(DataType.Date)]   
            public string DateCreated { get; set; }

            [Display(Name = "Post Title")]
            [Required(ErrorMessage = "Post title is required")]
            public string PostTitle { get; set; }

            [Display(Name = "Description")]
            [Required(ErrorMessage = "Description is required")]
            public string PostText { get; set; }
    
            //Relationships

            //PostOwner
            [Display(Name = "Comment Owner")]
            [Required(ErrorMessage = "Comment must have an owner")]
            public int ApplicationUserId { get; set; }
            public ApplicationUser CommentOwner { get; set; }

            
    }
}

