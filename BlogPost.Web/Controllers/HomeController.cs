using BlogPost.Web.Models;
using BlogPost.Web.Models.ViewModels;
using BlogPost.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogPost.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPageRepository blogPage;
        private readonly ITagRepository tag;

        public HomeController(ILogger<HomeController> logger,IBlogPageRepository blogPage,ITagRepository tag)
        {
            _logger = logger;
            this.blogPage = blogPage;
            this.tag = tag;
        }

        public async Task<IActionResult> Index()
        {
            var blog = await blogPage.GetAllAsync();
            var tags = await tag.GetAllAsync();
            var model = new HomePageRequest
            {
                Blog = blog,
                Tags = tags
            };
            return View(model);
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
}