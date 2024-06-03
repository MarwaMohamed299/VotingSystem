using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Infrastructure.Identity.Models
{
    public class PlatFormUser : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsExpired => DateTime.Now >= ExpiryDate;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
