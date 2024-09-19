using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json.Serialization;
using Voluntr.Api.Configurations;
using Voluntr.Api.Conventions;
using Voluntr.Crosscutting.Domain.Middlewares;
using Voluntr.Crosscutting.Infrastructure.Contexts.SqlServer;
using Voluntr.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

var keyVaultUrl = builder.Environment.IsDevelopment() ? Environment.GetEnvironmentVariable("VOLUNTR_KEY_VAULT_URL_BETA") : Environment.GetEnvironmentVariable(builder.Configuration["KeyVault:KeyVaultUrl"]);
var tenantId = builder.Environment.IsDevelopment() ? Environment.GetEnvironmentVariable("VOLUNTR_TENANT_ID_BETA") : Environment.GetEnvironmentVariable(builder.Configuration["KeyVault:TenantId"]);
var clientId = builder.Environment.IsDevelopment() ? Environment.GetEnvironmentVariable("VOLUNTR_CLIENT_ID_BETA") : Environment.GetEnvironmentVariable(builder.Configuration["KeyVault:ClientId"]);
var clientSecret = builder.Environment.IsDevelopment() ? Environment.GetEnvironmentVariable("VOLUNTR_CLIENT_SECRET_BETA") : Environment.GetEnvironmentVariable(builder.Configuration["KeyVault:ClientSecret"]);

var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
var client = new SecretClient(new Uri(keyVaultUrl), credential);
builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

IConfiguration configuration = builder.Configuration;

// Add services to the container.

var appInsightsConnectionString = builder.Configuration.GetConnectionString("ApplicationInsights");

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddApplicationInsights(
    configureTelemetryConfiguration: (config) => config.ConnectionString = appInsightsConnectionString,
    configureApplicationInsightsLoggerOptions: (options) => { }
);

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = appInsightsConnectionString;
});

builder.Services.AddDependencyInjectionSetup();
//builder.Services.AddVoluntrAuthentication();
builder.Services.AddAutoMapperSetup();
builder.Services.AddSwaggerSetup();
builder.Services.AddSqlContext<SqlContext>(configuration);
builder.Services.AddResponseCompression();
builder.Services.Configure<GzipCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Optimal);
builder.Services.AddCryptographySetup(configuration);
builder.Services.AddControllers(x => x.Conventions.Add(new ControllerDocumentationConvention()))
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddOptions();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddAzureBlobSetup(configuration);
builder.Services.AddTokenCredentialSetup(configuration);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MigrationAndSeedDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup();
}

app.ConfigureExceptionHandler();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseCors(cors =>
{
    cors.AllowAnyHeader();
    cors.AllowAnyMethod();
    cors.WithOrigins(["http://localhost:8080", "https://deploy-voluntr-web.com"]);
});

app.MapControllers();

app.Run();
