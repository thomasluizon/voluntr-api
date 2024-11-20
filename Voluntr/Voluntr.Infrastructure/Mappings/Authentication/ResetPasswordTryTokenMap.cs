using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class ResetPasswordTryMap : EntityTypeConfigurationBase<ResetPasswordTry>
    {
        public override void Configure(EntityTypeBuilder<ResetPasswordTry> builder)
        {
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.ResetToken)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
