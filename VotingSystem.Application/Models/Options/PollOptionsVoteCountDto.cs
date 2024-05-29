using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Application.Models.Options
{
    public class PollOptionsVoteCountDto
    {
        public int OptionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int VoteCount { get; set; }
    }
}
