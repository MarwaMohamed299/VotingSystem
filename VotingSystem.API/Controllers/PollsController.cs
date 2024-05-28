using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application.Abstractions.Services;
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
            var poll = await _poll.GetPollWithOptionsAsync(id);            
            return Ok(poll);
        }
    }
}
