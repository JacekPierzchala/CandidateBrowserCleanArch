using CandidateBrowserCleanArch.API;
using CandidateBrowserCleanArch.API.Configurations;
using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Identity;
using CandidateBrowserCleanArch.Infrastructure;
using CandidateBrowserCleanArch.Persistence;
using Serilog;
using Azure.Identity;
using Azure;


var builder = WebApplication.CreateBuilder(args);

//var azureServiceTokenProvider = new AzureServiceTokenProvider();
//var keyVaultClient = new KeyVaultClient(
//  new KeyVaultClient.AuthenticationCallback(
//      azureServiceTokenProvider.KeyVaultTokenCallback));

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
var keyVaultEndpoint = new Uri("https://candidatebrowsercleanarc.vault.azure.net/");

//builder.Host.ConfigureAppConfiguration((context, config) =>
//{
//    var builtConfig = config.Build();
//    var vaultName = builtConfig["CandidateBrowserCleanArc"];
//    var keyVaultClient = new KeyVaultClient(async (auth, resource, scope) =>
//    {
//        var credential = new DefaultAzureCredential(false);
//        var token=credential.GetToken(
//            new Azure.Core.TokenRequestContext
//            (new[] { "https://candidatebrowsercleanarc.vault.azure.net" }
//            ));

//        return token.Token;
//    });
//    config.AddAzureKeyVault(vaultName, keyVaultClient, new DefaultKeyVaultSecretManager()); ;
//});


builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential(false));

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

// Add services to the container.
builder.Host.UseSerilog((host, config) => config
    .WriteTo.Console()
    .ReadFrom.Configuration(host.Configuration));

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
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();


