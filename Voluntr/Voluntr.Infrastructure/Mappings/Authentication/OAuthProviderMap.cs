using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class OAuthProviderMap : EntityTypeConfigurationBase<OAuthProvider>
    {
        public override void Configure(EntityTypeBuilder<OAuthProvider> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.UserInfoApiUrl)
                .IsRequired();

            builder.Property(x => x.NameProperty)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.EmailProperty)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PictureProperty)
                .IsRequired();

            builder.Ignore(x => x.NameEnum);

            base.Configure(builder);
        }
    }
}
