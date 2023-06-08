using System;
using PersonalBlog.Models;

namespace PersonalBlog.Data.ViewModels
{
	public class PageWithPagination<T>
    {
        public IEnumerable<T> List { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }

    }
}

