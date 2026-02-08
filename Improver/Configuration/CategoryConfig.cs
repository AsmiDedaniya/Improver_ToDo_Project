using Improver.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Improver.Configuration
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Categoryid).HasName("categories_pkey");

            builder.ToTable("categories");

            builder.Property(e => e.Categoryid).HasColumnName("categoryid");
            builder.Property(e => e.Categoryname)
                .HasMaxLength(100)
                .HasColumnName("categoryname");
            builder.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            builder.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            builder.Property(e => e.Lastmodifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddate");
            builder.Property(e => e.Userid).HasColumnName("userid");

            builder.HasOne(d => d.User).WithMany(p => p.Categories)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        }

    }

}
