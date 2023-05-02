using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PersonalBlog.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddres { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

