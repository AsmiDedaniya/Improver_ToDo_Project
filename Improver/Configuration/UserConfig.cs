using Improver.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Improver.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Userid).HasName("users_pkey");

            builder.ToTable("users");

            builder.HasIndex(e => e.Email, "users_email_key").IsUnique();

            builder.Property(e => e.Userid).HasColumnName("userid");
            builder.Property(e => e.Createddate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createddate");
            builder.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");
            builder.Property(e => e.Firstname)
                    .HasMaxLength(100)
                    .HasColumnName("firstname");
            builder.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            builder.Property(e => e.Lastmodifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddate");
            builder.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            builder.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password").IsRequired();
            builder.Property(e => e.UserimageUrl)
                .HasMaxLength(255)
                .HasColumnName("userimage_url");
        }
        
    }
}
