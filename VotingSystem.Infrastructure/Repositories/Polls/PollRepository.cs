﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Data.Context;

namespace VotingSystem.Infrastructure.Repositories.Polls
{
    public class PollRepository : IPollRepository
    {
        private readonly VotingSystemContext _context;

        public PollRepository(VotingSystemContext context)
        {
            _context = context;
        }
        public async Task CreatePollAsync(Poll poll)
        {
            await _context.Polls.AddAsync(poll);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePollAsync(Poll poll)
        {
            _context.Polls.Update(poll);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePollAsync(int id)
        {
            var poll = await GetPollByIdAsync(id);

            _context.Options.RemoveRange(poll!.Questions.SelectMany(q => q.Options));

            _context.Questions.RemoveRange(poll.Questions);

            _context.Polls.Remove(poll);

            await _context.SaveChangesAsync();
        }
        public async Task<Poll?> GetPollByIdAsync(int id)
        {
            return await _context.Polls
                                     .Include(p => p.Questions)
                                     .ThenInclude(q => q.Options)
                                     .FirstOrDefaultAsync(p => p.PollId == id);
        }
        public async Task<Poll> GetPollWithQuestionsAsync(int pollId)
        {
            var pollWitOptions = await _context.Polls
                .Include(p => p.Questions)
                .ThenInclude(a=>a.Options)
                .FirstOrDefaultAsync(p => p.PollId == pollId);
            return pollWitOptions ?? null!;
        }
        public async Task<Poll> GetVotesForEachPullAsync(int pollId)
        {
            var poll = await _context.Polls
                .Include(p => p.Questions)
                .ThenInclude(q => q.Options)
                .ThenInclude(o => o.Votes)
                .FirstOrDefaultAsync(p => p.PollId == pollId);
            return poll ?? null!;
        }
    }
}
