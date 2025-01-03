using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class VolunteerMap : EntityTypeConfigurationBase<Volunteer>
    {
        public override void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Surname)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Nickname)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(x => x.BirthDate)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
