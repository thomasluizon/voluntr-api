using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class AddressMap : EntityTypeConfigurationBase<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.ZipCode)
                .HasMaxLength(8)
                .IsRequired(false);

            builder.Property(x => x.Street)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Number)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Complement)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.Neighbourhood)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Uf)
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(100)
                .IsRequired();

            base.Configure(builder);
        }
    }
}