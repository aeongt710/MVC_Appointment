using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Appointment.Models;
using MVC_Appointment.Models.ViewModels;
using System.Threading.Tasks;

namespace MVC_Appointment.Controllers
{
    public class AccountController : Controller
    {
        public readonly ApplicationDBContext _dbContext;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;



        public AccountController(ApplicationDBContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager
            )
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _roleManager = roleManager;
            _dbContext = db;
        }

        public IActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password,vm.RememberMe,false);
                if(signInResult.Succeeded)
                {
                    return RedirectToAction("Index","Appointment");
                }
                ModelState.AddModelError("", "Invalid Login Attempt!");
            }
            return View(vm);
        }

        public async Task<IActionResult> Register()
        {
            if(!_roleManager.RoleExistsAsync(Utility.Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Doctor));
                await _roleManager.CreateAsync(new IdentityRole(Utility.Helper.Patient));
            }
            return View();
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM m)
        {
            if(ModelState.IsValid)
            {
                var _user = new ApplicationUser
                {
                    Email = m.Email,
                    Name = m.Name,
                    UserName= m.Email
                };
                var result=  _userManager.CreateAsync(_user,m.Password).Result;
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(_user,m.RoleName);
                    await _signInManager.SignInAsync(_user,isPersistent:false);

                    return RedirectToAction("Index", "Home");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(m);
        }
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }

    }
}
