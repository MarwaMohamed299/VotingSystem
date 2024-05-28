using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Models.Votes;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Repositories.Votes;

namespace VotingSystem.Application.Services
{
    public class VotesService : IVoteService
    {
        private readonly IVoteRepository _repo;

        public VotesService(IVoteRepository  repo)
        {
            _repo = repo;
        }
        public async Task<VoteAddDto> AddAsync(VoteAddDto voteAddDto)
        {
            var vote = new Vote
            {
                OptionId = voteAddDto.OptionId,
                UserId = voteAddDto.VoterId,
                VoteDate = voteAddDto.VoteDate
            };
            await _repo.AddAsync(vote);
            return voteAddDto;
        }
    }
}
