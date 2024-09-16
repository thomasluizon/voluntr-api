using Microsoft.EntityFrameworkCore;
using Voluntr.Infrastructure.Contexts;

namespace Voluntr.Api.Configurations
{
    public static class MigrationManagerSetup
    {
        public static IHost MigrationAndSeedDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var context = (SqlContext)scope.ServiceProvider.GetService(typeof(SqlContext));
                context.Database.Migrate();
            }

            return host;
        }
    }
}
