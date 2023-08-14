using Some.Root.DefaultNamespace.Data;
using Microsoft.EntityFrameworkCore;

namespace Some.Root.DefaultNamespace.Migrations;

public class SomeDbContext : DbContext
{
    public DbSet<SomeRecord> SomeRecords { get; set; }
    public SomeDbContext(DbContextOptions<SomeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("some_schema");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SomeDbContext).Assembly);
    }
}
