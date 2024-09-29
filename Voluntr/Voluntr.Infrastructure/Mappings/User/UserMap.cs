using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class UserMap : EntityTypeConfigurationBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(100)
                .IsRequired(false);

            base.Configure(builder);
        }
    }
}
