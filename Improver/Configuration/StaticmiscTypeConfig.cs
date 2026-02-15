using Improver.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Improver.Configuration
{
    public class StaticmiscTypeConfig : IEntityTypeConfiguration<Staticmisctype>
    {
        public void Configure(EntityTypeBuilder<Staticmisctype> builder)
        {
            builder.HasKey(e => e.Staticmisctypeid).HasName("staticmisctype_pkey");

            builder.ToTable("staticmisctype");

            builder.Property(e => e.Staticmisctypeid).HasColumnName("staticmisctypeid");
            builder.Property(e => e.Code)
                .HasMaxLength(100)
                .HasColumnName("code");
        }

    }

}
