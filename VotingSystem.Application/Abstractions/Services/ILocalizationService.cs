using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Application.Abstractions.Services
{
    public interface ILocalizationService
    {
        string DetermineLanguage(string language);
    }
}
