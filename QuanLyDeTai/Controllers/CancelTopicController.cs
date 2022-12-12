using Microsoft.AspNetCore.Mvc;
using QuanLyDeTai.Models;

namespace QuanLyDeTai.Controllers
{
    public class CancelTopicController : Controller
    {
        private readonly QLDT_DbContext _context;

        public CancelTopicController(QLDT_DbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
