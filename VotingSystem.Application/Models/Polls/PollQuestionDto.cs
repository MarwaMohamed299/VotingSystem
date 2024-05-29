using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Models.Options;

namespace VotingSystem.Application.Models.Polls
{
    public class PollQuestionDto
    {
        public int QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<PollOptionsVoteCountDto> Options { get; set; } = new List<PollOptionsVoteCountDto>();

    }
}
