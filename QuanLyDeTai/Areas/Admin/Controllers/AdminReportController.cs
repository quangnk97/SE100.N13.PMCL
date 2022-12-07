using Microsoft.AspNetCore.Mvc;

namespace QuanLyDeTai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
