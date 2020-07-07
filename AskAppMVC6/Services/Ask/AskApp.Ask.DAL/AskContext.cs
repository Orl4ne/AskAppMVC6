using AskApp.Ask.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AskApp.Ask.DAL
{
    [ExcludeFromCodeCoverage]
    public class AskContext : DbContext
    {
        public AskContext(DbContextOptions<AskContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
                throw new ArgumentNullException(nameof(optionsBuilder));

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=askApp.db;");
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));
        }

        public DbSet<QuestionEF> Questions { get; set; }
        public DbSet<AnswerEF> Answers { get; set; }
    }
}
