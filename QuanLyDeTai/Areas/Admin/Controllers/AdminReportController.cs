using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDeTai.Models;

namespace QuanLyDeTai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminReportController : Controller
    {
        private readonly QLDT_DbContext _context;

        public AdminReportController(QLDT_DbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchTopic)
        {
            var topic = await _context.Topics.FirstOrDefaultAsync(x => x.TopicName == searchTopic);
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "AssessmentId");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusName");
            ViewData["SearchTopic"] = searchTopic;
            return View(topic);
        }
    }
}
