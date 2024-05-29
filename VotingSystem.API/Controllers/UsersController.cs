using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Models.Identity;
namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<RegisterResultDto>> RegisterAsync(RegisterDto registerDto)
        {
            var result = await _userService.Register(registerDto);
            return Ok(result);

        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResultDto>> Login(LoginDto credentials)
        {
            var result = await _userService.LogIn(credentials);
            return Ok(result);

        }
    }
}
