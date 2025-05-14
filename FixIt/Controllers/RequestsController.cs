// RequestsController.cs
using Microsoft.AspNetCore.Mvc;
using FixIt.Data;
using FixIt.Models;

namespace FixIt.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public RequestsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "User")
            {
                return RedirectToAction("Index", "Login");
            }

            var username = HttpContext.Session.GetString("Username");
            var requests = _context.RepairRequests
                .Where(r => r.Username == username)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return View(requests);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "User")
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RepairRequest request, IFormFile ImageFile)
        {
            if (HttpContext.Session.GetString("Role") != "User")
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    request.ImagePath = "/uploads/" + fileName;
                }

                request.CreatedAt = DateTime.Now;
                request.Status = RequestStatus.Waiting;
                request.Username = HttpContext.Session.GetString("Username");

                _context.RepairRequests.Add(request);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Role") != "User")
            {
                return RedirectToAction("Index", "Login");
            }

            var request = _context.RepairRequests.Find(id);
            if (request == null || request.Username != HttpContext.Session.GetString("Username"))
            {
                return NotFound();
            }

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RepairRequest updatedRequest, IFormFile NewImageFile)
        {
            if (HttpContext.Session.GetString("Role") != "User")
            {
                return RedirectToAction("Index", "Login");
            }

            var request = _context.RepairRequests.Find(id);
            if (request == null || request.Username != HttpContext.Session.GetString("Username"))
            {
                return NotFound();
            }

            request.ClientName = updatedRequest.ClientName;
            request.Phone = updatedRequest.Phone;
            request.Category = updatedRequest.Category;
            request.Description = updatedRequest.Description;
            request.Status = updatedRequest.Status;

            if (NewImageFile != null && NewImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(request.ImagePath))
                {
                    var oldPath = Path.Combine(_environment.WebRootPath, request.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(NewImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    NewImageFile.CopyTo(stream);
                }

                request.ImagePath = "/uploads/" + fileName;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Requests");
        }
    }
}
