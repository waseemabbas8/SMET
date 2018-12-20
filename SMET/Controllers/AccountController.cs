using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SMET.Helpers;
using SMET.Models;
using SMET.Models.AccountViewModels;

namespace SMET.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View("RegisterOr");
        }
        
        public IActionResult RegisterOrLogin()
        {
            var model = TempData.Get<RegisterViewModel>("user");
            if ( model != null)
            {
                var result = TempData.Get<IdentityResult>("ModelState");
                AddErrors(result);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            IdentityResult result = null;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation("User created a new account with password.");
                    return RedirectToAction(nameof(AccountController.Index));
                }
                //AddErrors(result);
            }
            
            TempData.Put("user", model);
            TempData.Put("ModelState", result);
            // If we got this far, something failed, redisplay form
            return RedirectToAction(nameof(AccountController.RegisterOrLogin));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }
}