using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Application.Models.Identity
{
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
