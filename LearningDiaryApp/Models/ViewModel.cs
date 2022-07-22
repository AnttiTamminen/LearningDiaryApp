using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDiaryApp.Models
{
    public class ViewModel
    {
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}
