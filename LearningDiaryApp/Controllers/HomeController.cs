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
using SearchObject;

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
            if (!String.IsNullOrEmpty(searchTerm))
            {
                var result = Query.Search(searchTerm, await _context.Topic.ToListAsync());
                ViewModel mymodel = new ViewModel();
                mymodel.Topics = result;
                mymodel.Tasks = await _context.Task.ToListAsync();
                mymodel.Notes = await _context.Note.ToListAsync();
                return View("Index", mymodel);
            }
            return View("Index", new ViewModel());
        }

        public async Task<IActionResult> TaskSearchResult(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                var result = Query.Search(searchTerm, await _context.Task.ToListAsync());

                var topicIds = new List<int>();
                var taskiIds = new List<int>();
                foreach (var task in result)
                {
                    topicIds.Add(task.TopicId);
                    taskiIds.Add(task.Id);
                }

                var topicsFiltered = new List<Topic>();
                var topics = await _context.Topic.ToListAsync();
                foreach (var id in topicIds.Distinct())
                {
                    var helpList = Query.Search(id.ToString(), topics);
                    topicsFiltered.Add(helpList.First());
                }


                foreach (var topi in topicsFiltered)
                {
                    for (int i = topi.Tasks.Count - 1; i >= 0; i--)
                    {
                        if (!taskiIds.Contains(topi.Tasks.ToList()[i].Id))
                            topi.Tasks.Remove(topi.Tasks.ElementAt(i));
                    }
                }

                ViewModel mymodel = new ViewModel();
                mymodel.Topics = topicsFiltered;
                mymodel.Tasks = result;
                mymodel.Notes = await _context.Note.ToListAsync();
                return View("Index", mymodel);
            }
            return View("Index", new ViewModel());
        }
            

        public async Task<IActionResult> NoteSearchResult(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                var result = Query.Search(searchTerm, await _context.Note.ToListAsync());

                var topicIds = new List<int>();
                var taskiIds = new List<int>();
                var noteIds = new List<int>();


                foreach (Note note in result)
                {
                    taskiIds.Add(note.TaskId);
                    noteIds.Add(note.Id);
                }

                var tasksFiltered = new List<Models.Task>();
                var tasks = await _context.Task.ToListAsync();
                foreach (var id in taskiIds.Distinct())
                {
                    var helpList1 = Query.Search(id.ToString(), tasks);
                    tasksFiltered.Add(helpList1.First());
                }

                foreach (var taski in tasksFiltered)
                {
                    for (int i = taski.Notes.Count - 1; i >= 0; i--)
                    {
                        if (!noteIds.Contains(taski.Notes.ToList()[i].Id))
                            taski.Notes.Remove(taski.Notes.ElementAt(i));
                    }
                }

                foreach (Models.Task task in tasksFiltered)
                {
                    topicIds.Add(task.TopicId);
                }

                var topicsFiltered = new List<Topic>();
                var topics = await _context.Topic.ToListAsync();
                foreach (var id in topicIds.Distinct())
                {
                    var helpList = Query.Search(id.ToString(), topics);
                    topicsFiltered.Add(helpList.First());
                }


                foreach (var topi in topicsFiltered)
                {
                    for (int i = topi.Tasks.Count - 1; i >= 0; i--)
                    {
                        if (!taskiIds.Contains(topi.Tasks.ToList()[i].Id))
                            topi.Tasks.Remove(topi.Tasks.ElementAt(i));
                    }
                }

                ViewModel mymodel = new ViewModel();
                mymodel.Topics = topicsFiltered;
                mymodel.Tasks = tasksFiltered;
                mymodel.Notes = result;
                return View("Index", mymodel);
            }
            return View("Index", new ViewModel());
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
