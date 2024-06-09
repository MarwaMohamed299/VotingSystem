using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Application.Abstractions.Services
{
    public interface IJWTService
    {
        public (string TokenString, DateTime ExpiryDate) GenerateVotingToken();
    }
}
