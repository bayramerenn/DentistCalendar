using DentistCalendar.Data.Entity;
using DentistCalendar.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DentistCalendar.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Kullanıcı veya şifre hatalı");
                        return View(loginViewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı");
                    return View(loginViewModel);
                }
            }
            ModelState.AddModelError("", "Lütfen bilgilerini doldurunuz");
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = model.Adapt<AppUser>();

                var result = _userManager.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {
                    bool roleCheck = model.IsDentist ? AddRole("Dentist").Result : AddRole("Secratary").Result;

                    if (!roleCheck)
                    {
                        return View("Error");
                    }

                    _userManager.AddToRoleAsync(user, user.IsDentist ? "Dentist" : "Secratary").Wait();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Login");
        }

        public IActionResult Denied()
        {
            return View();
        }

        private async Task<bool> AddRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                AppRole role = new AppRole
                {
                    Name = roleName
                };

                IdentityResult result = await _roleManager.CreateAsync(role);
                return result.Succeeded;
            }

            return true;
        }
    }
}