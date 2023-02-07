using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDeTai.Models;
using System.Collections.Generic;

namespace QuanLyDeTai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTopicsController : Controller
    {
        private readonly QLDT_DbContext _context;

        public AdminTopicsController(QLDT_DbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminTopics
        public async Task<IActionResult> Index(string searchDisapproved, string searchExtending, string searchCancelling, string searchApproved)
        {
            var qLDT_DbContext = _context.Topics.Include(t => t.Assessment).Include(t => t.Status);
            List<Topic> Topics = await qLDT_DbContext.ToListAsync();
            ViewData["SearchDisapproved"] = searchDisapproved;
            ViewData["SearchExtending"] = searchExtending;
            ViewData["SearchCancelling"] = searchCancelling;
            ViewData["SearchApproved"] = searchApproved;

            return View(Topics);
        }

        public async Task<IActionResult> AcceptD(int id)
        {
            var qLDT_DbContext = _context.Topics.Include(t => t.Assessment).Include(t => t.Status);
            List<Topic> Topics = await qLDT_DbContext.ToListAsync();
            var topic = Topics.FirstOrDefault(t=>t.TopicId==id);
            

                try
                {
                    topic.Approved = true;
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.TopicId))
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

        public async Task<IActionResult> AcceptExtend(int id)
        {
            var qLDT_DbContext = _context.Topics.Include(t => t.Assessment).Include(t => t.Status);
            List<Topic> Topics = await qLDT_DbContext.ToListAsync();
            var topic = Topics.FirstOrDefault(t => t.TopicId == id);


            try
            {
                topic.IsExtended = -1;
                topic.Duration = topic.Duration + topic.RequestTime * 30;
                _context.Update(topic);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(topic.TopicId))
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

        public async Task<IActionResult> AcceptCancel(int id)
        {
            var qLDT_DbContext = _context.Topics.Include(t => t.Assessment).Include(t => t.Status);
            List<Topic> Topics = await qLDT_DbContext.ToListAsync();
            var topic = Topics.FirstOrDefault(t => t.TopicId == id);


            try
            {
                topic.IsCancelled = -1;
                _context.Update(topic);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(topic.TopicId))
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

        // GET: Admin/AdminTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Assessment)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.TopicId == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Admin/AdminTopics/Create
        public IActionResult Create()
        {
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "AssessmentId");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return View();
        }

        // POST: Admin/AdminTopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicId,TopicName,RegisteredYear,ResearchField,LevelTopic,Duration,AssessmentId,AssessmentDate,Comment,Rating,StatusId,Approved,Reason,IsExtended,IsCancelled,RequestTime")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "AssessmentId", topic.AssessmentId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", topic.StatusId);
            return View(topic);
        }

        // GET: Admin/AdminTopics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.FindAsync(id);
            var list1 = new List<String>() { "Achieved", "Not Achieved" };
            if (topic == null)
            {
                return NotFound();
            }
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "AssessmentId", topic.AssessmentId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusName", topic.StatusId);
            return View(topic);
        }

        // POST: Admin/AdminTopics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicId,TopicName,RegisteredYear,ResearchField,LevelTopic,Duration,AssessmentId,AssessmentDate,Comment,Rating,StatusId,Approved,Reason,IsExtended,IsCancelled,RequestTime")] Topic topic)
        {
            if (id != topic.TopicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.TopicId))
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
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "AssessmentId", topic.AssessmentId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return View(topic);
        }

        // GET: Admin/AdminTopics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Topics == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Assessment)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.TopicId == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Admin/AdminTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Topics == null)
            {
                return Problem("Entity set 'QLDT_DbContext.Topics'  is null.");
            }
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicExists(int id)
        {
          return _context.Topics.Any(e => e.TopicId == id);
        }
    }
}
