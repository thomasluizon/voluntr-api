using Microsoft.EntityFrameworkCore;
using Voluntr.Infrastructure.Mappings;

namespace Voluntr.Infrastructure.Contexts
{
    public class SqlContext(
        DbContextOptions<SqlContext> options
    ) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VolunteerMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
