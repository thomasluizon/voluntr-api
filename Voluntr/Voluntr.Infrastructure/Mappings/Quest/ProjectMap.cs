using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voluntr.Crosscutting.Infrastructure.Mappings;
using Voluntr.Domain.Models;

namespace Voluntr.Infrastructure.Mappings
{
    public class ProjectMap : EntityTypeConfigurationBase<Project>
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.NgoId)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.DueDate)
                .IsRequired(false);

            builder.HasOne(x => x.Ngo)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.NgoId)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
