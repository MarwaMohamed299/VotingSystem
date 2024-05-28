using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Models.Votes;

namespace VotingSystem.Application.Abstractions.Services
{
    public interface IVoteService
    {
        Task<VoteAddDto> AddAsync(VoteAddDto voteAddDto);
    }
}
