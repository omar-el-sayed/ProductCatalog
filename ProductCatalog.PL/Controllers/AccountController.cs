using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.BL.Consts;
using ProductCatalog.BL.DTOs;
using ProductCatalog.DAL.Models;
using ProductCatalog.PL.Helpers;

namespace ProductCatalog.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        #region Constructor
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Actions
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new()
                    {
                        FullName = model.Name,
                        UserName = model.Email,
                        Email = model.Email,
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, Privilege.User);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.LogErrorInAFile(ex, "Register Page");
            }
            return View();
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, true);

                    if (result.Succeeded)
                    {
                        var loggedInUser = await _userManager.FindByEmailAsync(model.Email);
                        if (loggedInUser is not null)
                        {
                            if(await _userManager.IsInRoleAsync(loggedInUser, Privilege.Admin))
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Product");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Email Or Password");
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.LogErrorInAFile(ex, "Login Page");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //public IActionResult ForgetPassword() => View();

        //[HttpPost]
        //public async Task<IActionResult> ForgetPassword(ForgetPasswordDto model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var user = await _userManager.FindByEmailAsync(model.Email);

        //            if (user is not null)
        //            {
        //                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //                var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

        //                MailSender.Mail("Password Reset", passwordResetLink);

        //                //logger.Log(LogLevel.Warning, passwordResetLink);

        //                return RedirectToAction("ConfirmForgetPassword");
        //            }

        //            return RedirectToAction("ConfirmForgetPassword");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError.LogErrorInAFile(ex, "Forget Password Page");
        //    }
        //    return View();
        //}
        #endregion
    }
}
