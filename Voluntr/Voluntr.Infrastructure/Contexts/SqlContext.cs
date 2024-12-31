using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Voluntr.Domain.Config;
using Voluntr.Infrastructure.Mappings;

namespace Voluntr.Infrastructure.Contexts
{
    public class SqlContext(
        DbContextOptions<SqlContext> options,
        Urls urls
    ) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Authentication

            modelBuilder.ApplyConfiguration(new OAuthProviderMap());

            #endregion

            #region User

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new UserCauseMap());
            modelBuilder.ApplyConfiguration(new UserAchievementMap());

            #endregion

            #region Volunteer

            modelBuilder.ApplyConfiguration(new VolunteerMap());
            modelBuilder.ApplyConfiguration(new OnboardingTaskMap());

            #endregion

            #region Ngo

            modelBuilder.ApplyConfiguration(new NgoMap());

            #endregion

            #region Company

            modelBuilder.ApplyConfiguration(new CompanyMap());

            #endregion

            #region Achievement

            modelBuilder.ApplyConfiguration(new AchievementMap());
            modelBuilder.ApplyConfiguration(new CauseMap());

            #endregion

            #region Notification

            modelBuilder.ApplyConfiguration(new NotificationMap());

            #endregion

            #region Email

            modelBuilder.ApplyConfiguration(new EmailMap());

            #endregion

            #region Task

            modelBuilder.ApplyConfiguration(new ProjectMap());
            modelBuilder.ApplyConfiguration(new QuestMap());
            modelBuilder.ApplyConfiguration(new QuestAssignmentMap());

            #endregion

            #region Seed

            modelBuilder.SeedAchievements(urls.BlobStorage);
            modelBuilder.SeedOnboardingTasks(urls.BlobStorage);

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
