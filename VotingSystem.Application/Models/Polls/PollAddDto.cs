using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Models.Options;
using VotingSystem.Application.Models.Questions;

namespace VotingSystem.Application.Models.Polls
{
    public class PollAddDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<QuestionAddDto> Questions { get; set; } = new List<QuestionAddDto>();
        public List<OptionAddDto> Options { get; set; } = new List<OptionAddDto>();
    }
}
