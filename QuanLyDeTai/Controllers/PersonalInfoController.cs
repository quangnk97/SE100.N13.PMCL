using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var RegistersList = _context.Registers.Where(x => x.LecturerId == GlobalVariables.CurrentLoggedInUser.LecturerId).ToList();

            List<Topic> TopicsList = new List<Topic>();
            foreach (var item in RegistersList)
            {
                var temp = _context.Topics.FirstOrDefault(x => x.TopicId == item.TopicId);
                if (temp.Approved == true && temp.IsCancelled == 0)
                    TopicsList.Add(temp);
            }

            ViewBag.TopicsListForPersonalInfo = TopicsList;

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
