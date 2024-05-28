using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Identity;

namespace VotingSystem.Infrastructure.Data.Context
{
    public class VotingSystemContext : IdentityDbContext<PlatFormUser, PlatFormRole, Guid>
    {
        public DbSet<Voter> Voters => Set<Voter>();
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public VotingSystemContext(DbContextOptions<VotingSystemContext> Options) : base(Options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Poll>().HasData(
               new Poll
               {
                   PollId = 1,
                   Title = "Favorite Programming Language",
                   StartDate = DateTime.Now,
                   EndDate = DateTime.Now.AddDays(10)
               },
               new Poll
               {
                   PollId = 2,
                   Title = "Best IDE",
                   StartDate = DateTime.Now,
                   EndDate = DateTime.Now.AddDays(10)
               }
           );
            modelBuilder.Entity<Option>().HasData(
             new Option { OptionId = 1, PollId = 1, Description = "C#" },
             new Option { OptionId = 2, PollId = 1, Description = "Java" },
             new Option { OptionId = 3, PollId = 1, Description = "Python" },
             new Option { OptionId = 4, PollId = 1, Description = "JavaScript" },
             new Option { OptionId = 5, PollId = 2, Description = "Visual Studio" },
             new Option { OptionId = 6, PollId = 2, Description = "IntelliJ IDEA" },
             new Option { OptionId = 7, PollId = 2, Description = "PyCharm" },
             new Option { OptionId = 8, PollId = 2, Description = "VS Code" }
         );
        }
    }
}
