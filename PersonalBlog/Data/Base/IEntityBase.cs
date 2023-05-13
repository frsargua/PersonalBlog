using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Data.Base
{
	public interface IEntityBase
	{
        [Key]
        int Id { set; get; }
    }
}

