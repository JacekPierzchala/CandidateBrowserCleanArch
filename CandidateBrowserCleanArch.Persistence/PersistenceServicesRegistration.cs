using CandidateBrowserCleanArch.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
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
        options.UseSqlServer(connString)
                .UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddSerilog();
                })));
            
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICandidateRepository,CandidateRepository>();


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

        services.AddScoped<ConfigThemeRepository>();
        services.AddScoped<IConfigThemeRepository>(provider => 
        {
            return new ConfigThemeCachedRepository(
                provider.GetService<CandidatesBrowserDbContext>()!,
                provider.GetService<ConfigThemeRepository>()!,
                provider.GetService<IMemoryCache>()!);
        
        });

        services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    
    }
}
