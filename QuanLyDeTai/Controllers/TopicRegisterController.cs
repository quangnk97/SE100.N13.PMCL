using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDeTai.Models;

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
            return View();
        }
    }
}
