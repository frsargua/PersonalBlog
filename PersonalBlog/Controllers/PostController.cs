using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalBlog.Data.Services;
using PersonalBlog.Data.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalBlog.Controllers
{
    public class PostController : Controller
    {

        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //POST: /<controller>/
        [HttpPost]
        public async Task<IActionResult> Create(NewPostVM post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _service.AddNewPostAsync(post, ClaimTypes.NameIdentifier);
            return RedirectToAction(nameof(Index));
        }
    }
}

