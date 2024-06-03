using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Models.Options;
using VotingSystem.Application.Models.Questions;

namespace VotingSystem.Application.Models.Polls
{
    public class PollUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<QuestionUpdateDto> Questions { get; set; } = new List<QuestionUpdateDto>();
    }
}
