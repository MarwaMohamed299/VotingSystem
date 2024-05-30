using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
                RefreshToken = null,
                ExpiryDate = DateTime.MinValue,
                IsActive = false
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
                    UserId = user.Id
                };
                await _votingSystemContext.AddAsync(Voter);
                var res = await _votingSystemContext.SaveChangesAsync();
                if (res > 0)
                {
                    return new RegisterResultDto
                    {
                        Success = true
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

            var user = await _userManger.FindByNameAsync(credentials.UserName);
            if (user == null)
            {
                resultDto.IsSuccess = false;
                resultDto.Message = "User Name Or Password Isn't Correct";
                return resultDto;
            }
            if (await _userManger.IsLockedOutAsync(user))
            {
                resultDto.IsSuccess = false;
                resultDto.Message = "User Is Locked, Try again after 10 minutes";
                return resultDto;
            }
            if (!(await _userManger.CheckPasswordAsync(user, credentials.Password)))
            {
                await _userManger.AccessFailedAsync(user);
                resultDto.IsSuccess = false;
                resultDto.Message = "User Name Or Password Isn't Correct";
                return resultDto;
            }

            // Key Generation
            var secretKey = _config["SecretKey"];
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey!);
            var key = new SymmetricSecurityKey(secretKeyInBytes);
            // Hashing 
            var generatingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var userClaims = await _userManger.GetClaimsAsync(user);

            // Generate token
            var jwt = new JwtSecurityToken(
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: generatingToken
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwt);
            resultDto.IsSuccess = true;
            resultDto.Message = "Login Successfully";
            resultDto.Token = tokenString;
            resultDto.ExpiryDate = jwt.ValidTo;

            // Refresh token handling
            if (!string.IsNullOrEmpty(user.RefreshToken) && user.ExpiryDate > DateTime.Now && user.IsActive)
            {
                // There is an active refresh token
                resultDto.RefreshToken = user.RefreshToken;
            }
            else
            {
                // Generate and save a new refresh token
                var refreshTokenString = GenerateRefreshTokenString();
                user.RefreshToken = refreshTokenString;
                user.ExpiryDate = DateTime.Now.AddDays(7);
                user.IsActive = true;

                await _userManger.UpdateAsync(user);

                resultDto.RefreshToken = refreshTokenString;
            }

            return resultDto;
        }

        private string GenerateRefreshTokenString()
        {
            var random = new byte[64];
            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(random);
            }
            return Convert.ToBase64String(random);
        }

    }
}
