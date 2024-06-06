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
        private List<Vote> _votesToAdd = new List<Vote>();

        public VotesService(IVoteRepository  repo , IHttpContextAccessor contextAccessor)
        {
            _repo = repo;
            _contextAccessor = contextAccessor;
        }
        public async Task<List<VoteAddDto>> AddAsync(List<VoteAddDto> votes)
        {
            var sessionId = Guid.NewGuid().ToString();

            _contextAccessor.HttpContext.Response.Cookies.Append("sessionid", sessionId);

            foreach (var vote in votes)
            {
                var voteEntity = new Vote
                {
                    OptionId = vote.OptionId,
                    VoterId = vote.VoterId,
                    VoteDate = vote.VoteDate,
                    SessionIdentifier = sessionId
                };

               _votesToAdd.Add(voteEntity);
            }
            await _repo.AddAsync(_votesToAdd);
            return votes;
        }
    }
}
