using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using PersonalBlog.Data.Base;

namespace PersonalBlog.Models
{
	public class Post: IEntityBase
    {
            [Key]
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
    
            //Relationships

            //PostOwner
        //[ForeignKey("ApplicationUserId")]
            public string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

            
    }
}

