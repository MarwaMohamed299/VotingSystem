using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using VotingSystem.Application.Abstractions.Services;
using VotingSystem.Infrastructure.Data.Context;
using VotingSystem.Infrastructure.Identity.UserService;
using VotingSystem.Infrastructure.JWTService;
using VotingSystem.Infrastructure.Repositories.Polls;
using VotingSystem.Infrastructure.Repositories.Votes;

namespace VotingSystem.Infrastructure
{
    public static class InfraStructureConfigurationServiceExtension
    { public static IServiceCollection AddInfraStructureConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
        {

            #region Connection String 

            var ConnectionString = configuration.GetConnectionString("VotingSystem");
            services.AddDbContext<VotingSystemContext>(options => options.UseSqlServer(ConnectionString));

            #endregion

            #region Repos
            services.AddScoped<IPollRepository, PollRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<IJWTService, JwtService>();
            #endregion

            #region Identity
            services.AddScoped<IUserService, UserService>();
            #endregion

           
            return services;
        }
    }
}
