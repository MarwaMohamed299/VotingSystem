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
        public bool hasSubmitted { get; set; }

        //NavProp
        public Guid UserId { get; set; } 
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();


    }
}
