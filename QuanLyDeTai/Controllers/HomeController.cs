using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDeTai.Models;
using System.Diagnostics;

namespace QuanLyDeTai.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLDT_DbContext _context;

        public HomeController(QLDT_DbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var RegistersList = _context.Registers.Where(x => x.LecturerId == GlobalVariables.CurrentLoggedInUser.LecturerId && x.PositionTopic == "Topic Manager").ToList();


            // Number of topics
            List<Topic> TopicsList = new List<Topic>();
            foreach (var item in RegistersList)
            {
                var temp = await _context.Topics.FirstOrDefaultAsync(x => x.TopicId == item.TopicId);
                if (temp.Approved == true && temp.IsCancelled == 0)
                    TopicsList.Add(temp);
            }
            ViewBag.TopicsList = TopicsList;
            ViewBag.NumberOfTopics = TopicsList.Count.ToString();


            // Members of each topic
            List<List<Lecturer>> MembersOfEachTopics = new List<List<Lecturer>>();

            foreach (var item in TopicsList)
            {
                List<Lecturer> Lecturers = new List<Lecturer>();
                var LecturerIDList = _context.Registers.Where(x => x.TopicId == item.TopicId).ToList();
                foreach (var item1 in LecturerIDList)
                {
                    var Lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.LecturerId == item1.LecturerId);

                    Lecturers.Add(Lecturer);
                }
                MembersOfEachTopics.Add(Lecturers);
            }
            ViewBag.MembersOfEachTopics = MembersOfEachTopics;


            // Number of topics achieved
            List<Topic> TopicsListAchieved = new List<Topic>();
            foreach (var item in RegistersList)
            {
                var temp = await _context.Topics.FirstOrDefaultAsync(x => x.TopicId == item.TopicId);
                if (temp.Approved == true && temp.IsCancelled == 0 && temp.StatusId == "S01")
                    TopicsListAchieved.Add(temp);
            }
            ViewBag.NumberOfTopicsAchieved = TopicsListAchieved.Count.ToString();

            // Top 3 topics
            var Top3RegistersList = _context.Registers.Where(x => x.LecturerId == GlobalVariables.CurrentLoggedInUser.LecturerId).OrderByDescending(y => y.Score).Take(3).ToList();
            List<Topic> Top3TopicsList = new List<Topic>();
            foreach (var item in Top3RegistersList)
            {
                var temp = await _context.Topics.FirstOrDefaultAsync(x => x.TopicId == item.TopicId);
                if (temp.Approved == true && temp.IsCancelled == 0 && temp.StatusId == "S01")
                    Top3TopicsList.Add(temp);
            }
            ViewData["Top3Registers"] = Top3RegistersList;
            ViewData["Top3Topics"] = Top3TopicsList;
            /*ViewBag.Top3Registers = Top3RegistersList;
            ViewBag.Top3Topics = Top3TopicsList;*/

            return View();
        }
    }
}