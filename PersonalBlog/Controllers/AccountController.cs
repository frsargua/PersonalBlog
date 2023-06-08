using eTickets.Data.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Data.Services;
using PersonalBlog.Data.ViewModels;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPostService _service;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context, IPostService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _service = service;
        }

        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }

        public IActionResult Profile()
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.Users
                .Include(u => u.Posts)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Login");
            }

            var profileData = new UpdateUserVM { User = user };
            return View(profileData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserVM model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Login");
            }

            user.UserName = model.UserName;
            user.Email = model.Email;

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Failed to update user.";
                return RedirectToAction("Profile");
            }

            TempData["Success"] = "Profile updated successfully.";
            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Dashboard(string id, int page = 1)
        {
            var posts = await _service.GetAllByUserId(id, page);
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Login");
            }

            TempData["Id"] = userId;
            return View(posts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddres);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }

            TempData["Error"] = "Wrong credentials. Please try again.";
            return View(loginVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
                return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email address is already in use.";
                return View(registerVM);
            }

            var newUser = new AppUser
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegistrationCompleted");
            }

            TempData["Error"] = "Failed to register user.";
            return View(registerVM);
        }
    }
}
