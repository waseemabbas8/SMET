using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMET.Data;
using SMET.Helpers;
using SMET.Models;

namespace SMET.Controllers
{
    public class PostController : Controller
    {
        readonly ApplicationDbContext db;

        public PostController(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult BooksNotes()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult CreateUpdatePost()
        {
            ViewBag.ListOfCategories = db.PostCategory.ToList<PostCategory>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CreateUpdatePost(Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedDate = DateTime.Now;
                //ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
                //postCategory.CreatedBy = user.UserName;
                await db.Post.AddAsync(post);
                await db.SaveChangesAsync();
                ModelState.Clear();
                TempData["SuccessMessage"] = AppConstants.MSG_SAVED_SUCCESS;
                return RedirectToAction(nameof(CreateUpdatePost));
            }
            ViewBag.MessageError = AppConstants.MSG_SAVE_ERROR;
            return View(post);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManagePostCategory(int? id)
        {
            ViewBag.MessageSuccess = TempData["SuccessMessage"];
            var oListPostCategory = db.PostCategory.ToList<PostCategory>();
            ViewBag.ListOfCategories = oListPostCategory;
            if (id.HasValue)
            {
                return View(oListPostCategory.Where(m => m.Id.Equals(id)).FirstOrDefault());
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ManagePostCategory(PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                postCategory.CreatedDate = DateTime.Now;
                //ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
                //postCategory.CreatedBy = user.UserName;
                await db.PostCategory.AddAsync(postCategory);
                await db.SaveChangesAsync();
                ModelState.Clear();
                TempData["SuccessMessage"] = AppConstants.MSG_SAVED_SUCCESS;
                return RedirectToAction(nameof(ManagePostCategory));
            }
            ViewBag.MessageError = AppConstants.MSG_SAVE_ERROR;
            return View(postCategory);
        }
        
    }
}