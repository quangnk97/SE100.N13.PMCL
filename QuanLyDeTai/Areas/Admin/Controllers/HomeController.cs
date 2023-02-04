using Microsoft.AspNetCore.Mvc;
using QuanLyDeTai.Models;

namespace QuanLyDeTai.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
