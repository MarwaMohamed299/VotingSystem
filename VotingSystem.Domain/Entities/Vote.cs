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
        public int OptionId { get; set; }
        public string SessionIdentifier { get; set; } = string.Empty;
        public DateTime VoteDate { get; set; }
        //NavProp
        public Guid? UserId { get; set; }
        public int? VoterId { get; set; }
        public Voter? Voter { get; set; }
        public Option? Option { get; set; }
    }
}
