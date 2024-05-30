using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Infrastructure.Data.Context;

namespace VotingSystem.Infrastructure.Repositories.Voters
{
    public class VoterRepository
    {
        private readonly VotingSystemContext _context;

        public VoterRepository(VotingSystemContext context)
        {
            _context = context;
        }
        public async Task GetVoteSubmissionStatue( int voterId)
        {
            var status = await _context.Voters.Where(v => v.Id == voterId)
                .Select(v => v.hasSubmitted)
                .FirstOrDefaultAsync();
        }

    }
}
