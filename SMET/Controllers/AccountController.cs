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
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

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
        
        public IActionResult RegisterOrLogin()
        {
            var model = TempData.Get<LoginOrRegisterViewModel>("user");
            if ( model != null)
            {
                var result = TempData.Get<IdentityResult>("ModelState");
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt...");
                }
                else
                {
                    AddErrors(result);
                }
                
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginOrRegisterViewModel lr)
        {
            if (ModelState.IsValid)
            {
                LoginViewModel model = lr.LoginViewModel;
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    //_logger.LogInformation("User logged in.");
                    return RedirectToAction(nameof(Index));
                }
                if (result.RequiresTwoFactor)
                {
                   // return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    //_logger.LogWarning("User account locked out.");
                    // return RedirectToAction(nameof(Lockout));
                }
            }

            TempData.Put("user", lr);
            return RedirectToAction(nameof(RegisterOrLogin));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginOrRegisterViewModel lr)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            IdentityResult result = null;
            if (ModelState.IsValid)
            {
                RegisterViewModel model = lr.RegisterViewModel;
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
            
            TempData.Put("user", lr);
            TempData.Put("ModelState", result);
            // If we got this far, something failed, redisplay form
            return RedirectToAction(nameof(RegisterOrLogin));
        }

        private void AddErrors(IdentityResult result)
        {
            if (result !=null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

    }
}