using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Infrastructure.Identity
{
    public class PlatFormRole : IdentityRole<Guid>
    {
        public PlatFormRole(string roleName) : base(roleName)
        {

        }
        public PlatFormRole() : base()
        {

        }
    }
}
