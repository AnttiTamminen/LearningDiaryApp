using LearningDiaryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using LearningDiaryApp.Data;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace LearningDiaryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly LearningDiaryAppContext _context;

        public HomeController(ILogger<HomeController> logger, LearningDiaryAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewModel mymodel = new ViewModel();
            mymodel.Topics = await _context.Topic.ToListAsync();
            mymodel.Tasks = await _context.Task.ToListAsync();
            mymodel.Notes = await _context.Note.ToListAsync();
            return View(mymodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
