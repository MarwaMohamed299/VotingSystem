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

namespace VotingSystem.Infrastructure.JWTService
{
    public class JwtService :IJWTService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public (string TokenString, DateTime ExpiryDate) GenerateVotingToken()
        {
            //key
            var secretKey = _configuration["SecretKey"];
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey!);
            var key = new SymmetricSecurityKey(secretKeyInBytes);
            var generatingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            //claims
            var claims = new List<Claim>
            {
                new Claim("SessionId","##54Id23651@1"),
                new Claim("CurrentDateTime" ,DateTime.UtcNow.ToString())
            };

            var jwt = new JwtSecurityToken(
             claims: claims,
             notBefore: DateTime.Now,
             expires: DateTime.Now.AddYears(1),
             signingCredentials: generatingToken
              );

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwt);

            return (tokenString, jwt.ValidTo);

        }
    }
}
