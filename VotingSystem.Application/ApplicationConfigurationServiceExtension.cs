using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Application.Services;

namespace VotingSystem.Application
{
    public static class ApplicationConfigurationServiceExtension
    {
        public static IServiceCollection AddApplicationConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            #region services
            services.AddScoped<IPollService, PollService>();
            services.AddScoped<IVoteService, VotesService>();

            #endregion
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
