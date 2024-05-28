using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Application.Models.Votes
{
    public class VoteAddDto
    {
        public int VoterId { get; set; }
        public int OptionId { get; set; }
        public DateTime VoteDate { get; set; }
    }
}
