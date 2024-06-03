using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Models.Options;
using VotingSystem.Application.Models.Polls;
using VotingSystem.Application.Models.Questions;
using VotingSystem.Domain.Entities;
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
        public async Task<PollReadDto?> GetPollWithQuestionsAsync(int id)
        {
            var pollWithQuestions = await _repo.GetPollWithQuestionsAsync(id);

            var pollDto = new PollReadDto
            {
                PollId = pollWithQuestions.PollId,
                StartDate = pollWithQuestions.StartDate,
                EndDate = pollWithQuestions.EndDate,
                Title = pollWithQuestions.Title,
                Questions = pollWithQuestions.Questions.Select(q => new QuestionReadDto
                {
                    QuestionId = q.QuestionId,
                    Text = q.Text,
                    Options = q.Options.Select(o => new OptionReadDto
                    {
                        OptionId = o.OptionId,
                        Description = o.Description
                    }).ToList()
                }).ToList()
            };

            return pollDto;
        }

        public async Task<List<PollQuestionDto>> GetVotesCountForOptionsAsync(int pollId)
        {
            var poll = await _repo.GetVotesForEachPullAsync(pollId);

            var questionDtos = poll.Questions.Select(q => new PollQuestionDto
            {
                QuestionId = q.QuestionId,
                Text = q.Text,
                Options = q.Options.Select(o => new PollOptionsVoteCountDto
                {
                    OptionId = o.OptionId,
                    Description = o.Description,
                    VoteCount = o.Votes.Count
                }).ToList()
            }).ToList();

            return questionDtos;
        }

        public async Task<PollAddDto> AddPollAsync(PollAddDto pollDto)
        {
            var poll = new Poll
            {
                Title = pollDto.Title,
                StartDate = pollDto.StartDate,
                EndDate = pollDto.EndDate,
                Questions = pollDto.Questions.Select(q => new Question
                {
                    Text = q.Text,
                    Options = q.options.Select(o => new Option
                    {
                        Description=o.Description
                    }).ToList()
                }).ToList()
            };
            await _repo.CreatePollAsync(poll);
            return pollDto;
        }

        public async Task<PollUpdateDto> UpdatePollAsync(PollUpdateDto pollDto, int id)
        {
            var poll = await _repo.GetPollByIdAsync(id);

            poll.Title = pollDto.Title;


            await _repo.UpdatePollAsync(poll);

        }
    }
}

