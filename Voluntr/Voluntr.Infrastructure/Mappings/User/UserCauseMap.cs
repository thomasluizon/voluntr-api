using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class UserCauseMap : EntityTypeConfigurationBase<UserCause>
    {
        public override void Configure(EntityTypeBuilder<UserCause> builder)
        {
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.CauseId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Cause)
                .WithMany()
                .HasForeignKey(x => x.CauseId)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
