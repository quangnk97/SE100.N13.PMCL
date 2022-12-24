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
using Newtonsoft.Json;

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
        public async Task<IActionResult> Register([Bind] LecturersInTopicViewModel topic)
        {
            if (String.IsNullOrEmpty(topic.TopicModel.TopicName))
            {
                if (String.IsNullOrEmpty(topic.TopicModel.ResearchField))
                {
                    TempData["TopicNameEmpty"] = "Required.";
                    TempData["ResearchFieldEmpty"] = "Required.";
                }
                else
                {
                    TempData["TopicNameEmpty"] = "Required.";
                }
            }
            else if (String.IsNullOrEmpty(topic.TopicModel.ResearchField))
            {
                if (String.IsNullOrEmpty(topic.TopicModel.TopicName))
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
                topic.TopicModel.RegisteredYear = DateTime.Now.Year;
                topic.TopicModel.Duration = 365;
                topic.TopicModel.Approved = false;
                topic.TopicModel.IsExtended = false;
                topic.TopicModel.IsCancelled = false;
                //_context.Add(topic.TopicModel);
                //await _context.SaveChangesAsync();

                var selectedLecturers = JsonConvert.DeserializeObject<List<Lecturer>>(topic.SelectedLecturersList);
                TempData["ResearchFieldEmpty"] = selectedLecturers[0].LecturerName;


                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}