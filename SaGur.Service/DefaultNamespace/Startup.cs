#if (data)
using Some.Root.DefaultNamespace.Migrations;
using Some.Root.DefaultNamespace.Options;
using Microsoft.EntityFrameworkCore;
#endif
#if (kafka)
using Some.Root.DefaultNamespace.Handlers;
#endif
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace Some.Root.DefaultNamespace;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    private IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IClock>(SystemClock.Instance);
        services
            .AddControllers()
            .AddJsonOptions(opt => opt.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));

#if (kafka)
        services.Configure<KafkaOptions>(Configuration.GetSection("Kafka"));
        services.AddHostedService<SomeHandler>();
#endif

#if (data)
        SomeDbOptions dbOptions = new();
        Configuration.GetSection("SomeDb").Bind(dbOptions);
        services.AddDbContextFactory<SomeDbContext>(opt =>
            opt
            .UseNpgsql(dbOptions.ConnectionString, o => o.UseNodaTime())
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging(true));
#endif
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
    }
}