﻿using Microsoft.Extensions.Configuration;
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
using CandidateBrowserCleanArch.Identity.Helpers;
using System.Net;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using CandidateBrowserCleanArch.Identity.Interfaces;
using CandidateBrowserCleanArch.Identity.Services.Auth;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateBrowserCleanArch.Identity;

public static class IdentityServicesRegistration
{
    public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {

        var connString=string.Empty;
        if (environment.IsProduction())
        {
            connString=configuration["ConnectionStrings:CandidatesBrowserConnString"];
        }
        else
        {
            connString=configuration.GetConnectionString("CandidatesBrowserNewDev");
        }
        services.AddDbContext<CandidateBrowserCleanArchIdentityDbContext>(opt =>


        opt.UseSqlServer(connString,
        build => build.MigrationsAssembly(typeof(CandidateBrowserCleanArchIdentityDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>(opt => 
        {
            opt.SignIn.RequireConfirmedEmail = true;
            opt.User.RequireUniqueEmail= true;
            opt.Tokens.EmailConfirmationTokenProvider="emailconfirmation";
            opt.Tokens.PasswordResetTokenProvider = "passwordchange";
        })
        .AddEntityFrameworkStores<CandidateBrowserCleanArchIdentityDbContext>()
        .AddDefaultTokenProviders()
        .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation")
        .AddTokenProvider<PasswordResetTokenProvider<ApplicationUser>>("passwordchange");

        services.AddMemoryCache();


        services.Configure<DataProtectionTokenProviderOptions>(opt =>
        opt.TokenLifespan = TimeSpan.FromHours(2));
        services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
         opt.TokenLifespan = TimeSpan.FromDays(3));
        services.Configure<PasswordResetTokenProviderOptions>(opt =>
         opt.TokenLifespan = TimeSpan.FromHours(1));

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IJwtService, JwtService>();
        services.AddTransient<IUserServicesManager, UserServicesManager>();
        services.AddTransient<IExternalAuthProvidersValidator, ExternalAuthProvidersValidator>();
        services.AddTransient<IGoogleAuthHelper, GoogleAuthHelper>();
        services.AddTransient<IEncryptService, EncryptService>();
        services.AddTransient<IUserRepository, UserService>();

        services.AddScoped<RoleService>();
        services.AddScoped<IRoleRepository>(provider => 
        { 
            return new RoleCachedService(provider.GetService<RoleService>(),
                provider.GetService<IMemoryCache>());
        
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.ConfigureAuthentication(configuration);

        return services;
    }
}
