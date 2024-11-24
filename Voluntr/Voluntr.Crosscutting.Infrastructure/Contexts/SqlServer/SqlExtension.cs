using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Voluntr.Crosscutting.Infrastructure.Contexts.SqlServer
{
    public static class SqlExtension
    {
        public static void AddSqlContext<TContext>(
           this IServiceCollection services,
           IConfiguration configuration
        ) where TContext : DbContext
        {
            services.AddDbContext<TContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SqlServer"), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    );
                })
                .LogTo(Console.WriteLine, LogLevel.Information);
            });

            services.AddScoped<DbContext, TContext>();
        }
    }
}
