using Microsoft.VisualBasic;
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
using VotingSystem.Domain.Constants;
using Constants = VotingSystem.Domain.Constants.Constants;
using Microsoft.AspNetCore.Http;


namespace VotingSystem.Application.Services
{
    public class PollService : IPollService
    {
        private readonly IPollRepository _repo;
        private readonly ILocalizationService _localization;

        public PollService(IPollRepository repository , ILocalizationService localization)
        {
            _repo = repository;
            _localization = localization;
        }
        public async Task<PollReadDto?> GetPollWithQuestionsAsync(int id, string language)
        {
            var pollWithQuestions = await _repo.GetPollWithQuestionsAsync(id);

            var pollDto = new PollReadDto
            {
                PollId = pollWithQuestions.PollId,
                StartDate = pollWithQuestions.StartDate,
                EndDate = pollWithQuestions.EndDate,
                Title = language == Constants.English ? pollWithQuestions.TitleEn : pollWithQuestions.TitleAr,
                Questions = pollWithQuestions.Questions.Select(q => new QuestionReadDto
                {
                    QuestionId = q.QuestionId,
                    Text = language == Constants.English ? q.TextEn : q.TextAr,
                    Options = q.Options.Select(o => new OptionReadDto
                    {
                        OptionId = o.OptionId,
                        Description = language == Constants.English ? o.DescriptionEn : o.DescriptionAr
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
                Text = q.TextEn,
                Options = q.Options.Select(o => new PollOptionsVoteCountDto
                {
                    OptionId = o.OptionId,
                    Description = o.DescriptionEn,
                    VoteCount = o.Votes.Count
                }).ToList()
            }).ToList();

            return questionDtos;
        }

        public async Task<PollAddDto> AddPollAsync(PollAddDto pollDto)
        {
            var poll = new Poll
            {
                TitleEn = pollDto.Title,
                StartDate = pollDto.StartDate,
                EndDate = pollDto.EndDate,
                Questions = pollDto.Questions.Select(q => new Question
                {
                    TextEn= q.Text,
                    Options = q.options.Select(o => new Option
                    {
                        DescriptionEn=o.Description
                    }).ToList()
                }).ToList()
            };
            await _repo.CreatePollAsync(poll);
            return pollDto;
        }

        public async Task<PollUpdateDto> UpdatePollAsync(PollUpdateDto pollDto, int id)
        {
            var poll = await _repo.GetPollByIdAsync(id);

            poll!.TitleEn = pollDto.Title;
            poll.StartDate = pollDto.StartDate;
            poll.EndDate = pollDto.EndDate;
            poll.Questions = pollDto.Questions.Select(q => new Question
            {
                TextEn = q.Text,
                Options = q.Options.Select(o => new Option
                {
                    DescriptionEn = o.Description
                }).ToList()
            }).ToList();


            await _repo.UpdatePollAsync(poll);
            return pollDto;
        }
        public async Task DeletePollAsync(int id)
        {
            var poll = await _repo.GetPollByIdAsync(id);
            await _repo.DeletePollAsync(id);

        } 
    }
}

