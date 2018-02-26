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
using ChromiumSite.Models.AdminViewModels;
using Microsoft.AspNetCore.Http;
using ChromiumSite.Services;

namespace ChromiumSite.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IFileWorker _fileWorker;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db,
            IFileWorker fileWorker
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
            _fileWorker = fileWorker;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AquaProposalModel> Aqua = _db.AquaProposalModels.Select(y => y).ToList();
            //Aqua.ForEach(async y => y.User = await _userManager.FindByIdAsync(y.UserId).ConfigureAwait(true));
            foreach(var aq in Aqua)
            {
                aq.User =await _userManager.FindByIdAsync(aq.UserId);
            }
            ViewBag.AquaPropositions = Aqua;

            List<ChromeProposalModel> Chrome = _db.ChromeProposalModels.Select(x => x).ToList();
            //Chrome.ForEach(async x => x.User = await _userManager.FindByIdAsync(x.UserId).ConfigureAwait(true));
            foreach (var ch in Chrome)
            {
                ch.User = await _userManager.FindByIdAsync(ch.UserId);
            }
            ViewBag.ChromePropositions = Chrome;

            return View();
        }

        [HttpGet]
        public IActionResult UserChanges()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            model = _userManager.Users.Select(u => new UserListViewModel
            {
                Id = u.Id,
                Name = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).ToList();
            return View(model);
        }

    [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();
            model.ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.Name = user.UserName;
                    model.Email = user.Email;
                    model.ApplicationRoleId = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }
            return PartialView("_EditUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.UserName = model.Name;
                    user.Email = model.Email;
                    string existingRole = _userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = _roleManager.Roles.Single(r => r.Name == existingRole).Id;
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (existingRoleId != model.ApplicationRoleId)
                        {
                            IdentityResult roleResult = await _userManager.RemoveFromRoleAsync(user, existingRole);
                            if (roleResult.Succeeded)
                            {
                                var applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                                if (applicationRole != null)
                                {
                                    IdentityResult newRoleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                                    if (newRoleResult.Succeeded)
                                    {
                                        return RedirectToAction("UserChanges");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return PartialView("_EditUser", model);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUserForm(string id)
        {
            var delete = new DeleteUserViewModel();
            delete.Id = id;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    delete.Name = applicationUser.UserName;
                }
            }
            return PartialView("_DeleteUser", delete);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(DeleteUserViewModel deleteUser)
        {

            if (!String.IsNullOrEmpty(deleteUser.Id))
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(deleteUser.Id);
                if (applicationUser != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(UserChanges));
                    }
                }
            }
            return RedirectToAction(nameof(UserChanges));
        }

        [HttpGet]
        public IActionResult GalleryManage()
        {
            List<GalleryImageViewModel>model = new List<GalleryImageViewModel>();
            model = _db.GalleryImages.Select(u => new GalleryImageViewModel
            {
                Id= u.Id,
                Path = u.PathToImage
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            foreach (var imageFile in Request.Form.Files)
            {
                var Image = new GalleryImage()
                {
                    PathToImage = await _fileWorker.SaveImgAsync("/images/Gallery/", imageFile)
                };
                _db.GalleryImages.Add(Image);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(GalleryManage));
        }

        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int Id)
        {
            var galleryImage = await _db.GalleryImages.Where(x => x.Id == Id).FirstAsync();
                if (galleryImage != null)
                {
                _db.GalleryImages.Remove(galleryImage);
                _db.SaveChanges();
                }
            return RedirectToAction(nameof(GalleryManage));
        }

        [HttpGet]
        public IActionResult ChangeStatus(string id,string status,string model)
        {
            if (model == "chrome")
            {
                var chrome = _db.ChromeProposalModels.Where(x => x.Id == int.Parse(id)).First();
                chrome.Status = status;
                _db.ChromeProposalModels.Update(chrome);
            }
            else
            {
                var aqua = _db.AquaProposalModels.Where(x => x.Id == int.Parse(id)).First();
                aqua.Status = status;
                _db.AquaProposalModels.Update(aqua);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DeleteProposition(int id,string model)
        {
            if (model == "chrome")
            {
                var chrome = _db.ChromeProposalModels.Where(x => x.Id == id).First();
                _db.ChromeProposalModels.Remove(chrome);
            }
            else
            {
                var aqua = _db.AquaProposalModels.Where(x => x.Id == id).First();
                _db.AquaProposalModels.Remove(aqua);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ManageAquaprint()
        {
            List<AquaImageViewModel> model = new List<AquaImageViewModel>();
            model = _db.AquaImages.Select(u => new AquaImageViewModel
            {
                Id = u.Id,
                Path = u.PathToImage,
                Is_Template = u.Is_Template
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangeTemplate(int Id)
        {
            var model = _db.AquaImages.Where(x => x.Id == Id).First();
            if (model.Is_Template) { model.Is_Template = false; } else { model.Is_Template = true; }
            _db.AquaImages.Update(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageAquaprint));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTemplate(int Id)
        {
            var aquaImage = await _db.AquaImages.Where(x => x.Id == Id).FirstAsync();
            if (aquaImage != null)
            {
                _db.AquaImages.Remove(aquaImage);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAquaprint));
        }

        [HttpPost]
        public async Task<IActionResult> UploadTemplates(AquaImageViewModel aqua)
        {
            foreach (var imageFile in Request.Form.Files)
            {
                var Image = new AquaImage()
                {
                    Is_Template = true,
                    PathToImage = await _fileWorker.SaveImgAsync("/images/aquatemplates/", imageFile)
                };
                _db.AquaImages.Add(Image);
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(ManageAquaprint));
        }

        [HttpGet]
        public IActionResult HomeNews()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(HomeBlogViewModel home)
        {
            var user = await _userManager.GetUserAsync(User);
            var Home = new HomeBlog()
            {
                Title=home.Title,
                Content=home.Content,
                ImagePath= await _fileWorker.SaveImgAsync("/images/Blog/", home.File),
                User = user ?? throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'."),
                UserId = user.Id
        };
            _db.HomeBlogs.Add(Home);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(HomeNews));
        }
    }
}
