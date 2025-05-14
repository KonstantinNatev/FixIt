using Microsoft.AspNetCore.Mvc;
using FixIt.Data;
using FixIt.Models;
using Microsoft.AspNetCore.Http;

namespace FixIt.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var requests = _context.RepairRequests
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int id, RequestStatus newStatus)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var request = _context.RepairRequests.Find(id);
            if (request == null) return NotFound();

            request.Status = newStatus;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Login");
            }

            var request = _context.RepairRequests.Find(id);
            if (request == null) return NotFound();

            if (!string.IsNullOrEmpty(request.ImagePath))
            {
                var fullPath = Path.Combine(_environment.WebRootPath, request.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

            _context.RepairRequests.Remove(request);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
