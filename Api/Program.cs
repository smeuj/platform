using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Nouwan.Smeuj.Api;
using Serilog;

CreateHostBuilder(args)
    .Build()
    .Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });