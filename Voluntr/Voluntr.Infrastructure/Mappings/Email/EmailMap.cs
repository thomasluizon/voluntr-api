using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class EmailMap : EntityTypeConfigurationBase<Email>
    {
        public override void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.Property(x => x.TemplateId)
                .HasMaxLength(150);

            builder.Property(x => x.Type)
                .HasMaxLength(50)
                .IsRequired();

            builder.Ignore(x => x.EmailTypeEnum);

            base.Configure(builder);
        }
    }
}
