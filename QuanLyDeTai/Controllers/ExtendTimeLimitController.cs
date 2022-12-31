using Microsoft.AspNetCore.Mvc;
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
            var RegistersList = _context.Registers.Where(x => x.LecturerId == GlobalVariables.CurrentLoggedInUser.LecturerId && x.PositionTopic == "Topic Manager").ToList();

            List<Topic> TopicsList = new List<Topic>();
            foreach (var item in RegistersList)
            {
                TopicsList.Add(_context.Topics.FirstOrDefault(x => x.TopicId == item.TopicId));
            }    

            ViewData["TopicsList"] = new SelectList(TopicsList, "TopicId", "TopicName");
            ViewData["RegisteredYearList"] = new SelectList(TopicsList, "TopicId", "RegisteredYear");

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
                    if (topicTemp.IsExtended == false)
                    {
                        topicTemp.IsExtended = true;
                        topicTemp.RequestTime = topic.RequestTime;
                        _context.Update(topicTemp);
                        await _context.SaveChangesAsync();
                    }    
                    else
                    {
                        TempData["abc"] = "This topic has already been extended.";
                    }    
                }
            }
            else
            {
                TempData["abc"] = "Topic doesn't exist.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
