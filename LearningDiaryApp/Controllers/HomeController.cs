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
using Utility;

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

        public async Task<IActionResult> TopicSearchResult(string searchTerm)
        {
            var result = Query.Search(searchTerm, await _context.Topic.ToListAsync());
            ViewModel mymodel = new ViewModel();
            mymodel.Topics = result;
            mymodel.Tasks = await _context.Task.ToListAsync();
            mymodel.Notes = await _context.Note.ToListAsync();
            return View("Index", mymodel);
        }

        public async Task<IActionResult> TaskSearchResult(string searchTerm)
        {
            var result = Query.Search(searchTerm, await _context.Task.ToListAsync());
            var tIds = new List<int>();
            var taskiId = new List<int>();
            foreach (var task in result)
            {
                tIds.Add(task.TopicId);
                taskiId.Add(task.Id);
            }

            var topicsFiltered = new List<Topic>();
            var topics = await _context.Topic.ToListAsync();
            foreach (var id in tIds.Distinct())
            {
                var helpList = Query.Search(id.ToString(), topics);
                topicsFiltered.Add(helpList[0]);
            }


            foreach (var topi in topicsFiltered)
            {
                for (int i = topi.Tasks.Count - 1; i >= 0; i--)
                {
                    if (!taskiId.Contains(topi.Tasks.ToList()[i].Id))
                    {
                        topi.Tasks.Remove(topi.Tasks.ElementAt(i));
                    }
                }
            }

            ViewModel mymodel = new ViewModel();
            mymodel.Topics = topicsFiltered;
            mymodel.Tasks = result;
            mymodel.Notes = await _context.Note.ToListAsync();
            return View("Index", mymodel);
        }

        public async Task<IActionResult> NoteSearchResult(string searchTerm)
        {
            ViewModel mymodel = new ViewModel();
            mymodel.Topics = await _context.Topic.ToListAsync();
            mymodel.Tasks = await _context.Task.ToListAsync();
            mymodel.Notes = await _context.Note.ToListAsync();
            return View("Index", mymodel);
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
