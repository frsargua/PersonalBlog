using System;
using PersonalBlog.Models;

namespace PersonalBlog.Data.ViewModels
{
	public class SinglePostLoggedIn
	{
        public Post? post { get; set; }
            public List<Comment>? Comments { get; set; }
        public NewCommentVM? newCommentVM { get; set; }

    }
}

