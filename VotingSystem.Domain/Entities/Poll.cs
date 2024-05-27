using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Domain.Entities
{
    public class Poll
    {
        public int PollId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //NavProp
        public List<Option> Options { get; set; } = new List<Option>();

    }
}
