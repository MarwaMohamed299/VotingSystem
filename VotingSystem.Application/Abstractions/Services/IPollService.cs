using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Models.Polls;

namespace VotingSystem.Application.Abstractions.Services
{
    public interface IPollService
    {
        Task<PollReadDto?> GetPollWithOptionsAsync(int id);
    }
}
