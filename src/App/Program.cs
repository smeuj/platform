using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using Smeuj.Platform.App.Features.Home;
using Smeuj.Platform.App.Lib.Sqids;
using Smeuj.Platform.App.Lib.Version;
using Smeuj.Platform.Infrastructure;
using Smeuj.Platform.Infrastructure.Database;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path: "appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(configuration.GetSection("SeriLog")["Path"] ?? "log.txt", rollingInterval: RollingInterval.Day, 
        retainedFileTimeLimit:TimeSpan.FromDays(90))
    .MinimumLevel.Is(Enum.Parse<LogEventLevel>(configuration.GetSection("SeriLog")["LogLevel"] ?? "Information"))
    .CreateLogger();

Log.Information("Starting web application");
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

//Register services
builder.Services.RegisterInfrastructure();

// Add services to the container.
builder.Services.AddDataProtection()
    .PersistKeysToDbContext<Database>();

builder.Services.AddAntiforgery();
builder.Services.AddRazorComponents();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHsts(options => {
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(1.095);
});

builder.Services.RegisterSqids(builder.Configuration);
builder.Services.RegisterVersion();

builder.Services.AddScoped<IHomeHandler, HomeHandler>();

var app = builder.Build();
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

var cacheMaxTwoMonths = (60 * 60 * 24 * 90).ToString();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
            "Cache-Control", $"public, max-age={cacheMaxTwoMonths}");
    }
});
app.UseSerilogRequestLogging();

app.UseAntiforgery();

app.MapGet("/",
    async ([FromServices] IHomeHandler home, [FromQuery] string? search, CancellationToken ct) =>
    await home.GetHomeAsync(search, ct));
app.MapGet("/suggestions",
    async ([FromServices] IHomeHandler home, CancellationToken ct) => await home.GetSuggestionsAsync(ct));

app.Services.MigrateDatabase();
app.Run();