using Microsoft.AspNetCore.Http;
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
        private readonly UserManager<PlatFormUser> _userManager;
        private readonly VotingSystemContext _votingSystemContext;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<PlatFormUser> userManager, VotingSystemContext votingSystemContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _votingSystemContext = votingSystemContext;
            _config = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegisterResultDto> Register(RegisterDto userFromRequest)
        {
            PlatFormUser user = new PlatFormUser
            {
                UserName = userFromRequest.UserName,
                Email = userFromRequest.Email,
            };
            var registerResult = await _userManager.CreateAsync(user, userFromRequest.Password);
            if (!registerResult.Succeeded)
            {
                return new RegisterResultDto
                {
                    Success = false
                };
            }

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userFromRequest.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, userFromRequest.Email),
                new Claim("Nationality", "Egyptian"),
            };
            await _userManager.AddClaimsAsync(user, userClaims);

            var voter = new Voter
            {
                hasSubmitted = false,
                UserId = user.Id
            };
            await _votingSystemContext.AddAsync(voter);
            var res = await _votingSystemContext.SaveChangesAsync();
            if (res > 0)
            {
                return new RegisterResultDto
                {
                    Success = true
                };
            }

            await _userManager.DeleteAsync(user);
            return new RegisterResultDto { Success = false };
        }

        public async Task<LoginResultDto> LogIn(LoginDto credentials)
        {
            var resultDto = new LoginResultDto();

            var user = await _userManager.FindByNameAsync(credentials.UserName);
            if (user == null || await _userManager.IsLockedOutAsync(user) || !await _userManager.CheckPasswordAsync(user, credentials.Password))
            {
                resultDto.IsSuccess = false;
                resultDto.Message = user == null || !await _userManager.CheckPasswordAsync(user, credentials.Password) ? "User Name Or Password Isn't Correct" : "User Is Locked, Try again after 10 minutes";
                if (user != null && !await _userManager.CheckPasswordAsync(user, credentials.Password))
                {
                    await _userManager.AccessFailedAsync(user);
                }
                return resultDto;
            }

            return await GenerateTokensForUser(user);
        }

        public async Task<LoginResultDto> RefreshToken(string refreshToken)
        {
            var resultDto = new LoginResultDto();
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshToken == refreshToken);

            if (user == null || user.IsExpired)
            {
                resultDto.IsSuccess = false;
                resultDto.Message = "Invalid refresh token";
                return resultDto;
            }

            return await GenerateTokensForUser(user);
        }

        private async Task<LoginResultDto> GenerateTokensForUser(PlatFormUser user)
        {
            var resultDto = new LoginResultDto();
            var token = GenerateJwtToken(user);
            resultDto.IsSuccess = true;
            resultDto.Message = "Login Successfully";
            resultDto.Token = token.TokenString;
            resultDto.RefreshTokenExpiryDate = token.ExpiryDate;

            if (!string.IsNullOrEmpty(user.RefreshToken) && user.ExpiryDate > DateTime.Now && user.IsActive)
            {
                resultDto.RefreshToken = user.RefreshToken;
            }
            else
            {
                var refreshTokenString = GenerateRefreshTokenString();
                user.RefreshToken = refreshTokenString;
                user.ExpiryDate = DateTime.Now.AddDays(7);
                user.CreatedOn = DateTime.Now;

                await _userManager.UpdateAsync(user);
                resultDto.RefreshToken = refreshTokenString;
            }

           //cookies
            SetRefreshTokenInCookie(resultDto.RefreshToken, user.ExpiryDate);

            return resultDto;
        }

        private (string TokenString, DateTime ExpiryDate) GenerateJwtToken(PlatFormUser user)
        {
            var secretKey = _config["SecretKey"];
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey!);
            var key = new SymmetricSecurityKey(secretKeyInBytes);
            var generatingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var userClaims = _userManager.GetClaimsAsync(user).Result;

            var jwt = new JwtSecurityToken(
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: generatingToken
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwt);

            return (tokenString, jwt.ValidTo);
        }

        private  string GenerateRefreshTokenString()
        {
            var random = new byte[64];
            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(random);
            }
            return Convert.ToBase64String(random);
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expiryDate)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expiryDate 
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
