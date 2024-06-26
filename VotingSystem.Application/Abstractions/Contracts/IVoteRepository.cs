﻿using VotingSystem.Domain.Entities;

namespace VotingSystem.Infrastructure.Repositories.Votes
{
    public interface IVoteRepository
    {
        Task AddAsync(List<Vote> votes);
    }
}