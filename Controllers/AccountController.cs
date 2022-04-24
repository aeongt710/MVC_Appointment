using Microsoft.AspNetCore.Mvc;
using MVC_Appointment.Models;

namespace MVC_Appointment.Controllers
{
    public class AccountController : Controller
    {
        public readonly ApplicationDBContext _dbContext;
        public AccountController(ApplicationDBContext db)
        {
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
    }
}
