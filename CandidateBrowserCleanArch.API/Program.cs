
using CandidateBrowserCleanArch.API;
using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Identity;
using CandidateBrowserCleanArch.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

AddSwaggerDoc(builder.Services);

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(options =>
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

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void AddSwaggerDoc(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
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

        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "HR Leave Management Api",

        });

    });
}
