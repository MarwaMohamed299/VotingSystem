using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Models.Options;
using VotingSystem.Domain.Entities;

namespace VotingSystem.Application.Models.Polls
{
    public class PollReadDto
    {
        public int PollId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //NavProp
        public List<OptionReadDto> Options { get; set; } = new List<OptionReadDto>();
    }
}
