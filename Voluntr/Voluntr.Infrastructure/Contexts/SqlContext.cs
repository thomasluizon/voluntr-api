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
            #region Authentication

            modelBuilder.ApplyConfiguration(new OAuthProviderMap());

            #endregion

            #region User

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new VolunteerMap());
            modelBuilder.ApplyConfiguration(new NgoMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());

            #endregion

            #region Notification

            modelBuilder.ApplyConfiguration(new NotificationMap());

            #endregion

            #region Email

            modelBuilder.ApplyConfiguration(new EmailMap());

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
