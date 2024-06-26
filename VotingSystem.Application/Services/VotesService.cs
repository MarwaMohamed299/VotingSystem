﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Models.Votes;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure.Repositories.Votes;

namespace VotingSystem.Application.Services
{
    public class VotesService : IVoteService
    {
        private readonly IVoteRepository _repo;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IJWTService _jwt;
        private List<Vote> _votesToAdd = new List<Vote>();

        public VotesService(IVoteRepository repo, IHttpContextAccessor contextAccessor, IJWTService jWTService)
        {
            _repo = repo;
            _contextAccessor = contextAccessor;
            _jwt = jWTService;
        }
        public async Task<(List<VoteAddDto>, string)> AddAsync(List<VoteAddDto> votes)
        {
            // Generate JWT token
            var (tokenString, expiryDate) = _jwt.GenerateVotingToken();

            //// Set token in cookie
            _contextAccessor.HttpContext.Response.Cookies.Append("jwtToken", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = expiryDate
            }); 

            foreach (var vote in votes)
            {
                var voteEntity = new Vote
                {
                    OptionId = vote.OptionId,
                    VoterId = vote.VoterId,
                    VoteDate = vote.VoteDate,
                    SessionIdentifier = tokenString
                };

                _votesToAdd.Add(voteEntity);
            }

            await _repo.AddAsync(_votesToAdd);
            _votesToAdd.Clear();

            

            return (votes, tokenString);
        }
    }
}

