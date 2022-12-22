using Microsoft.AspNetCore.Mvc;
using QuanLyDeTai.Models;
using Syncfusion.EJ2.Linq;

namespace QuanLyDeTai.Controllers
{
    public class LoginController : Controller
    {
        private readonly QLDT_DbContext _context;

        public LoginController(QLDT_DbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new Lecturer());
        }


        [HttpGet]
        public async Task<IActionResult> Login([Bind] Lecturer lecturer)
        {
            if (String.IsNullOrEmpty(lecturer.LecturerCode) || String.IsNullOrEmpty(lecturer.Password))
            {
                TempData["Message"] = "Please fill out the necessary information.";
            }    
            else
            {
                if (lecturer.LecturerCode == "admin")
                {
                    if (lecturer.Password == "daylamatkhaudanhchoadmin")
                        return RedirectToAction(nameof(Index), "Home", new { Area = "Admin" });
                    else
                        TempData["PasswordValid"] = "Wrong password.";
                }
                else
                {
                    var lecturerTemp = _context.Lecturers.FirstOrDefault(x => x.LecturerCode == lecturer.LecturerCode);
                    if (lecturerTemp != null)
                    {
                        if (lecturerTemp.Password == lecturer.Password)
                            return RedirectToAction(nameof(Index), "Home");
                        else
                            TempData["PasswordValid"] = "Wrong password.";

                    }
                    else
                    {
                        TempData["PasswordValid"] = "Account not exists.";
                    }    
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
