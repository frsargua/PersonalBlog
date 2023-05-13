using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Data.Services;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IPostService _post;

    public HomeController(ILogger<HomeController> logger, IPostService postService)
    {
        _logger = logger;
        _post = postService;
    }

    public async Task<IActionResult> Index()
    {
        return View();

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

