using Improver.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Improver.Configuration
{
    public class StaticMiscValueMapConfig : IEntityTypeConfiguration<Staticmiscvaluemap>
    {
        public void Configure(EntityTypeBuilder<Staticmiscvaluemap> builder)
        {
            builder.HasKey(e => e.Staticmiscvaluemapid).HasName("staticmiscvaluemap_pkey");

            builder.ToTable("staticmiscvaluemap");

            builder.Property(e => e.Staticmiscvaluemapid).HasColumnName("staticmiscvaluemapid");
            builder.Property(e => e.Code)
                .HasMaxLength(100)
                .HasColumnName("code");
            builder.Property(e => e.Staticmisctypeid).HasColumnName("staticmisctypeid");

            builder.HasOne(d => d.Staticmisctype).WithMany(p => p.Staticmiscvaluemaps)
                .HasForeignKey(d => d.Staticmisctypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_staticmisctype");
        }

    }

}
