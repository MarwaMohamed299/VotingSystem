using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Domain.Entities
{
    public class Option
    {
        public int OptionId { get; set; }
        public string Description { get; set; } = string.Empty;
        //NavProp
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();

    }
}
