using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions.Services;

namespace VotingSystem.Application.Services
{
    public class LocalizationService :ILocalizationService
    {
        public string DetermineLanguage( string language)
        {
            switch (language)
            {
                case "en":
                case "ar":
                    return language;
                default:
                    return "ar";
            }

        }

    }
}
