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

        [HttpPost]
        public async Task<IActionResult> ModifyPersonalInfo([Bind] Lecturer lecturer)
        {
            var lecturerTemp = await _context.Lecturers.FindAsync(lecturer.LecturerId);

            if (lecturerTemp != null)
            {
                lecturerTemp.LecturerName = lecturer.LecturerName;
                lecturerTemp.Dob = lecturer.Dob;
                lecturerTemp.Email = lecturer.Email;
                lecturerTemp.PhoneNumber = lecturer.PhoneNumber;
                _context.Update(lecturerTemp);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
