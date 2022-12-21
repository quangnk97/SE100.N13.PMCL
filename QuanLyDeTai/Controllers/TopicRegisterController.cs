using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using QuanLyDeTai.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuanLyDeTai.Controllers
{
    public class TopicRegisterController : Controller
    {
        private readonly QLDT_DbContext _context;

        public TopicRegisterController(QLDT_DbContext context)
        {
            _context = context;
        }

        // GET: TopicRegister
        public async Task<IActionResult> Index()
        {
            ViewBag.LecturersList = await _context.Lecturers.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind]Topic topic, List<Lecturer> selectedLecturers)
        {
            TempData["abc"] = User.Identity.GetUserId();
            if (String.IsNullOrEmpty(topic.TopicName))
            {
                if (String.IsNullOrEmpty(topic.ResearchField))
                {
                    TempData["TopicNameEmpty"] = "Required.";
                    TempData["ResearchFieldEmpty"] = "Required.";
                }
                else
                {
                    TempData["TopicNameEmpty"] = "Required.";
                }
            }
            else if (String.IsNullOrEmpty(topic.ResearchField))
            {
                if (String.IsNullOrEmpty(topic.TopicName))
                {
                    TempData["TopicNameEmpty"] = "Required.";
                    TempData["ResearchFieldEmpty"] = "Required.";
                }
                else
                {
                    TempData["ResearchFieldEmpty"] = "Required.";
                }
            }
            else
            {
                topic.RegisteredYear = DateTime.Now.Year;
                topic.Duration = 365;
                topic.Approved = false;
                topic.IsExtended = false;
                topic.IsCancelled = false;
                //_context.Add(topic);
                //await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
