using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Domain.Constants;

namespace VotingSystem.Application.Services
{
    public class LocalizationService :ILocalizationService
    {
        public string DetermineLanguage( string language)
        {
            switch (language)
            {
                case Constants.Arabic:
                case Constants.English:
                    return language;
                default:
                    return Constants.Arabic;
            }

        }

    }
}
