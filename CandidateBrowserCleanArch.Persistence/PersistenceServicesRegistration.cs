using CandidateBrowserCleanArch.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateBrowserCleanArch.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, 
        IConfiguration configuration) 
    {
        services.AddDbContext<CandidatesBrowserDbContext>(options =>
        options.UseSqlServer(configuration["ConnectionStrings:CandidatesBrowserConnString"]));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    
    }
}
