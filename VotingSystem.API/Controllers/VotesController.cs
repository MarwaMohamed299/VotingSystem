using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Models.Votes;
using VotingSystem.Application.Services;
using VotingSystem.Infrastructure.Repositories.Votes;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService _service;

        public VotesController(IVoteService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> AddVotes([FromBody] List<VoteAddDto> votes)
        {
            var (addedVotes, tokenString) = await _service.AddAsync(votes);
            return Ok(new
            {
                Votes = addedVotes,
                Token = tokenString
            });
        }
    }
}
