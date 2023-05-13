using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using PersonalBlog.Data.Base;

namespace PersonalBlog.Models
{
	public class Comment: IEntityBase
    {
            [Key]
            public int Id { get; set; }


            [Display(Name = "Description")]
            [Required(ErrorMessage = "Description is required")]
            public DateTime DateCreated { get; set; }

            [Display(Name = "Comment Text")]
            [Required(ErrorMessage = "Comment text is required")]
            public string CommentText { get; set; }

        //Relationships

        //CommentOwner
        //[Display(Name = "Comment Owner")]
        //[Required(ErrorMessage = "Comment must have an owner")]
        //public int ApplicationUserId { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        //PostOrigin
        //[Display(Name = "Post Origin")]
        //[Required(ErrorMessage = "Post must have an origin")]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

