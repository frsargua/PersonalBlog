using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Data.Services;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _service;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(UserManager<AppUser> userManager, ICommentService service)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

         
        //POST: /<controller>/
        [HttpPost]
        public async Task<IActionResult> Create(SinglePostLoggedIn comment)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userId = _userManager.GetUserId(User);

            var newComment = new Comment();


            newComment.DateCreated = DateTime.Now;
            newComment.AppUserId = userId;
            newComment.PostId = Int32.Parse(comment.newCommentVM.PostId);
            newComment.CommentText = comment.newCommentVM.CommentText;

            await _service.AddNewCommentAsync(newComment);
            return RedirectToAction("SinglePost", "Post", new { id = comment.newCommentVM.PostId });
        }
    }
}

