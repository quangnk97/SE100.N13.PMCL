using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDeTai.Models;

namespace QuanLyDeTai.Controllers
{
    public class CancelTopicController : Controller
    {
        private readonly QLDT_DbContext _context;

        public CancelTopicController(QLDT_DbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var RegistersListCancel = _context.Registers.Where(x => x.LecturerId == GlobalVariables.CurrentLoggedInUser.LecturerId && x.PositionTopic == "Topic Manager").ToList();

            List<Topic> TopicsListCancel = new List<Topic>();
            foreach (var item in RegistersListCancel)
            {
                var temp = _context.Topics.FirstOrDefault(x => x.TopicId == item.TopicId);
                if (temp.Approved == true && temp.IsCancelled == 0)
                    TopicsListCancel.Add(temp);
            }

            ViewData["TopicsListCancel"] = new SelectList(TopicsListCancel, "TopicId", "TopicName");
            ViewData["RegisteredYearListCancel"] = new SelectList(TopicsListCancel, "TopicId", "RegisteredYear");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel([Bind] Topic topic)
        {
            if (topic.TopicId != 0)
            {
                if (String.IsNullOrEmpty(topic.Reason))
                {
                    TempData["abc"] = "Reason required.";
                }
                else
                {
                    var topicTemp = await _context.Topics.FindAsync(topic.TopicId);

                    if (topicTemp != null)
                    {
                        topicTemp.IsCancelled = 1;
                        topicTemp.Reason = topic.Reason;
                        _context.Update(topicTemp);
                        await _context.SaveChangesAsync();
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
