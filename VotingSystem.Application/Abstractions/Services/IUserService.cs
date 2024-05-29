using Microsoft.AspNetCore.Identity;
using VotingSystem.Application.Models.Identity;

namespace VotingSystem.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<LoginResultDto> LogIn(LoginDto credentials);
        Task<IEnumerable<IdentityError>> Register(RegisterDto userFromRequest);
    }
}