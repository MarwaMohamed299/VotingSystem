using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Identity;

namespace VotingSystem.Infrastructure.Context
{
    public class VotingSystemContext : IdentityDbContext<PlatFormUser,PlatFormRole ,Guid>
    {
        public  DbSet<Voter> Voters => Set<Voter>();
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public VotingSystemContext(DbContextOptions<VotingSystemContext> Options) : base(Options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
