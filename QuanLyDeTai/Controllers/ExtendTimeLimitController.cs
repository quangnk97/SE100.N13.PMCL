using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var RegistersListExtend = await _context.Registers.Where(x => x.LecturerId == GlobalVariables.CurrentLoggedInUser.LecturerId && x.PositionTopic == "Topic Manager").ToListAsync();

            List<Topic> TopicsListExtend = new List<Topic>();
            foreach (var item in RegistersListExtend)
            {
                var temp = await _context.Topics.FirstOrDefaultAsync(x => x.TopicId == item.TopicId);
                if (temp.Approved == true && temp.IsExtended == 0 && temp.IsCancelled == 0)
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
                    topicTemp.IsExtended = 1;
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
