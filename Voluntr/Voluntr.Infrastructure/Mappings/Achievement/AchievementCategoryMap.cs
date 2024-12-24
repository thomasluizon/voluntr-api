using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class AchievementCategoryMap : EntityTypeConfigurationBase<AchievementCategory>
    {
        public override void Configure(EntityTypeBuilder<AchievementCategory> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(x => x.ImageUrl)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
