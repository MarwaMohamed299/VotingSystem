using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Application.Models.Identity
{
    public class LoginResultDto
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? RefreshToken { get; set; } 
    }
}
