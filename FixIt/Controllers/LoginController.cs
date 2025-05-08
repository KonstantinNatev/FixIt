using Microsoft.AspNetCore.Mvc;

namespace FixIt.Controllers
{
    public class LoginController : Controller
    {
        private const string adminUser = "admin";
        private const string adminPass = "admin123";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == adminUser && password == adminPass)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Error = "Невалидни данни за вход.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Index");
        }
    }
}
