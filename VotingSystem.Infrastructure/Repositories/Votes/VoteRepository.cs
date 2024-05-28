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
        public async Task AddAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }
    }
}
