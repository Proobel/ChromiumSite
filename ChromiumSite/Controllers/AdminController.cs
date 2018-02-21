using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChromiumSite.Data;
using ChromiumSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ChromiumSite.Controllers
{
    public class AdminController : Controller
    {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private ApplicationDbContext _db;


            public AdminController(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                RoleManager<IdentityRole> roleManager,
                ApplicationDbContext db)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _roleManager = roleManager;
                _db = db;
            }

            [HttpGet]
            public IActionResult Index()
            {
                return View();
            }
    }
}
