using Microsoft.AspNetCore.Mvc;
using FixIt.Data;
using FixIt.Models;

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

        // GET: /Admin
        public IActionResult Index()
        {
            // Ако искаш защита на достъпа – махни коментара по-долу:
            // if (HttpContext.Session.GetString("IsAdmin") != "true")
            // {
            //     return RedirectToAction("Index", "Login");
            // }

            var requests = _context.RepairRequests
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return View(requests);
        }

        // POST: /Admin/ChangeStatus/5
        [HttpPost]
        public IActionResult ChangeStatus(int id, RequestStatus newStatus)
        {
            var request = _context.RepairRequests.Find(id);
            if (request == null) return NotFound();

            request.Status = newStatus;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var request = _context.RepairRequests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            // Ако има снимка – изтрий и нея от диска
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
