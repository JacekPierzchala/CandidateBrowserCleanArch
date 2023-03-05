using CandidateBrowserCleanArch.API;
using CandidateBrowserCleanArch.API.Configurations;
using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Identity;
using CandidateBrowserCleanArch.Persistence;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
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
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    SwaggerExtensions.ConfigureSwaggerUI(app);
    //app.UseSwaggerUI(options =>
    //{
    //    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    //    {
    //        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
    //            description.GroupName.ToUpperInvariant());
    //    }
    //});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


