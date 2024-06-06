using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Models.Votes;
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
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteAddDto>> AddVote(List<VoteAddDto> votes)
        {
            var result = await _service.AddAsync(votes);
            return Ok(result);
        }
    }
}
