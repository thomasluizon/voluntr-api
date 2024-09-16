using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class VolunteerMap : EntityTypeConfigurationBase<Volunteer>
    {
        public override void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
