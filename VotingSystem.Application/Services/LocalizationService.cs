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
    public class LocalizationService : ILocalizationService
    {
        public string DetermineLanguage(HttpRequest request)
        {
            var acceptLanguageHeader = request.Headers["Accept-Language"].ToString();
            string language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            if (string.IsNullOrEmpty(acceptLanguageHeader))
            {

                 language = Constants.Arabic;
            }

            switch (language)
            {
                case Constants.Arabic:
                    return Constants.Arabic;
                case Constants.English:
                    return Constants.English;
                default:
                    return Constants.Arabic;
            }
        }

    }
}
