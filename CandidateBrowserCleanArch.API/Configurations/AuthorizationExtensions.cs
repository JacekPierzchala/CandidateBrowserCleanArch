using CandidateBrowserCleanArch.Application;

namespace CandidateBrowserCleanArch.API.Configurations
{
    public static  class AuthorizationExtensions
    {
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(CustomRoleClaims.CandidateDelete, policy =>
                    policy.RequireClaim(CustomClaimTypes.Permission, CustomRoleClaims.CandidateDelete));
                options.AddPolicy(CustomRoleClaims.CandidateRead, policy =>
                    policy.RequireClaim(CustomClaimTypes.Permission, CustomRoleClaims.CandidateRead));
                options.AddPolicy(CustomRoleClaims.CandidateCreate, policy =>
                    policy.RequireClaim(CustomClaimTypes.Permission, CustomRoleClaims.CandidateCreate));
                options.AddPolicy(CustomRoleClaims.CandidateUpdate, policy =>
                    policy.RequireClaim(CustomClaimTypes.Permission, CustomRoleClaims.CandidateUpdate));
            });

        }
        public static void ConfigureCors(this IServiceCollection services) 
        {
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        }
    }
}
