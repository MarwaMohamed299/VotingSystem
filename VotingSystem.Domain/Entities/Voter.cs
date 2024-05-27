using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Domain.Entities
{
    public class Voter 
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        //NavProp
        public Guid UserId { get; set; } 
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();


    }
}
