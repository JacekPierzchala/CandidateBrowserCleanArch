using CandidateBrowserCleanArch.API;
using CandidateBrowserCleanArch.API.Configurations;
using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Identity;
using CandidateBrowserCleanArch.Infrastructure;
using CandidateBrowserCleanArch.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.ConfigureAuthorization();
builder.Services.ConfigureCors();

builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureVersionedApiExplorer();
builder.Services.AddSwaggerDocs(builder.Configuration);
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    SwaggerExtensions.ConfigureSwaggerUI(app);
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();


