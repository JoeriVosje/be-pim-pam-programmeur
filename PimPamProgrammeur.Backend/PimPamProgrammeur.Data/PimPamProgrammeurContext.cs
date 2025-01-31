﻿using Microsoft.EntityFrameworkCore;
using PimPamProgrammeur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PimPamProgrammeur.Data
{
    public class PimPamProgrammeurContext : DbContext
    {
        public PimPamProgrammeurContext(DbContextOptions<PimPamProgrammeurContext> options)
            : base(options)
        {
        }

        public DbSet<Module> Modules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.FindEntityType(typeof(Result)).GetForeignKeys())
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

    }
}
