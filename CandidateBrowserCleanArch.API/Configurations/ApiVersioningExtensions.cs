using Microsoft.AspNetCore.Mvc.Versioning;

namespace CandidateBrowserCleanArch.API.Configurations
{
    public static class ApiVersioningExtensions
    {
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                 opt.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"));
               // opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
            });
        }

        public static void ConfigureVersionedApiExplorer(this IServiceCollection services) 
        {
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
        }
    }

    public static class ApiVersionNumber
    {
        public const string V1_0 = "1.0";
        public const string V1_2 = "1.1";
        public const string V2_0 = "2.0";

    };

}
