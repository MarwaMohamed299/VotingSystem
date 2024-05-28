using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Models.Options;
using VotingSystem.Application.Models.Polls;
using VotingSystem.Infrastructure.Repositories;
using VotingSystem.Infrastructure.Repositories.Polls;

namespace VotingSystem.Application.Services
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _repo;

        public PollService(IPollRepository repository)
        {
            _repo = repository;
        }
        public async Task<PollReadDto?> GetPollWithOptionsAsync(int id)
        {
            var pollWithOptions = await _repo.GetPollWithOptionsAsync(id);
            if (pollWithOptions == null)
            {
                return null;
            }

            var pollDto = new PollReadDto
            {
                PollId = pollWithOptions.PollId,
                StartDate = pollWithOptions.StartDate,
                EndDate = pollWithOptions.EndDate,
                Title = pollWithOptions.Title,
                Options = pollWithOptions.Options.Select(o => new OptionReadDto
                {
                    OptionId = o.OptionId,
                   Description = o.Description
                }).ToList()
            };

            return pollDto;
        }
    }
}
