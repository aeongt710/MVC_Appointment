using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Appointment.Models;
using MVC_Appointment.Models.ViewModels;

namespace MVC_Appointment.Controllers
{
    public class AccountController : Controller
    {
        public readonly ApplicationDBContext _dbContext;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<ApplicationUser> _roleManager;



        public AccountController(ApplicationDBContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationUser> roleManager)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _roleManager=roleManager;
            _dbContext = db;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public  IActionResult Register(RegisterVM m)
        {
            if(ModelState.IsValid)
            {
                var _user = new ApplicationUser
                {
                    Email = m.Email,
                    Name = m.Name,
                    UserName= m.Email
                };
                var result=  _userManager.CreateAsync(_user).Result;
                if (result.Succeeded)
                {
                    _signInManager.SignInAsync(_user,isPersistent:false);
                }
            }
            return View();
        }
    }
}
