using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json.Serialization;
using Voluntr.Api.Configurations;
using Voluntr.Api.Conventions;
using Voluntr.Api.Filters;
using Voluntr.Crosscutting.Domain.Middlewares;
using Voluntr.Crosscutting.Domain.Services.Authentication;
using Voluntr.Crosscutting.Infrastructure.Contexts.SqlServer;
using Voluntr.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var configuration = builder.Configuration;

// Register services
builder.AddAzureKeyVaultSetup();
builder.AddLoggingSetup();

builder.Services.AddScoped<UserTypeValidationFilter>();
builder.Services.AddDependencyInjectionSetup();
builder.Services.AddTokenCredentialSetup(configuration);
builder.Services.AddUrlsSetup(configuration);
builder.Services.AddVoluntrAuthentication(configuration);
builder.Services.AddCryptographySetup(configuration);
builder.Services.AddAutoMapperSetup();
builder.Services.AddSqlContext<SqlContext>(configuration);
builder.Services.AddAzureBlobSetup(configuration);
builder.Services.AddSendGridSetup(configuration);
builder.Services.AddSwaggerSetup();
builder.Services.AddResponseCompression();
builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
builder.Services.AddSignalR();
builder.Services.AddOptions();
builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddControllers(options =>
    {
        options.Conventions.Add(new ControllerDocumentationConvention());
        options.Filters.Add<UserTypeValidationFilter>();
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Apply migrations and seed database
app.MigrationAndSeedDatabase();

// Configure middlewares
app.UseSwaggerSetup();
app.ConfigureExceptionHandler();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseCors(cors =>
{
    cors.AllowAnyHeader();
    cors.AllowAnyMethod();
    cors.AllowCredentials();
    cors.WithOrigins("" +
        "http://localhost:3000",
        "http://localhost:5000",
        app.Configuration.GetSection("Urls").GetValue<string>("VoluntrWeb"),
        app.Configuration.GetSection("Urls").GetValue<string>("VoluntrApi")
    );
});

app.Run();

public partial class Program { }