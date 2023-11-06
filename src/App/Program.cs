using Microsoft.AspNetCore.Mvc;
using Smeuj.Platform.App.Features.Home;
using Smeuj.Platform.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Register services
builder.Services.RegisterInfrastructure();

// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddScoped<IHomeHandler, HomeHandler>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/", async ([FromServices] IHomeHandler home, CancellationToken ct) => await home.GetHomeAsync(ct));
app.MapGet("/suggestions",
    async ([FromServices] IHomeHandler home, CancellationToken ct) => await home.GetSuggestionsAsync(ct));
app.MapGet("/search", async ([FromServices] IHomeHandler home, [FromForm] SearchModel model,
        CancellationToken ct) => await home.GetSearch(model, ct));


app.Services.MigrateDatabase();
app.Run();