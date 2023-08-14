Никогда не используйте креды к реальным базам в коде (в том числе в ApplicationDbContextFactory)

Initial setup

1. Install dotnet EF tool
`dotnet tool install --global dotnet-ef (dotnet tool install/update --global dotnet-ef --version 7.0.0)`

2. Install in reference  project
`install nuget Microsoft.EntityFrameworkCore.Design`

3. If it's class - add method to initialize context

```
public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<RepositoryDbContext>
	{
		public RepositoryDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<RepositoryDbContext>();
			optionsBuilder.UseNpgsql("connectionstring");(.UseSqlServer(.UseNpgsql("connectionstring");))
			return new RepositoryDbContext(optionsBuilder.Options);
		}
	}
```

Day by day routine

4. Add new migration
`dotnet ef migrations add InitialCreate`
form sql to apply using Liquibase (we are creating separate files in SQL folder, you can take name of migration file or recreate it using Get-Date in powershell)
Or you can do 'dotnet ef migrations list' and choose 2 last migrations (use 0 if you have only one)

```
$date = Get-Date ([datetime]::UtcNow) -Format "yyyyMMddHHmm";
dotnet ef migrations script 0 MeasureUnitNullability -o ..\DefaultNamespace\Distrib\Database\SQL\20221004070623_MeasureUnitNullability.sql
```
or
'dotnet ef migrations list
Build started...
Build succeeded.
An error occurred while accessing the database. Continuing without the information provided by the database. Error: No such host is known.
20221214092640_InitialCreate
20221215101702_UserNameAndIndex

dotnet ef migrations script InitialCreate UserNameAndIndex -o ..\SaGur.DefaultNamespace\Distrib\Database\SQL\20221215101702_UserNameAndIndex.sql
'
5. Remove migration
```
dotnet ef migrations list - choose migration you want to be base now
dotnet ef Database update BaseMigrationName - you will revert all migrations after base (if you want to communicate with dbase directly - which is unfortunate)
dotnet ef migrations remove --force - remove last migration (--force (-f) flag - it will give you warning if connection to the database unavailable, but remove migration anyway)
```

If you started to use this strategy not from begging.
