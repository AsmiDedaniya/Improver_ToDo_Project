using Improver.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Improver.Configuration
{
    public class TaskConfig : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.HasKey(e => e.Taskid).HasName("tasks_pkey");

            builder.ToTable("tasks");

            builder.Property(e => e.Taskid).HasColumnName("taskid");
            builder.Property(e => e.Categoryid).HasColumnName("categoryid");
            builder.Property(e => e.Createdbyid).HasColumnName("createdbyid");
            builder.Property(e => e.Taskname)
                .HasMaxLength(100).
                HasColumnName("taskname");
            builder.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            builder.Property(e => e.Endtime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("endtime");
            builder.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            builder.Property(e => e.Lastmodifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddate");
            builder.Property(e => e.Starttime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("starttime");
            builder.Property(e => e.Taskdate).HasColumnName("taskdate");
            builder.Property(e => e.Tasknote)
                .HasMaxLength(200)
                .HasColumnName("tasknote");
            builder.Property(e => e.Taskpriorityid).HasColumnName("taskpriorityid");
            builder.Property(e => e.Taskstatusid).HasColumnName("taskstatusid");

            builder.HasOne(d => d.Category).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("fk_tasks_category");

            builder.HasOne(d => d.Createdby).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Createdbyid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tasks_user");

            builder.HasOne(d => d.Taskpriority).WithMany(p => p.TaskTaskpriorities)
                .HasForeignKey(d => d.Taskpriorityid)
                .HasConstraintName("fk_tasks_priority");

            builder.HasOne(d => d.Taskstatus).WithMany(p => p.TaskTaskstatuses)
                .HasForeignKey(d => d.Taskstatusid)
                .HasConstraintName("fk_tasks_status");
        }

    }

}
