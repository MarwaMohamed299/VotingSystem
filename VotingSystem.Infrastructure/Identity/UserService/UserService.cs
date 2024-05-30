using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Models.Identity;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Data.Context;
using VotingSystem.Infrastructure.Identity.Models;

namespace VotingSystem.Infrastructure.Identity.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<PlatFormUser> _userManger;
        private readonly VotingSystemContext _votingSystemContext;
        private readonly IConfiguration _config;

        public UserService(UserManager<PlatFormUser> userManager, VotingSystemContext votingSystemContextt, IConfiguration configuration)
        {
            _userManger = userManager;
            _votingSystemContext = votingSystemContextt;
            _config = configuration;
        }

        public async Task<RegisterResultDto> Register(RegisterDto userFromRequest)
        {
            PlatFormUser user = new PlatFormUser
            {
                UserName = userFromRequest.UserName,
                Email = userFromRequest.Email,
            };
            var RegisterResult = await _userManger.CreateAsync(user, userFromRequest.Password);
            if (!RegisterResult.Succeeded)
            {
                return new RegisterResultDto
                {
                    Success = false
                };
            }
            else
            {
                var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userFromRequest.UserName),
                new Claim (ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email, userFromRequest.Email),
                new Claim("Nationality", "Egyptian"),
            };
                await _userManger.AddClaimsAsync(user, userClaims);
                var Voter = new Voter
                {
                    hasSubmitted = false,
                };
                await _votingSystemContext.AddAsync(Voter);
                var res = await _votingSystemContext.SaveChangesAsync();
                if (res > 0)
                {
                    return new RegisterResultDto
                    {
                        Success = false
                    };
                }
                else
                {
                    await _userManger.DeleteAsync(user);
                    return null!;
                }
            }
        }

        public async Task<LoginResultDto> LogIn(LoginDto credentials)
        {
            LoginResultDto resultDto = new LoginResultDto();

            var User = await _userManger.FindByNameAsync(credentials.UserName);
            if (User == null)
            {
                resultDto.IsSuccess = false;
                resultDto.Message = "User Name Or Password Isn't Correct";
                return resultDto;
            }
            if (await _userManger.IsLockedOutAsync(User))
            {
                resultDto.IsSuccess = false;
                resultDto.Message = "User Is Locked, Try again after 10 minutes";
                return resultDto;
            }
            if (!(await _userManger.CheckPasswordAsync(User, credentials.Password)))
            {
                await _userManger.AccessFailedAsync(User);
                resultDto.IsSuccess = false;
                resultDto.Message = "User Name Or Password Isn't Correct";
                return resultDto;
            }

            //Key Generation

            var SecretKey = _config["SecretKey"];
            var secretKeyInBytes = Encoding.ASCII.GetBytes(SecretKey!);
            var Key = new SymmetricSecurityKey(secretKeyInBytes);
            //Hashing 
            var generatingToken = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            var userClaims = await _userManger.GetClaimsAsync(User);

            //Generate token
            var jwt = new JwtSecurityToken
                (
                    claims: userClaims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: generatingToken
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwt);
            resultDto.IsSuccess = true;
            resultDto.Message = "Login Successfully";
            resultDto.Token = tokenString;
            resultDto.ExpiryDate = jwt.ValidTo;
            return resultDto;


        }

    }
}
