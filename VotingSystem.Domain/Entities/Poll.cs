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
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //NavProp
        public List<Question> Questions { get; set; } = new List<Question>();


    }
}
