using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Index()
        {
            return View(_context.Lecturers.Find(GlobalVariables.CurrentLoggedInUser.LecturerId));
        }
    }
}
