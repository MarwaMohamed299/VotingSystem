﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Models.Options;
using VotingSystem.Application.Models.Polls;
using VotingSystem.Application.Services;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _poll;

        public PollsController(IPollService pollservice)
        {
            _poll = pollservice;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PollReadDto>> GetPoll(int id)
        {
            var poll = await _poll.GetPollWithQuestionsAsync(id);            
            return Ok(poll);
        }
        [HttpGet("{pollId}/votes")]
        public async Task<ActionResult<List<PollOptionsVoteCountDto>>> GetVotesCountForOptions(int pollId)
        {
            var optionVoteCounts = await _poll.GetVotesCountForOptionsAsync(pollId);
            return Ok(optionVoteCounts);
        }
        [HttpPost]
        public async Task<ActionResult> AddPoll([FromBody] PollAddDto pollDto)
        {
            var poll = await _poll.AddPollAsync(pollDto);
            return Ok(poll);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> EditPoll(int id, [FromBody] PollUpdateDto pollDto)
        {
            var poll = await _poll.UpdatePollAsync(pollDto, id);
            return Ok(poll);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePoll(int id)
        {
            await _poll.DeletePollAsync(id);
            return Ok();
        }

    }
}
