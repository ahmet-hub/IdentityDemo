using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetIdentityProject.Helper;
using AspNetIdentityProject.Models;
using AspNetIdentityProject.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentityProject.Controllers
{
    public class HomeController : Controller
    {
        UserManager<AppUser> _userManager { get; }
        SignInManager<AppUser> _signInManager { get; }
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel loginUser)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(loginUser.Email);

                if (user != null)
                {

                    var deneme = await _userManager.IsLockedOutAsync(user);

                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "Hesap bir süreliğine engellenmiştir. Daha sonra tekrar deneyiniz.");
                        return View(loginUser);
                    }
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.RememberMe, false);

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);

                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return RedirectToAction("Index", "Member");
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);
                        var failCount = await _userManager.GetAccessFailedCountAsync(user);

                        if (await _userManager.GetAccessFailedCountAsync(user) > 2)
                        {
                            ModelState.AddModelError("", " Hesap bir süreliğine engellenmiştir.Daha sonra tekrar deneyiniz.");
                            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddMinutes(20));
                            return View(loginUser);
                        }

                        ModelState.AddModelError("", $"Kalan Deneme hakkı : {3 - failCount}");
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");

                        return View(loginUser);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                    return View(loginUser);
                }
            }
            else
            {

                return View(loginUser);
            }
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel registerUser)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                {
                    user.UserName = registerUser.UserName;
                    user.Email = registerUser.Email;
                    user.PhoneNumber = registerUser.PhoneNumber;

                }
                IdentityResult result = await _userManager.CreateAsync(user, registerUser.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerUser);
        }

        public IActionResult SendPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendPassword(SendPasswordViewModel sendPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(sendPasswordViewModel.Email);

                if (user != null)
                {
                    string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                    string passwordResetLink = Url.Action("ResetPasswordConfirm", "Home", new
                    {
                        userId = user.Id,
                        token = passwordResetToken
                    }, HttpContext.Request.Scheme);

                    PasswordReset.PasswordResetSendEmail(link: passwordResetLink, emailAddress: sendPasswordViewModel.Email);
                    ViewBag.status = "Başarılı";
                    
                   
                }
                else
                {
                    ModelState.AddModelError("", "Kayıtlı email adresi bulunamamıştır.");
                    return View(sendPasswordViewModel);
                }
            }
            else
            {
                ModelState.AddModelError("", "Kayıtlı email adresi bulunamamıştır.");
                return View(sendPasswordViewModel);
            }
            return View(sendPasswordViewModel);
        }

        public IActionResult ResetPasswordConfirm(string userId,string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm( ResetPasswordViewModel resetPasswordViewModel)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();

            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, resetPasswordViewModel.NewPassword);

                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    TempData["passwordResetInfo"] = "Şifreniz başarıyla yenilenmiştir.";
                }
                else
                {
                    foreach (var item in result.Errors) 
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Bir hata meydana gelmiştir.");
            }
            return View();
        }
    }
}


