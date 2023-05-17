using CandidateBrowserCleanArch.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CandidateBrowserCleanArch.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, 
        IConfiguration configuration, IWebHostEnvironment environment) 
    {
        var connString = string.Empty;
        if (environment.IsProduction())
        {
            connString = configuration["ConnectionStrings:CandidatesBrowserConnString"];
        }
        else
        {
            connString = configuration.GetConnectionString("CandidatesBrowserNewDev");
        }

        services.AddDbContext<CandidatesBrowserDbContext>(options =>
        options.UseSqlServer(connString));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    
    }
}
