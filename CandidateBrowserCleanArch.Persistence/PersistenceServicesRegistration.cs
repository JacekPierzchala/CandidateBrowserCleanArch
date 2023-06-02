using CandidateBrowserCleanArch.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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

        services.AddMemoryCache();

        services.AddDbContext<CandidatesBrowserDbContext>(options =>
        options.UseSqlServer(connString));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<CandidateRepository>();      
        services.AddScoped<ICandidateRepository>(provider =>
        {
            return new CandidateCachedRepository(
                provider.GetService<CandidatesBrowserDbContext>()!,
                provider.GetService<CandidateRepository>()!, provider.GetService<IMemoryCache>()!);
        });

        services.AddScoped<CompanyRepository>();
        services.AddScoped<ICompanyRepository>(provider => 
        {               
            return new CompanyCachedRepository(provider.GetService<IMemoryCache>()!, 
                provider.GetService<CompanyRepository>()!,
                provider.GetService<CandidatesBrowserDbContext>()!);

        });

        services.AddScoped<ProjectRepository>();
        services.AddScoped<IProjectRepository>(provider =>
        {
            return new ProjectCachedRepository(provider.GetService<CandidatesBrowserDbContext>()!,
                provider.GetService<ProjectRepository>()!
                , provider.GetService<IMemoryCache>()!);
        });

        services.AddScoped<ICandidateCompanyRepository, CandidateCompanyRepository>();
        services.AddScoped<ICandidateProjectRepository, CandidateProjectRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    
    }
}
