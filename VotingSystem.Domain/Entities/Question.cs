using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Domain.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string TextAr { get; set; } = string.Empty;
        public string TextEn { get; set; } = string.Empty;

        // Navigation Property
        public int PollId { get; set; }
        public Poll? Poll { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
    }
}
