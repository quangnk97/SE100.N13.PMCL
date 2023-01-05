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
            ViewBag.LecturersList = await _context.Lecturers.Where(x => x.LecturerId != GlobalVariables.CurrentLoggedInUser.LecturerId).ToListAsync();
            ViewBag.FacultiesList = await _context.Faculties.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind] LecturersInTopicViewModel topic)
        {
            if (String.IsNullOrEmpty(topic.TopicModel.TopicName))
            {
                if (String.IsNullOrEmpty(topic.TopicModel.ResearchField))
                {
                    TempData["TopicNameEmpty"] = "Topic name required.";
                    TempData["ResearchFieldEmpty"] = "Research field required.";
                }
                else
                {
                    TempData["TopicNameEmpty"] = "Topic name required.";
                }
            }
            else if (String.IsNullOrEmpty(topic.TopicModel.ResearchField))
            {
                if (String.IsNullOrEmpty(topic.TopicModel.TopicName))
                {
                    TempData["TopicNameEmpty"] = "Topic name required.";
                    TempData["ResearchFieldEmpty"] = "Research field required.";
                }
                else
                {
                    TempData["ResearchFieldEmpty"] = "Research field required.";
                }
            }
            else
            {
                if (String.IsNullOrEmpty(topic.SelectedLecturersList)){}
                else
                {
                    var selectedLecturers = JsonConvert.DeserializeObject<List<Lecturer>>(topic.SelectedLecturersList);

                    if (selectedLecturers.Count > 0 && selectedLecturers.Count <= 4)
                    {
                        topic.TopicModel.RegisteredYear = DateTime.Now.Year;
                        topic.TopicModel.Duration = 365;
                        topic.TopicModel.Approved = false;
                        topic.TopicModel.IsExtended = false;
                        topic.TopicModel.IsCancelled = 0;

                        _context.Add(topic.TopicModel);
                        await _context.SaveChangesAsync();

                        var topicTemp = await _context.Topics.FirstOrDefaultAsync(x => x.TopicName == topic.TopicModel.TopicName);
                        if (topicTemp != null)
                        {
                            for (int i = 0; i < selectedLecturers.Count; i++)
                            {
                                Register register = new Register();
                                register.TopicId = topicTemp.TopicId;
                                register.LecturerId = selectedLecturers[i].LecturerId;
                                register.PositionTopic = "Author";
                                register.LevelLecturer = selectedLecturers[i].LevelCurrent;

                                _context.Add(register);
                                await _context.SaveChangesAsync();
                            }
                        }

                        Register reg = new Register();
                        reg.TopicId = topicTemp.TopicId;
                        reg.LecturerId = GlobalVariables.CurrentLoggedInUser.LecturerId;
                        reg.PositionTopic = "Topic Manager";
                        reg.LevelLecturer = GlobalVariables.CurrentLoggedInUser.LevelCurrent;

                        _context.Add(reg);
                        await _context.SaveChangesAsync();
                    }
                    else if (selectedLecturers.Count == 0)
                    {
                        topic.TopicModel.RegisteredYear = DateTime.Now.Year;
                        topic.TopicModel.Duration = 365;
                        topic.TopicModel.Approved = false;
                        topic.TopicModel.IsExtended = false;
                        topic.TopicModel.IsCancelled = 0;

                        _context.Add(topic.TopicModel);
                        await _context.SaveChangesAsync();

                        var topicTemp = await _context.Topics.FirstOrDefaultAsync(x => x.TopicName == topic.TopicModel.TopicName);
                        if (topicTemp != null)
                        {
                            Register reg = new Register();
                            reg.TopicId = topicTemp.TopicId;
                            reg.LecturerId = GlobalVariables.CurrentLoggedInUser.LecturerId;
                            reg.PositionTopic = "Topic Manager";
                            reg.LevelLecturer = GlobalVariables.CurrentLoggedInUser.LevelCurrent;

                            _context.Add(reg);
                            await _context.SaveChangesAsync();
                        }
                    }    
                    else
                        TempData["TopicMembersInvalid"] = "Maximum 5 members for one topic.";
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}