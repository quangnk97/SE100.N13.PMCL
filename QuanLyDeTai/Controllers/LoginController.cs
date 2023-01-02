using Microsoft.AspNetCore.Mvc;
using QuanLyDeTai.Models;
using Syncfusion.EJ2.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
                    {
                        GlobalVariables.CurrentLoggedInUser.LecturerId = 0;
                        return RedirectToAction(nameof(Index), "Home", new { Area = "Admin" });
                    }    
                    else
                        TempData["PasswordValid"] = "Wrong password.";
                }
                else
                {
                    var lecturerTemp = _context.Lecturers.FirstOrDefault(x => x.LecturerCode == lecturer.LecturerCode);
                    if (lecturerTemp != null)
                    {
                        if (lecturerTemp.Password == lecturer.Password)
                        {
                            GlobalVariables.CurrentLoggedInUser = lecturerTemp;
                            return RedirectToAction(nameof(Index), "Home");
                        }    
                        else
                            TempData["PasswordValid"] = "Wrong password.";

                    }
                    else
                    {
                        TempData["PasswordValid"] = "Account doesn't exist.";
                    }    
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
