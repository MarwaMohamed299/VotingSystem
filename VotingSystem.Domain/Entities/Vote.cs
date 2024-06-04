using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Domain.Entities
{
    public class Vote
    {
        public int VoteId { get; set; }
        public int? UserId { get; set; }
        public int OptionId { get; set; }
        public DateTime VoteDate { get; set; }
        //NavProp
        public Voter? Voter { get; set; }
        public Option? Option { get; set; }
    }
}
