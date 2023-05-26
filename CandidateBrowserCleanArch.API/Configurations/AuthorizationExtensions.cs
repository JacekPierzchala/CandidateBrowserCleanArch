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


                options.AddPolicy(CustomRoleClaims.UserAssignRole, policy =>
                    policy.RequireClaim(CustomClaimTypes.Permission, CustomRoleClaims.UserAssignRole));
                options.AddPolicy(CustomRoleClaims.UserDelete, policy =>
                    policy.RequireClaim(CustomClaimTypes.Permission, CustomRoleClaims.UserDelete));
                options.AddPolicy(CustomRoleClaims.UserLock, policy =>
                    policy.RequireClaim(CustomClaimTypes.Permission, CustomRoleClaims.UserLock));
                options.AddPolicy(CustomRoleClaims.UserUpdate, policy =>
                    policy.RequireClaim(CustomClaimTypes.Permission, CustomRoleClaims.UserUpdate));
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
