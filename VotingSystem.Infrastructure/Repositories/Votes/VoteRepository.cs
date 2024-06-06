using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions.Contracts;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Data.Context;

namespace VotingSystem.Infrastructure.Repositories.Votes
{
    public class VoteRepository : IVoteRepository
    {
        private readonly VotingSystemContext _context;

        public VoteRepository(VotingSystemContext context) 
        {
            _context = context;
        }
        public async Task AddAsync(List<Vote> votes)
        {
            await _context.Votes.AddRangeAsync(votes);
            await _context.SaveChangesAsync();
        }
       
    }
}
