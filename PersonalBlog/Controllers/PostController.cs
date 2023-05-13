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
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalBlog.Controllers
{
    public class PostController : Controller
    {

        private readonly IPostService _service;
        private readonly UserManager<AppUser> _userManager;


        public PostController(UserManager<AppUser> userManager, IPostService service)
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
            var singlePost = await _service.GetByIdAsync(id, o => o.Comment);

            var combinedViewModel = new SinglePostLoggedIn
            {
                post = singlePost,
            };

            ViewBag.UserId = _userManager.GetUserId(User);


            return View(combinedViewModel);
        }

        // GET: /<controller>/
        public async Task<IActionResult> PostsByCategory(int id)
        {
            var allPosts = await _service.GetAllByCategory(id);

            return View(allPosts);
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
            var currentUser = await _userManager.GetUserAsync(User);

            if (_userManager == null)
            {
                throw new Exception("_userManager is null");
            }

            if (User == null)
            {
                throw new Exception("User is null");
            }

            var newPost = new Post();

            newPost.AppUserId = userId;
            newPost.DateCreated = DateTime.Now;
            newPost.PostTitle = post.PostTitle;
            newPost.PostText = post.PostText;
            newPost.PostCategory = post.PostCategory;

            await _service.AddNewPostAsync(newPost);
            return RedirectToAction("Index","Home");
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
            return RedirectToAction("SinglePost","Post", new { id = post.Id });
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

