using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using PersonalBlog.Data;
using PersonalBlog.Data.Base;

namespace PersonalBlog.Models
{
    public class Post : IEntityBase
    {
        public int Id { get; set; }

        [Display(Name = "Date Created")]
        [Required(ErrorMessage = "Date created is required")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Post Title")]
        [Required(ErrorMessage = "Post title is required")]
        public string PostTitle { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string PostText { get; set; }


        public PostCategory PostCategory { get; set; }

        //Relationships

        //PostOwner
        //public int ApplicationUserID { get; set; }
        //[ForeignKey("ApplicationUserID")]
        //public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


        public ICollection<Comment>? Comment { get; set; }



    
    }
}

