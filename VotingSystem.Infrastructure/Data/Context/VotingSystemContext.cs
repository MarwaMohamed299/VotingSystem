using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Identity.Models;

namespace VotingSystem.Infrastructure.Data.Context
{
    public class VotingSystemContext : IdentityDbContext<PlatFormUser, PlatFormRole, Guid>
    {
        public DbSet<Voter> Voters => Set<Voter>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Vote> Votes => Set<Vote>();
        public DbSet<Option> Options => Set<Option>();
        public DbSet<Poll> Polls => Set<Poll>();
        public VotingSystemContext(DbContextOptions<VotingSystemContext> Options) : base(Options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seeding data
            modelBuilder.Entity<Poll>().HasData(
                new Poll { PollId = 1, Title = "Favorite Programming Language", StartDate = new DateTime(2024, 5, 1), EndDate = new DateTime(2024, 5, 31) }
            );

            modelBuilder.Entity<Question>().HasData(
                new Question { QuestionId = 1, PollId = 1, Text = "What is your favorite backend language?" },
                new Question { QuestionId = 2, PollId = 1, Text = "What is your favorite frontend language?" }
            );

            modelBuilder.Entity<Option>().HasData(
                new Option { OptionId = 1, QuestionId = 1, Description = "C#" },
                new Option { OptionId = 2, QuestionId = 1, Description = "Java" },
                new Option { OptionId = 3, QuestionId = 1, Description = "Python" },
                new Option { OptionId = 4, QuestionId = 2, Description = "JavaScript" },
                new Option { OptionId = 5, QuestionId = 2, Description = "TypeScript" },
                new Option { OptionId = 6, QuestionId = 2, Description = "Dart" }
            );
        }
    }
}
