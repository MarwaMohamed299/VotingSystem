using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Models.Options;
using VotingSystem.Application.Models.Polls;
using VotingSystem.Domain.Entities;

namespace VotingSystem.Application.Abstractions.Services
{
    public interface IPollService
    {
        Task<PollReadDto?> GetPollWithQuestionsAsync(int id);
        Task<List<PollQuestionDto>> GetVotesCountForOptionsAsync(int pollId);
    }
}
