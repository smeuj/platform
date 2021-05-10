using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Nouwan.SmeujPlatform.Api;
using Serilog;

CreateHostBuilder(args)
    .Build()
    .Run();
Log.CloseAndFlush();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console())
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());