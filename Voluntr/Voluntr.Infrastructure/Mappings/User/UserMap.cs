using Microsoft.EntityFrameworkCore;
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

            builder.Property(x => x.OAuth)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.OAuthProvider)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Ignore(x => x.OAuthProviderEnum);

            base.Configure(builder);
        }
    }
}
