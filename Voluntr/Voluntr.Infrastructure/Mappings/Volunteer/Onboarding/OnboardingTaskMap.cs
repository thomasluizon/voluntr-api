using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class OnboardingTaskMap : EntityTypeConfigurationBase<OnboardingTask>
    {
        public override void Configure(EntityTypeBuilder<OnboardingTask> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Image)
                .IsRequired();

            builder.Property(x => x.Redirect)
                .HasMaxLength(100)
                .IsRequired();

            builder.Ignore(x => x.OnboardingTaskEnum);

            base.Configure(builder);
        }
    }
}
