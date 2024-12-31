using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class AchievementMap : EntityTypeConfigurationBase<Achievement>
    {
        public override void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.Property(x => x.CauseId)
                .IsRequired(false);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.QuestCount)
                .IsRequired();

            builder.Property(x => x.ImageUrl)
                .IsRequired();

            builder.HasOne(x => x.Cause)
                .WithMany(c => c.Achievements)
                .HasForeignKey(x => x.CauseId)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
