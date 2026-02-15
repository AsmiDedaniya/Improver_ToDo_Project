using System;
using System.Collections.Generic;
using Improver.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Improver.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Staticmisctype> Staticmisctype { get; set; }

    public virtual DbSet<Staticmiscvaluemap> Staticmiscvaluemap { get; set; }

    public virtual DbSet<Tasks> Tasks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new UserConfig());

        modelBuilder.ApplyConfiguration(new CategoryConfig());

        modelBuilder.ApplyConfiguration(new StaticmiscTypeConfig());
        modelBuilder.ApplyConfiguration(new StaticMiscValueMapConfig());
        modelBuilder.ApplyConfiguration(new TaskConfig());
    }

}
