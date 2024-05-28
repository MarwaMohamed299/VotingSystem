using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Infrastructure.Data.Context;
using VotingSystem.Infrastructure.Repositories.Polls;

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
            #endregion

            return services;
        }
    }
}
