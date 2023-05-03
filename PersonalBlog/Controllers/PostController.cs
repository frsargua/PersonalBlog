using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using PersonalBlog.Data.Services;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalBlog.Controllers
{
    public class PostController : Controller
    {

        private readonly IPostService _service;
        private readonly UserManager<ApplicationUser> _userManager;


        public PostController(UserManager<ApplicationUser> userManager, IPostService service)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult Create()
        {
            return View();
        }

        // GET: /<controller>/
        public async Task<IActionResult> SinglePost(int id)
        {
            var singlePost = await _service.GetByIdAsync(id);

            return View(singlePost);
        }

        // GET: /<controller>/
        public async Task<IActionResult> Edit(int id)
        {
            var singlePost = await _service.GetByIdAsync(id);

            return View(singlePost);
        }

        //POST: /<controller>/
        [HttpPost]
        public async Task<IActionResult> Create(NewPostVM post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userId = _userManager.GetUserId(User);
            await _service.AddNewPostAsync(post, userId);
            return RedirectToAction(nameof(Index));
        }

        //POST: /<controller>/
        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _service.UpdateAsync(  post.Id ,post);
            return RedirectToAction(nameof(Index));
        }

        //POST: /<controller>/
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index),"Home");
        }
    }
}

