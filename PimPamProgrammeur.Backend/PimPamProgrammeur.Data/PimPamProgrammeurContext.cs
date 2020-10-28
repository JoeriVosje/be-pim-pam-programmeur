using Microsoft.EntityFrameworkCore;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PimPamProgrammeur.Data
{
    public class PimPamProgrammeurContext : DbContext
    {
        public PimPamProgrammeurContext(DbContextOptions<PimPamProgrammeurContext> options)
            : base(options)
        {
            // This makes EF a bit faster. Don't use this when you add logic to the controller that relies on the query results
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Module> Modules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentInfo> ComponentInfos { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<ResultOverview> ResultOverviews { get; set; }
        public DbSet<Session> Sessions { get; set; }

    }
}
