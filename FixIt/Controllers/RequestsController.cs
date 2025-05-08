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

        // GET: /Requests
        public IActionResult Index()
        {
            var requests = _context.RepairRequests
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return View(requests);
        }

        // GET: /Requests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Requests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RepairRequest request, IFormFile ImageFile)
        {
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

                _context.RepairRequests.Add(request);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }
    }
}
