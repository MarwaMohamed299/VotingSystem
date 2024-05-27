using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Domain.Entities;

namespace VotingSystem.Infrastructure.EntityConfiguration
{
    public class VoterConfiguration : IEntityTypeConfiguration<Voter>
    {
        public void Configure(EntityTypeBuilder<Voter> builder)
        {
            builder.HasMany(v => v.Votes)
                               .WithOne(v => v.Voter)
                               .HasForeignKey(v => v.UserId)
                               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
