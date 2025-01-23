using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class QuestMap : EntityTypeConfigurationBase<Quest>
    {
        public override void Configure(EntityTypeBuilder<Quest> builder)
        {
            builder.Property(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.DueDate)
                .IsRequired(false);

            builder.Property(x => x.Reward)
                .IsRequired();

            builder.Property(x => x.MaxVolunteers)
                .IsRequired();

            builder.Property(x => x.IsRemote)
                .IsRequired();

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Quests)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
