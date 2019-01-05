using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMET.Data;
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
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CreateUpdatePost(Post post)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManagePostCategory()
        {
            ViewBag.ListOfCategories = db.PostCategory.ToList<PostCategory>();
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
                RedirectToAction(nameof(ManagePostCategory));
            }
            return View(postCategory);
        }
    }
}