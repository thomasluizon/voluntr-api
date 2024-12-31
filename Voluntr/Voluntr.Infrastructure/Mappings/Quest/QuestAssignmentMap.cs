using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class QuestAssignmentMap : EntityTypeConfigurationBase<QuestAssignment>
    {
        public override void Configure(EntityTypeBuilder<QuestAssignment> builder)
        {
            builder.Property(x => x.QuestId)
                .IsRequired();

            builder.Property(x => x.VolunteerId)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.SubmissionDate)
                .IsRequired(false);

            builder.Property(x => x.ApprovalDate)
                .IsRequired(false);

            builder.Property(x => x.ImageAttachmentUrl)
                .IsRequired();

            builder.Ignore(x => x.QuestAssignmentStatusEnum);

            builder.HasOne(x => x.Quest)
                .WithMany()
                .HasForeignKey(x => x.QuestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Volunteer)
                .WithMany()
                .HasForeignKey(x => x.VolunteerId)
                .OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}
