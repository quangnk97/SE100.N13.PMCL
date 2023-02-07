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
    public class AdminAssessmentsController : Controller
    {
        private readonly QLDT_DbContext _context;

        public AdminAssessmentsController(QLDT_DbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminAssessments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Assessments.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("AssessmentId,NumberOfMembers")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assessment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assessment);
        }

        // GET: Admin/AdminAssessments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Assessments == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessments
                .FirstOrDefaultAsync(m => m.AssessmentId == id);
            if (assessment == null)
            {
                return NotFound();
            }

            return View(assessment);
        }

        // GET: Admin/AdminAssessments/Create
        public IActionResult Create(string searchTopicID, string search1, string search2, 
                                    string search3, string search4, string search5, string search6,
                                    string search7, string num)
        {
            if (!string.IsNullOrEmpty(searchTopicID))
            {
                ViewData["SearchTopicID"] = searchTopicID;
                List<Topic> topics = _context.Topics.ToList();
                ViewData["TopicList"] = topics;

                List<Lecturer> lecturers = _context.Lecturers.ToList();
                ViewData["LecturerList"] = lecturers;

                if (!string.IsNullOrEmpty(search1)) { ViewData["Search1"] = search1; }
                if (!string.IsNullOrEmpty(search2)) { ViewData["Search2"] = search2; }
                if (!string.IsNullOrEmpty(search3)) { ViewData["Search3"] = search3; }
                if (!string.IsNullOrEmpty(search4)) { ViewData["Search4"] = search4; }
                if (!string.IsNullOrEmpty(search5)) { ViewData["Search5"] = search5; }
                if (!string.IsNullOrEmpty(search6)) { ViewData["Search6"] = search6; }
                if (!string.IsNullOrEmpty(search7)) { ViewData["Search7"] = search7; }
                if (!string.IsNullOrEmpty(num)) { ViewData["Num"] = num; }
            }
            return View();
        }

        // POST: Admin/AdminAssessments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create1([Bind] SubmitCouncil a)
        {
            Assessment ass = new Assessment();
            ass.NumberOfMembers = a.NumberofAssessment;
            _context.Assessments.Add(ass);
            await _context.SaveChangesAsync();

            Topic to = await _context.Topics.FirstOrDefaultAsync(x => x.TopicId == a.idtopic);
            if (to != null)
            {
                to.AssessmentDate = a.date;
                to.AssessmentId = ass.AssessmentId;
                _context.Topics.Update(to);
                await _context.SaveChangesAsync();
            }

            Assess s1 = new Assess();
            Lecturer lec = await _context.Lecturers.FirstOrDefaultAsync(x => x.LecturerCode == a.LCode1);
            if (lec != null)
            {
                s1.AssessmentId = ass.AssessmentId;
                s1.TopicId = a.idtopic;
                s1.LecturerId = lec.LecturerId;
                s1.PositionAssess = a.Pos1;
                _context.Assesses.Add(s1);
            }

            Assess s2 = new Assess();
            lec = await _context.Lecturers.FirstOrDefaultAsync(x => x.LecturerCode == a.LCode2);
            if (lec != null)
            {
                s2.AssessmentId = ass.AssessmentId;
                s2.TopicId = a.idtopic;
                s2.LecturerId = lec.LecturerId;
                s2.PositionAssess = a.Pos2;
                _context.Assesses.Add(s2);
            }

            Assess s3 = new Assess();
            lec = await _context.Lecturers.FirstOrDefaultAsync(x => x.LecturerCode == a.LCode3);
            if (lec != null)
            {
                s3.AssessmentId = ass.AssessmentId;
                s3.TopicId = a.idtopic;
                s3.LecturerId = lec.LecturerId;
                s3.PositionAssess = a.Pos3;
                _context.Assesses.Add(s3);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminAssessments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Assessments == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessments.FindAsync(id);
            if (assessment == null)
            {
                return NotFound();
            }
            return View(assessment);
        }

        // POST: Admin/AdminAssessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssessmentId,NumberOfMembers")] Assessment assessment)
        {
            if (id != assessment.AssessmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assessment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssessmentExists(assessment.AssessmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(assessment);
        }

        // GET: Admin/AdminAssessments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Assessments == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessments
                .FirstOrDefaultAsync(m => m.AssessmentId == id);
            if (assessment == null)
            {
                return NotFound();
            }

            return View(assessment);
        }

        // POST: Admin/AdminAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Assessments == null)
            {
                return Problem("Entity set 'QLDT_DbContext.Assessments'  is null.");
            }
            var assessment = await _context.Assessments.FindAsync(id);
            if (assessment != null)
            {
                _context.Assessments.Remove(assessment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssessmentExists(int id)
        {
          return _context.Assessments.Any(e => e.AssessmentId == id);
        }
    }
}
