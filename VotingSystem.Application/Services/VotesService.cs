using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _contextAccessor;

        public VotesService(IVoteRepository  repo , IHttpContextAccessor contextAccessor)
        {
            _repo = repo;
            _contextAccessor = contextAccessor;
        }
        public async Task<VoteAddDto> AddAsync(VoteAddDto voteAddDto)
        {
            var sessionId = Guid.NewGuid().ToString();

            _contextAccessor.HttpContext.Response.Cookies.Append("sessionid", sessionId);

            var vote = new Vote
            {
                OptionId = voteAddDto.OptionId,
                VoterId = voteAddDto.VoterId,
                VoteDate = voteAddDto.VoteDate,
                SessionIdentifier = sessionId
            };
            await _repo.AddAsync(vote);
            return voteAddDto;
        }
    }
}
