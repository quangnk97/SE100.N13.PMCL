using Microsoft.AspNetCore.Mvc;
using QuanLyDeTai.Models;

namespace QuanLyDeTai.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly QLDT_DbContext _context;

        public PersonalInfoController(QLDT_DbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
