using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LearningDiaryApp.Models;

namespace LearningDiaryApp.Data
{
    public class LearningDiaryAppContext : DbContext
    {
        public LearningDiaryAppContext (DbContextOptions<LearningDiaryAppContext> options)
            : base(options)
        {
        }

        public DbSet<LearningDiaryApp.Models.Topic> Topic { get; set; }

        public DbSet<LearningDiaryApp.Models.Task> Task { get; set; }

        public DbSet<LearningDiaryApp.Models.Note> Note { get; set; }
    }
}
