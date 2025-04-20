using Microsoft.AspNetCore.Mvc;
using FixIt.Data;
using FixIt.Models;

namespace FixIt.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Requests
        public IActionResult Index()
        {
            var requests = _context.RepairRequests.OrderByDescending(r => r.CreatedAt).ToList();
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
        public IActionResult Create(RepairRequest request)
        {
            if (ModelState.IsValid)
            {
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
