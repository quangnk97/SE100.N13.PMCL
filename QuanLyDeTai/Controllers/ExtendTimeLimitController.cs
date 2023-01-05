﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDeTai.Models;

namespace QuanLyDeTai.Controllers
{
    public class ExtendTimeLimitController : Controller
    {
        private readonly QLDT_DbContext _context;

        public ExtendTimeLimitController(QLDT_DbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var RegistersListExtend = _context.Registers.Where(x => x.LecturerId == GlobalVariables.CurrentLoggedInUser.LecturerId && x.PositionTopic == "Topic Manager").ToList();

            List<Topic> TopicsListExtend = new List<Topic>();
            foreach (var item in RegistersListExtend)
            {
                var temp = _context.Topics.FirstOrDefault(x => x.TopicId == item.TopicId);
                if (temp.Approved == true && temp.IsExtended == false && temp.IsCancelled == 0)
                    TopicsListExtend.Add(temp);
            }    

            ViewData["TopicsListExtend"] = new SelectList(TopicsListExtend, "TopicId", "TopicName");
            ViewData["RegisteredYearListExtend"] = new SelectList(TopicsListExtend, "TopicId", "RegisteredYear");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Extend([Bind] Topic topic)
        {
            if (topic.TopicId != 0)
            {
                var topicTemp = await _context.Topics.FindAsync(topic.TopicId);

                if (topicTemp != null)
                {
                    topicTemp.IsExtended = true;
                    topicTemp.RequestTime = topic.RequestTime;
                    _context.Update(topicTemp);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var topicTemp = await _context.Topics.FindAsync(topic.TopicId);
                TempData["abc"] = "Topic doesn't exist.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
