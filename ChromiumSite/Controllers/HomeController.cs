using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChromiumSite.Models;
using System.Drawing;
using ChromiumSite.Data;
using ChromiumSite.Models.AdminViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using NuGet;

namespace ChromiumSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Index()
        {
            List<HomeBlog> home = new List<HomeBlog>();
            home = _db.HomeBlogs.Select(u => u).ToList();
            return View(home);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Gallery()
        {
            List<GalleryImageViewModel> model = new List<GalleryImageViewModel>();
            model = _db.GalleryImages.Select(u => new GalleryImageViewModel
            {
                Path = u.PathToImage
            }).ToList();
            ViewData["Message"] = "Our works";
            return View(model);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
    }
}
