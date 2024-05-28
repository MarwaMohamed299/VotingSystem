using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Application.Models.Options
{
    public class OptionReadDto
    {
        public int OptionId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
