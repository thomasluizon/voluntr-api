using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json.Serialization;
using Voluntr.Api.Configurations;
using Voluntr.Api.Conventions;
using Voluntr.Api.Extensions;
using Voluntr.Crosscutting.Domain.Middlewares;
using Voluntr.Crosscutting.Domain.Services.Authentication;
using Voluntr.Crosscutting.Infrastructure.Contexts.SqlServer;
using Voluntr.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Configure application settings
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

var configuration = builder.Configuration;

// Register services
builder.AddAzureKeyVaultSetup();
builder.AddLoggingSetup();
builder.Services.AddDependencyInjectionSetup();
builder.Services.AddTokenCredentialSetup(configuration);
builder.Services.AddVoluntrAuthentication(configuration);
builder.Services.AddCryptographySetup(configuration);
builder.Services.AddAutoMapperSetup();
builder.Services.AddSqlContext<SqlContext>(configuration);
builder.Services.AddAzureBlobSetup(configuration);

builder.Services.AddControllers(options =>
    options.Conventions.Add(new ControllerDocumentationConvention()))
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerSetup();
builder.Services.AddResponseCompression();
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    options.Level = CompressionLevel.Optimal);
builder.Services.AddOptions();
builder.Services.AddMediatR(options =>
    options.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Apply migrations and seed database
app.MigrationAndSeedDatabase();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup();
}

app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(cors =>
{
    cors.AllowAnyHeader();
    cors.AllowAnyMethod();
    cors.WithOrigins("http://localhost:8080", "https://deploy-voluntr-web.com");
});

app.MapControllers();

app.Run();
