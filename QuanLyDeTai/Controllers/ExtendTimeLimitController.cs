using Microsoft.AspNetCore.Mvc;
using QuanLyDeTai.Models;

namespace QuanLyDeTai.Controllers
{
    public class ExtendTimeLimitController : Controller
    {
        private readonly QLDT_DbContext _context;

        public ExtendTimeLimitController(QLDT_DbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Extend()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
