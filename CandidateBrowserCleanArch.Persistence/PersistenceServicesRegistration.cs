using CandidateBrowserCleanArch.Application.Contracts.Persistence;
using CandidateBrowserCleanArch.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, 
            IConfiguration configuration) 
        {
            services.AddDbContext<CandidatesBrowserDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CandidatesBrowserConnString")));
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            return services;
        
        }
    }
}
