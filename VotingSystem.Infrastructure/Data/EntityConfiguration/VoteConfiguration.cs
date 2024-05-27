using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Domain.Entities;

namespace VotingSystem.Infrastructure.Data.EntityConfiguration
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasOne(v => v.Option)
                              .WithMany(o => o.Votes)
                              .HasForeignKey(v => v.OptionId)
                              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Voter)
                   .WithMany(vt => vt.Votes)
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
