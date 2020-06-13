using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetIdentityProject.Models;
using AspNetIdentityProject.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentityProject.Controllers
{
    public class AdminController : Controller
    {
        UserManager<AppUser> _userManager { get; }
        public AdminController(UserManager<AppUser>  userManager)
        {
            _userManager = userManager;
        }
        public  IActionResult Index()
        {
            return  View(_userManager.Users.ToList());
        }

       
    }
}