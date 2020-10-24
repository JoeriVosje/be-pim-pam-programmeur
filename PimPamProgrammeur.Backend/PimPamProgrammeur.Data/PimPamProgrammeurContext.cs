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

        public DbSet<Student> Students { get; set; }
    }
}
