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
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

IConfiguration configuration = builder.Configuration;

// Add services to the container.

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
