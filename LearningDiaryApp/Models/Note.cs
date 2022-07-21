using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace LearningDiaryApp.Models
{
    public partial class Note
    {
        public Note() { }

        public int Id { get; set; }
        public string Title { get; set; }
        public int TaskId { get; set; }
        public string Note1 { get; set; }

        public virtual Task Task { get; set; }
    }
}
