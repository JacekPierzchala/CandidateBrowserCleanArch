using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace CandidateBrowserCleanArch.API;

public static class SwaggerExtensions
{
    public static void AddSwaggerDocs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description= configuration["ApiOptions:SecuritySchemeDescription"],
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
               {
                new OpenApiSecurityScheme
                {
                     Reference = new OpenApiReference
                     {
                       Type = ReferenceType.SecurityScheme,
                       Id = "Bearer"
                     },
                     Scheme = "oauth2",
                     Name = "Bearer",
                     In = ParameterLocation.Header,

                 },
                  new List<string>()
                }
             });
        });
    }

}
