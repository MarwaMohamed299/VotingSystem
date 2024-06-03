using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Models.Options;

namespace VotingSystem.Application.Models.Questions
{
    public class QuestionAddDto
    {
        public string Text { get; set; } = string.Empty;
        public List<OptionAddDto> options { get; set; } = new List<OptionAddDto>();

    }
}
