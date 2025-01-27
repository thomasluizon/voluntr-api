using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class AddressMap : EntityTypeConfigurationBase<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.UserId)
                .IsRequired(false);

            builder.Property(x => x.QuestId)
                .IsRequired(false);

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

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Quest)
                .WithMany()
                .HasForeignKey(x => x.QuestId)
                .OnDelete(DeleteBehavior.NoAction);

            base.Configure(builder);
        }
    }
}