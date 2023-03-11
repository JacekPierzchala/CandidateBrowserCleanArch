using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CandidateBrowserCleanArch.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


namespace CandidateBrowserCleanArch.Identity;

public static class IdentityServicesRegistration
{
    public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddDbContext<CandidateBrowserCleanArchIdentityDbContext>(opt =>
        opt.UseSqlServer(configuration.GetConnectionString("CandidatesBrowserConnString"),
        build => build.MigrationsAssembly(typeof(CandidateBrowserCleanArchIdentityDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<CandidateBrowserCleanArchIdentityDbContext>().AddDefaultTokenProviders();
        
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IJwtService, JwtService>();

        services.ConfigureAuthentication(configuration);

        return services;
    }
}
