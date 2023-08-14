using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Some.Root.DefaultNamespace.Migrations;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<SomeDbContext>
{
    public SomeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SomeDbContext>();
        optionsBuilder
            .UseNpgsql(
                "Host=no_host;Port=5432;CommandTimeout=300;Database=no_database;Username=no_user;Password=no_password;",
                x => x
                .MigrationsAssembly(ThisAssembly.Info.Title)
                .UseNodaTime())
            .UseSnakeCaseNamingConvention();
        return new SomeDbContext(optionsBuilder.Options);
    }
}
