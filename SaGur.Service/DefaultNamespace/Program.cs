using Some.Root.DefaultNamespace;

Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
        .Build()
        .Run();