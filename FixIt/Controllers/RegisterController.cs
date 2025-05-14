using Microsoft.AspNetCore.Mvc;
using FixIt.Data;
using FixIt.Models;
using System.Linq;

namespace FixIt.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Потребителското име вече съществува.";
                return View();
            }

            var user = new User
            {
                Username = username,
                Password = password,
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Регистрацията е успешна. Влезте в системата.";
            return RedirectToAction("Index", "Login");
        }
    }
}
