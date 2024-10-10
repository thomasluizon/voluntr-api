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
            #region User

            modelBuilder.ApplyConfiguration(new VolunteerMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            #endregion

            #region Email

            modelBuilder.ApplyConfiguration(new EmailMap());

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
