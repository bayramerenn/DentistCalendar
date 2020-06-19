using DentistCalendar.Data.Entity;
using DentistCalendar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Linq;

namespace DentistCalendar.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            SecretaryViewModel secretaryViewModel = new SecretaryViewModel
            {
                User = user,
                Dentists = _userManager.Users.Where(x => x.IsDentist)
            };
            return View(user.IsDentist == false ?
                               "Secretary"
                               : "Dentist",secretaryViewModel);
        }

        public IActionResult Secretary()
        {
            return View();
        }

        public IActionResult Dentist()
        {
            return View();
        }
    }
}